using CadastroUsuario.Application.DTOs.Request;
using CadastroUsuario.Application.DTOs.Response;
using CadastroUsuario.Application.Interfaces.Services;
using CadastroUsuario.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace CadastroUsuario.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;


        public IdentityService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public async Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Name, usuarioLogin.Password, false, true);
            if (result.Succeeded)
                return await GerarCredenciais(usuarioLogin.Name);

            var usuarioLoginResponse = new UsuarioLoginResponse();
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    usuarioLoginResponse.AdicionarErro("Essa conta está bloqueada");
                else if (result.IsNotAllowed)
                    usuarioLoginResponse.AdicionarErro("Essa conta não tem permissão para fazer login");

                else
                    usuarioLoginResponse.AdicionarErro("Usuário ou senha estão incorretos");
            }

            return usuarioLoginResponse;
        }

        public async Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastroRequest)
        {
            using var scope = _serviceProvider.CreateScope(); // Cria um escopo de serviço
            var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDataContext>();

            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            IdentityUser userExist = await _userManager.FindByNameAsync(usuarioCadastroRequest.Name);

            if (userExist is null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = usuarioCadastroRequest.Name;
                user.NormalizedUserName = usuarioCadastroRequest.Name.ToUpper();
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, usuarioCadastroRequest.Password);

                if (result.Succeeded)
                    await _userManager.SetLockoutEnabledAsync(user, false);

                var usuarioCadastro = new UsuarioCadastroResponse(result.Succeeded);

                if (!result.Succeeded && result.Errors.Count() > 0)
                    usuarioCadastro.AdicionarErros(result.Errors.Select(r => r.Description));

                if (result.Succeeded)
                {

                    var roleResult = await _userManager.AddToRoleAsync(user, usuarioCadastroRequest.Profile);

                    await transaction.CommitAsync();
                    return usuarioCadastro;

                }

            }

            var usuarioCadastroResponse = new UsuarioCadastroResponse(false);

            var erros = new List<string>();
            erros.Add("Usuário ja cadastrado no sistema");


            if (userExist is not null)
                usuarioCadastroResponse.AdicionarErros(erros);

            return usuarioCadastroResponse;


        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }


        private async Task<UsuarioLoginResponse> GerarCredenciais(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            var accessTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: true);
            var refreshTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: false);

            var dataExpiracaoAccessToken = DateTime.UtcNow.AddHours(24);
            var dataExpiracaoRefreshToken = DateTime.UtcNow.AddHours(36);

            var accessToken = GerarToken(accessTokenClaims, dataExpiracaoAccessToken);
            var refreshToken = GerarToken(refreshTokenClaims, dataExpiracaoRefreshToken);

            return new UsuarioLoginResponse
            (
                sucesso: true,
                accessToken: accessToken,
                refreshToken: refreshToken
            );
        }

        private async Task<IList<Claim>> ObterClaims(IdentityUser user, bool adicionarClaimsUsuario)
        {
            var claims = new List<Claim>();

            // Usar Unix timestamp para nbf e iat
            var nbf = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            var iat = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, nbf));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, iat));

            if (adicionarClaimsUsuario)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                claims.AddRange(userClaims);

                foreach (var role in roles)
                    claims.Add(new Claim("role", role));
            }

            return claims;
        }

        private string GerarToken(IEnumerable<Claim> claims, DateTime dataExpiracao)
        {



            // Gerar chave privada para gerar o token
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            // Gerar assinatura digital do token
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            
            var jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: dataExpiracao,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
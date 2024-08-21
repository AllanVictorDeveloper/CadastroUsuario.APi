
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CadastroUsuario.Infra.IoC
{
    public static class DependencyInjectionJWT
    {
        public static IServiceCollection AddInfrastructureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var teste = configuration["Jwt:SecretKey"];


            _ = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Habilita a autenticacao JWT usando o esquema e desafio definidos
            // Validar o token
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),

                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
          
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");


                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        // Manipulação personalizada da resposta de erro
                        context.HandleResponse();
                        context.Response.ContentType = "application/json";

                        if (context.Error == "invalid_token")
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            var result = new
                            {
                                message = "O token fornecido é inválido.",
                                status = 401
                            };
                            return context.Response.WriteAsJsonAsync(result);
                        }
                        else if (context.Error == "missing_token")
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            var result = new
                            {
                                message = "Token ausente na requisição.",
                                status = 401
                            };
                            return context.Response.WriteAsJsonAsync(result);
                        }
                        else
                        {
                            // Caso genérico
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            var result = new
                            {
                                message = "Você não está autenticado.",
                                status = 401
                            };
                            return context.Response.WriteAsJsonAsync(result);


                        }
                    }
                };
            });

            return services;
        }
    }
}
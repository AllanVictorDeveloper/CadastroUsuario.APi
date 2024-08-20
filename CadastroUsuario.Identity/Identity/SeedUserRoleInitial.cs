using CadastroUsuario.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CadastroUsuario.Identity.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            if (_userManager.FindByNameAsync("allan.santos").Result is null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "allan.santos";
                user.NormalizedUserName = "allan.santos".ToUpper();
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Dev@@dwyfgwyfgywegfyuw").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Administrador").Wait();
                }
            }
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Administrador").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrador";
                role.NormalizedName = "Administrador".ToUpper();

                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Usuario").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Usuario";
                role.NormalizedName = "Usuario".ToUpper();

                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }
        }
    }
}
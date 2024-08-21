using CadastroUsuario.Application.Interfaces.Services;
using CadastroUsuario.Domain.Interfaces;
using CadastroUsuario.Identity.Data;
using CadastroUsuario.Identity.Services;
using CadastroUsuario.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroUsuario.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddDbContext<IdentityDataContext>(options =>
                options.UseNpgsql(connectionString)
            );

            //Services Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            //// Repository
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();

            //// Services
            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<IProductService, ProductService>();

            //// AutoMapper
            //services.AddAutoMapper(typeof(MappingProfile));
            //services.AddAutoMapper(typeof(MappingCommandProfile));

            return services;
        }
    }
}
using CadastroUsuario.Application.Interfaces.Services;
using CadastroUsuario.Application.Services;
using CadastroUsuario.Domain.Entities;
using CadastroUsuario.Domain.Interfaces;
using CadastroUsuario.Domain.Interfaces.Repositories;
using CadastroUsuario.Domain.Interfaces.Services;
using CadastroUsuario.Domain.Services;
using CadastroUsuario.Identity.Data;
using CadastroUsuario.Identity.Services;
using CadastroUsuario.Infra.Data;
using CadastroUsuario.Infra.Data.Repositories;
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
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Connection"));
            });

            services.AddDbContext<IdentityDataContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Connection"))
            );

            //Services Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            // App

            services.AddScoped<IAppPessoa, AppPessoa>();
            services.AddScoped<IAppFoto, AppFoto>();

            //// Repository
            services.AddScoped<IRepositoryPessoa, RepositoryPessoa>();
            services.AddScoped<IRepositoryFoto, RepositoryFoto>();
            //services.AddScoped<IRepositoryBase<Pessoa>, RepositoryBase<Pessoa>>();
            //services.AddScoped<IRepositoryBase<Foto>, RepositoryBase<Foto>>();


            // Services
            services.AddScoped<IServicePessoa, ServicePessoa>();
            services.AddScoped<IServiceFoto, ServiceFoto>();
            //services.AddScoped<IServicoBase<Pessoa>, ServicoBase<Pessoa>>();
            //services.AddScoped<IServicoBase<Foto>, ServicoBase<Foto>>();



            //// AutoMapper
            //services.AddAutoMapper(typeof(MappingProfile));
            //services.AddAutoMapper(typeof(MappingCommandProfile));

            return services;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CadastroUsuario.Infra.IoC
{
    public static class DependencyInjectionSwagger
    {
        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {

               

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CadastroUsuario.API", Version = "v1.0.0" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = @"Cabeçalho de autorização JWT usando schema Bearer.
                                    Entre com a palavra chave 'Bearer' [espaço] e logo apos o seu token
                                    Exemplo: 'Bearer 769823hhdgfkjhsf0'",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme ="oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new string[]{}
                    }
                });
            });

            return services;
        }
    }
}
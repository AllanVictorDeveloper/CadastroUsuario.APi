using CadastroUsuario.Api.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace CadastroUsuario.Api.Configuration
{
    public static class ValidatorConfig
    {
        public static void AddValidationConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<PessoaValidation>();
        }
    }
}

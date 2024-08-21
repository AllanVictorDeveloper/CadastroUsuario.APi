using CadastroUsuario.Infra.Data.Mapper;

namespace CadastroUsuario.Api.Configuration
{
    public static class AutoMapperConfig
    {

        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PessoaMapper));
            
        }
    }
}

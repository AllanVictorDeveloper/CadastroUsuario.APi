using CadastroUsuario.Application.Interfaces.Services;
using CadastroUsuario.Domain.Entities;
using CadastroUsuario.Domain.Interfaces.Services;

namespace CadastroUsuario.Application.Services
{
    public class AppFoto : AppBase<Foto>, IAppFoto
    {
        private readonly IServiceFoto _serviceFoto;
        public AppFoto(IServiceFoto serviceFoto) : base(serviceFoto)
        {
            _serviceFoto = serviceFoto;
        }
    }
}

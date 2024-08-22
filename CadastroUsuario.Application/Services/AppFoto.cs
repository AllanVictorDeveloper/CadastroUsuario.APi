using CadastroUsuario.Application.Interfaces.Services;
using CadastroUsuario.Domain.Entities;
using CadastroUsuario.Domain.Interfaces.Services;

namespace CadastroUsuario.Application.Services
{
    public class AppFoto : AppBase<Foto>, IAppFoto
    {
        public AppFoto(IServicoBase<Foto> servico) : base(servico)
        {
        }
    }
}

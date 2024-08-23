using CadastroUsuario.Domain.Entities;
using CadastroUsuario.Domain.Interfaces.Repositories;
using CadastroUsuario.Domain.Interfaces.Services;


namespace CadastroUsuario.Domain.Services
{
    public class ServiceFoto : ServicoBase<Foto>, IServiceFoto
    {

        //private readonly IRepositoryFoto _repositoryFoto;

        //public ServiceFoto(IRepositoryFoto repositoryFoto) : base(repositoryFoto)
        //{
        //    _repositoryFoto = repositoryFoto;
        //}
        public ServiceFoto(IRepositoryBase<Foto> repositorioBase) : base(repositorioBase)
        {
        }
    }
}

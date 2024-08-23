using CadastroUsuario.Domain.Entities;
using CadastroUsuario.Domain.Interfaces.Repositories;
using CadastroUsuario.Domain.Interfaces.Services;


namespace CadastroUsuario.Domain.Services
{
    public class ServicePessoa : ServicoBase<Pessoa>, IServicePessoa
    {

        //private readonly IRepositoryPessoa _repositorioPessoa;

        //public ServicePessoa(IRepositoryPessoa repositorioPessoa) : base(repositorioPessoa)
        //{
        //    _repositorioPessoa = repositorioPessoa;
        //}
        public ServicePessoa(IRepositoryBase<Pessoa> repositorioBase) : base(repositorioBase)
        {
        }
    }
}

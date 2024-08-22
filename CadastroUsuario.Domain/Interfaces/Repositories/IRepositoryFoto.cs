using CadastroUsuario.Domain.Entities;


namespace CadastroUsuario.Domain.Interfaces.Repositories
{
    public interface IRepositoryFoto : IRepositoryBase<Foto>
    {

        Task<Foto> ObterFotoPrincipalAsync(int pessoaId);
    }
}

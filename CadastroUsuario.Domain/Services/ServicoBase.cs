using CadastroUsuario.Domain.Interfaces.Repositories;
using CadastroUsuario.Domain.Interfaces.Services;

namespace CadastroUsuario.Domain.Services;


public class ServicoBase<T> : IServicoBase<T> where T : class
{
    private readonly IRepositoryBase<T> _repositorioBase;

    public ServicoBase(IRepositoryBase<T> repositorioBase)
    {
        _repositorioBase = repositorioBase;
    }

    public T Atualizar(T objeto)
    {
        return _repositorioBase.Atualizar(objeto);
    }

    public void Excluir(int id)
    {
        _repositorioBase.Excluir(id);
    }

    public void ExcluirPermanente(int id)
    {
        _repositorioBase.ExcluirPermanente(id);
    }

    public async Task<T> Inserir(T objeto)
    {
        return await _repositorioBase.InserirAsync(objeto);
    }

    public T RetornaPorId(int id)
    {
        return _repositorioBase.RetornaPorId(id);
    }

    public ICollection<T> ListarTudo()
    {
        return _repositorioBase.ListarTudo();
    }

}
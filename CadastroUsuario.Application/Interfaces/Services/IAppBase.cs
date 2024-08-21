

namespace CadastroUsuario.Application.Interfaces.Services;

public interface IAppBase<T> where T : class
{
    T Inserir(T objeto);

    T Atualizar(T objeto);

    void Excluir(int id);

    ICollection<T> ListarTudo();

    T RetornaPorId(int id);

}

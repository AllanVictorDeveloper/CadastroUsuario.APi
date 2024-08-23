

namespace CadastroUsuario.Domain.Interfaces.Services;

public interface IServicoBase<T> where T:class
{
    Task<T> Inserir(T objeto);
    T Atualizar(T objeto);
    void Excluir(int id);
    ICollection<T> ListarTudo();
    T RetornaPorId(int id);
    void ExcluirPermanente(int id);


}

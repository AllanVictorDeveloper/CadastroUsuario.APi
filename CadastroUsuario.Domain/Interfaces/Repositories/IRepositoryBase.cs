﻿

namespace CadastroUsuario.Domain.Interfaces.Repositories;

public interface IRepositoryBase<T>  where T : class
{
    Task<T> InserirAsync(T objeto);

    T Atualizar(T objeto);

    void Excluir(int id);

    ICollection<T> ListarTudo();

    T RetornaPorId(int id);

    void ExcluirPermanente(int id);
}
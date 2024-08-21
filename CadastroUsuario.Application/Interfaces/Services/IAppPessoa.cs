using CadastroUsuario.Application.DTOs.Request;
using CadastroUsuario.Application.DTOs.Response;
using CadastroUsuario.Domain.Entities;


namespace CadastroUsuario.Application.Interfaces.Services
{
    public interface IAppPessoa : IAppBase<Pessoa>
    {
        void Cadastrar(PessoaRequest request);


        PessoaResponse Atualizar(PessoaRequest request);

        ICollection<PessoaResponse> RetornarTodos();
        PessoaResponse RetornarPorId(int Id);
    }
}

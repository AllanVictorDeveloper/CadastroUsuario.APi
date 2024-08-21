using CadastroUsuario.Application.Interfaces.Services;
using CadastroUsuario.Domain.Interfaces.Services;

namespace CadastroUsuario.Application.Services
{
    public class AppBase<T> : IAppBase<T> where T : class
    {

        private readonly IServicoBase<T> _servico;

        public AppBase(IServicoBase<T> servico)
        {
            _servico = servico;
        }

        public virtual T Atualizar(T objeto)
        {
            return _servico.Atualizar(objeto);
        }

        public virtual void Excluir(int id)
        {
            _servico.Excluir(id);
        }

        public virtual T Inserir(T objeto)
        {
            return _servico.Inserir(objeto);
        }

        public virtual ICollection<T> ListarTudo()
        {
            return _servico.ListarTudo();
        }

        public virtual T RetornaPorId(int id)
        {
            return _servico.RetornaPorId(id);
        }
    }
}

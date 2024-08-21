using CadastroUsuario.Domain.Entities;
using CadastroUsuario.Domain.Interfaces.Repositories;


namespace CadastroUsuario.Infra.Data.Repositories
{
    public class RepositoryPessoa : RepositoryBase<Pessoa>, IRepositoryPessoa
    {
        public RepositoryPessoa(ApplicationDbContext context) : base(context)
        {


        }


        public override ICollection<Pessoa> ListarTudo()
        {
            return _context.Pessoas
                .OrderByDescending(p => p.DataCadastro)
                .ToList();
        }
    }
}

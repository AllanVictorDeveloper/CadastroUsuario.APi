using CadastroUsuario.Domain.Entities;
using CadastroUsuario.Domain.Interfaces.Repositories;


namespace CadastroUsuario.Infra.Data.Repositories
{
    public class RepositoryFoto : RepositoryBase<Foto>, IRepositoryFoto
    {

        public RepositoryFoto(ApplicationDbContext context) : base(context)
        {


        }

    
   

        public Task<Foto> ObterFotoPrincipalAsync(int pessoaId)
        {
            throw new NotImplementedException();
        }
    }
}

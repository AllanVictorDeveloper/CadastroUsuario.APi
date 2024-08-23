using CadastroUsuario.Domain.Entities;
using CadastroUsuario.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;


namespace CadastroUsuario.Infra.Data.Repositories
{
    public class RepositoryFoto : RepositoryBase<Foto>, IRepositoryFoto
    {


        public RepositoryFoto(ApplicationDbContext context) : base(context)
        {


        }




        public async Task<Foto> ObterFotoPrincipalAsync(int pessoaId)
        {
            return await _context.Fotos
                .Where(f => f.PessoaId == pessoaId && f.Principal)
                .FirstOrDefaultAsync();
        }
    }
}

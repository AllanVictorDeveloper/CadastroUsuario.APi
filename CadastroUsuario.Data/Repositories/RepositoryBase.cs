using CadastroUsuario.Domain.Entities;
using CadastroUsuario.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;


namespace CadastroUsuario.Infra.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T :  EntityBase
    {

        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Atualizar(T objeto)
        {
            var result = _dbSet.Find(objeto.Id);

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(objeto);
                    _context.SaveChanges();
                    return objeto;
                }
                catch (Exception err)
                {
                    throw new Exception(err.StackTrace);
                }
            }
            else
            {
                return null;
            }
        }

        //public void Dispose()
        //{
        //    _context.Dispose();
        //    GC.SuppressFinalize(this);
        //}

        public void Excluir(int id)
        {
           
            try
            {
                var o = _dbSet.Find(id);

                _context.Entry(o).State = EntityState.Detached;
                _context.Entry(o).Property("DataExclusao").CurrentValue = DateTime.Now;
                _context.Entry(o).State = EntityState.Modified;
                _context.SaveChanges();


            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public void ExcluirPermanente(int id)
        {
           
            try
            {
                var objeto = _dbSet.Find(id);

                _context.Entry(objeto).State = EntityState.Detached;
                _context.Entry(objeto).State = EntityState.Deleted;
                _context.SaveChanges();


            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public async Task<T> InserirAsync(T objeto)
        {

            try
            {
                _context.Entry(objeto).State = EntityState.Added;
                await _context.SaveChangesAsync();


                return objeto;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public virtual ICollection<T> ListarTudo()
        {
            return _dbSet.ToList();
        }

        public T RetornaPorId(int id)
        {
            return _dbSet.Find(id);
        }
    }
}

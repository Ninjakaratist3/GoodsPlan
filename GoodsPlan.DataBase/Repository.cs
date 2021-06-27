using GoodsPlan.DataBase.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsPlan.DataBase
{
    public class Repository<T> : IRepository<T> where T : class, IEntityBase<long>
    {
        private readonly RepositoryContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(RepositoryContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Get(long id)
        {
            return _dbSet.Where(t => t.Id == id).FirstOrDefault();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Query()
        {
            return _dbSet;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

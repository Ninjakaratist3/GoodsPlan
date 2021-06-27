using GoodsPlan.DataBase.Models.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsPlan.DataBase
{
    public interface IRepository<T> where T : class, IEntityBase<long>
    {
        T Get(long id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void Update(T entity);
        IQueryable<T> Query();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}

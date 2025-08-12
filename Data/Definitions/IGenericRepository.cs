using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Definitions
{
    public interface IGenericRepository<T>
    {
        void Create(T obj);
        void Update(T obj);
        Task UpdateAsync(T obj);

        void Delete(T obj);
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetEverything();
    }
}

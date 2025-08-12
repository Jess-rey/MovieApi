using Data.Contexts;
using Data.Definitions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    internal class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        protected readonly DbContext _Context;
        public GenericRepository(IContext contexto)
        {
            _Context = contexto as DbContext;
        }
        public void Dispose()
        {
            _Context.Dispose();
        }

        public void Update(T obj)
        {
            _Context.Entry(obj).State = EntityState.Modified;
            _Context.SaveChanges();
        }
        public async Task UpdateAsync(T obj)
        {
            _Context.Entry(obj).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
        }

        public void Create(T obj)
        {
            _Context.Entry(obj).State = EntityState.Added;
            _Context.SaveChanges();
        }

        public void Delete(T obj)
        {
            _Context.Entry(obj).State = EntityState.Deleted;
            _Context.SaveChanges();
        }
        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return _Context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetEverything()
        {
            return _Context.Set<T>();
        }
    }
}

using MessageLogger.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MessageLogger.Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext Context = null;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).ToList();
        }
        
        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }
    }
}

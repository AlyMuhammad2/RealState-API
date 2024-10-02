using BLL.Interfaces;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class Repository<T> :IRepository<T> where T : class
    {
        private readonly Context applicationDbContext;
        DbSet<T> set;
        public Repository(Context applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            set = applicationDbContext.Set<T>();
        }
        public T Add(T entity)
        {
            set.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            T entity = Get(id);
            set.Remove(entity);
        }

        public T Get(int id)
        {
            return set.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return set.ToList();
        }

        public void Update(T UpdatedEntity)
        {
            set.Update(UpdatedEntity);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return applicationDbContext.Set<T>().Where(predicate).ToList();
        }
        
        public T FilterIncluded(string Included, Func<T, bool> func)
        {
            return applicationDbContext.Set<T>().Include(Included).Where(func).FirstOrDefault();
        }
      
        public IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return set.ToList().Where(predicate);
        }
        public IEnumerable<T> GetAllWithInclude(params Func<IQueryable<T>, IQueryable<T>>[] includeExpressions)
        {
            IQueryable<T> query = applicationDbContext.Set<T>();

            foreach (var includeExpression in includeExpressions)
            {
                query = includeExpression(query);
            }

            return query.ToList();
        }
        public T GetWithInclude(int id, Expression<Func<T, bool>> predicate, params Func<IQueryable<T>, IQueryable<T>>[] includeExpressions)
        {
            IQueryable<T> query = applicationDbContext.Set<T>();
            foreach (var includeExpression in includeExpressions)
            {
                query = includeExpression(query);
            }
            return query.FirstOrDefault(predicate );
        }

    }
}

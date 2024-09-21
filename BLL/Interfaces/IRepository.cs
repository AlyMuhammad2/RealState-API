using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Update(T UpdatedEntity);
        void Delete(int id);
        T Add(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FilterIncluded(string Included, Func<T, bool> func);
        IEnumerable<T> GetAll(Func<T, bool> predicate);
        IEnumerable<T> GetAllWithInclude(params Func<IQueryable<T>, IQueryable<T>>[] includeExpressions);
        T GetWithInclude(int id, Expression<Func<T, bool>> predicate, params Func<IQueryable<T>, IQueryable<T>>[] includeExpressions);


    }
}

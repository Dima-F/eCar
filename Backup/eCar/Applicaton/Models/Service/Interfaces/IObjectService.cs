using System;
using System.Linq;
using System.Linq.Expressions;

namespace eCar.Applicaton.Models.Service.Interfaces
{
    public interface IObjectService<T>
    {
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null);        
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

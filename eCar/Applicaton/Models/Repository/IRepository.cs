using System;
using System.Linq;
using System.Linq.Expressions;

namespace eCar.Applicaton.Models.Repository
{
    //this generic interface was found from Json Mitchel post
    public interface IRepository
    {
        void Save();
        //в даном случае переимущества Expression<Func<T, bool>> перед Func<T, bool> будет состоять в том, что 
        //в первом случае из базы данных будет выполнятся уже отфильтрованая выборка, а во втором - полный набор, и уже в памяти будет
        //выполнятся фильтрация.
        IQueryable<T> Query<T>(Expression<Func<T, bool>> filter = null) where T : class;
        T SingleOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;
        void Insert<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
    }
}

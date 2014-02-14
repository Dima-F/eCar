using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace eCar.Applicaton.Models.Repository
{
    public class EFRepository : IRepository
    {
        private readonly DbContext _dataContext;
        //уязвимое место. Здесь нада продумать обходный путь, чтобы конструктор принимал более общий контекст
        //для удобства тестирования.
        public EFRepository(DbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Save()
        {
            _dataContext.SaveChanges();
        }
        public IQueryable<T> Query<T>(Expression<Func<T, bool>> filter = null) where T : class
        {
            IQueryable<T> query = _dataContext.Set<T>();
            if (filter != null)
                query = query.Where(filter);
            return query;
        }
        public T SingleOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _dataContext.Set<T>().SingleOrDefault(predicate);
        }
        public void Insert<T>(T entity) where T : class
        {

            _dataContext.Set<T>().Add(entity);
            _dataContext.SaveChanges();
        }
        public void Update<T>(T entity) where T : class
        {
            DbEntityEntry entityEntry = _dataContext.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                _dataContext.Set<T>().Attach(entity);
                entityEntry.State = EntityState.Modified;
            }
            _dataContext.SaveChanges();
        }
        public void Delete<T>(T entity) where T : class
        {
            _dataContext.Set<T>().Remove(entity);
            _dataContext.SaveChanges();
        }
    }
}
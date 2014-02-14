using System;
using System.Linq;
using eCar.Applicaton.Models.Repository;
using eCar.Applicaton.Models.Service.Entities;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models.Service.Internal
{
    public class CategoryService:ICategoryService
    {
        private readonly IRepository _repository;
        public CategoryService(IRepository repository)
        {
            _repository = repository;
        }       

        public void Insert(Category entity)
        {
            _repository.Insert<Category>(entity);
        }

        public void Update(Category entity)
        {
            _repository.Update<Category>(entity);
        }

        public void Delete(Category entity)
        {
            _repository.Delete<Category>(entity);
        }

        public IQueryable<Category> Query(System.Linq.Expressions.Expression<Func<Category, bool>> filter = null)
        {
            return _repository.Query(filter);
        }

        public Category SingleOrDefault(System.Linq.Expressions.Expression<Func<Category, bool>> predicate)
        {
            return _repository.SingleOrDefault(predicate);
        }
    }
}
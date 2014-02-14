using System;
using System.Linq;
using eCar.Applicaton.Models.Repository;
using eCar.Applicaton.Models.Service.Entities;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models.Service.Internal
{
    public class DepartmentService:IDepartmentService
    {
        private readonly IRepository _repository;
        public DepartmentService(IRepository repository)
        {
            _repository = repository;
        }

        public void Insert(Department entity)
        {
            _repository.Insert<Department>(entity);
        }

        public void Update(Department entity)
        {
            _repository.Update<Department>(entity);
        }

        public void Delete(Department entity)
        {
            _repository.Delete<Department>(entity);
        }

        public IQueryable<Department> Query(System.Linq.Expressions.Expression<Func<Department, bool>> filter = null)
        {
            return _repository.Query(filter);
        }

        public Department SingleOrDefault(System.Linq.Expressions.Expression<Func<Department, bool>> predicate)
        {
            return _repository.SingleOrDefault(predicate);
        }
    }
}
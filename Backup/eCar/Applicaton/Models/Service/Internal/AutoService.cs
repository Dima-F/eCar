using System;
using System.Linq;
using System.Linq.Expressions;
using eCar.Applicaton.Models.Repository;
using eCar.Applicaton.Models.Service.Entities;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models.Service.Internal
{
    public class AutoService:IAutoService
    {
        private readonly IRepository _repository;
        public AutoService(IRepository repository)
        {
            _repository = repository;
        }

        public void Insert(Auto entity)
        {
            _repository.Insert<Auto>(entity);
        }

        public void Update(Auto entity)
        {
            _repository.Update<Auto>(entity);
        }

        public void Delete(Auto entity)
        {
            _repository.Delete<Auto>(entity);
        }

        public IQueryable<Auto> Query(Expression<Func<Auto, bool>> filter = null)
        {
            return _repository.Query(filter);
        }
        public IQueryable<Auto> BrowseDepartmentCategory(int? categoryId, int? departmentId)
        {
            IQueryable<Auto> autos;
            if (categoryId.HasValue)
                autos = Query(a => a.CategoryID == categoryId);
            else if (departmentId.HasValue)
                autos = Query(a=>a.Category.DepartmentID==departmentId);
            else
            {
                autos = Query();
            }
            return autos;
        }

        public Auto SingleOrDefault(Expression<Func<Auto, bool>> predicate)
        {
            return _repository.SingleOrDefault(predicate);
        }
    }
}
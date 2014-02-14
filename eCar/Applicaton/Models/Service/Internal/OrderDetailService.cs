using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using eCar.Applicaton.Models.Repository;
using eCar.Applicaton.Models.Service.Entities;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models.Service.Internal
{
    public class OrderDetailService:IOrderDetailService
    {
        private readonly IRepository _repository;
        public OrderDetailService(IRepository repository)
        {
            _repository = repository;
        }
        public IQueryable<OrderDetail> Query(Expression<Func<OrderDetail, bool>> filter = null)
        {
            return _repository.Query(filter);
        }

        public OrderDetail SingleOrDefault(Expression<Func<OrderDetail, bool>> predicate)
        {
            return _repository.SingleOrDefault(predicate);
        }

        public void Insert(OrderDetail entity)
        {
            _repository.Insert(entity);
        }

        public void Update(OrderDetail entity)
        {
            _repository.Update(entity);
        }

        public void Delete(OrderDetail entity)
        {
            _repository.Delete(entity);
        }
    }
}
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
    public class OrderService:IOrderService
    {
        private readonly IRepository _repository;
        public OrderService(IRepository repository)
        {
            _repository = repository;
        }
        public IQueryable<Order> Query(Expression<Func<Order, bool>> filter = null)
        {
            return _repository.Query(filter);
        }

        public Order SingleOrDefault(Expression<Func<Order, bool>> predicate)
        {
            return _repository.SingleOrDefault(predicate);
        }

        public void Insert(Order entity)
        {
            _repository.Insert(entity);
        }

        public void Update(Order entity)
        {
            _repository.Update(entity);
        }

        public void Delete(Order entity)
        {
            _repository.Delete(entity);
        }
    }
}
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
    public class CartService:ICartService
    {
        private readonly IRepository _repository;
        public CartService(IRepository repository)
        {
            _repository = repository;
        }
        public IQueryable<Cart> Query(Expression<Func<Cart, bool>> filter = null)
        {
            return _repository.Query(filter);
        }

        public Cart SingleOrDefault(Expression<Func<Cart, bool>> predicate)
        {
            return _repository.SingleOrDefault(predicate);
        }

        public void Insert(Cart entity)
        {
            _repository.Insert(entity);
        }

        public void Update(Cart entity)
        {
            _repository.Update(entity);
        }

        public void Delete(Cart entity)
        {
            _repository.Delete(entity);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCar.Applicaton.Models.Service.Interfaces
{
    public interface IShoppingCartService
    {
        ICartService CartService { get; }
        IOrderService OrderService { get;  }
        IOrderDetailService OrderDetailService { get; }
    }

}

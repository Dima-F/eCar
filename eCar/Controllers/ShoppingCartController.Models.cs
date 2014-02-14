using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Service.Entities;

namespace eCar.Controllers
{
    public partial class ShoppingCartController
    {
        public class ShoppingCartViewModel:LayoutModel
        {
            public List<Cart> CartItems { get; set; }
            public decimal CartTotal { get; set; }

        }
        public class ShoppingCartRemoveViewModel:LayoutModel
        {
            public string Message { get; set; }
            public decimal CartTotal { get; set; }
            public int CartCount { get; set; }
            public int ItemCount { get; set; }
            public int DeleteId { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCar.Applicaton.Models.Service.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int AutoId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Auto Auto { get; set; }
        public virtual Order Order { get; set; }
    }
}
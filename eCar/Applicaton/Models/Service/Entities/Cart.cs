using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCar.Applicaton.Models.Service.Entities
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int AutoId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Auto Auto { get; set; }
    } 
}
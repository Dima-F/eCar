using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Service.Entities;

namespace eCar.Controllers
{
    public partial class StoreManagerController
    {
        //ListAutoModels отличается от AutoModels  отсутствием поля PagingInfo.
        public class ListAutoModels : LayoutModel
        {
            public IEnumerable<ListAutoModel> Autos { get; set; }
        }
        //ListAutoModel отличается от AutoModel(StoreController) только отсутствием изображения.
        public class ListAutoModel
        {
            public int AutoID { get; set; }
            public string Name { get; set; }
            //first 16 characters...
            public string ShortDescription { get; set; }
            public decimal Price { get; set; }
        }
        //так сделано только для наследования от LayoutModel
        public class AutoDetailsModel:LayoutModel
        {
            public Auto Auto { get; set; }
        }
    }
}
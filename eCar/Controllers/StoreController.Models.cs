using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Service.Entities;

namespace eCar.Controllers
{
    public partial class StoreController
    {

        #region AutoModels
        public class PagingInfo
        {
            public int TotalItems { get; set; }
            public int ItemsPerPage { get; set; }
            public int CurrentPage { get; set; }
            public int TotalPages
            {
                get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
            }
        }
        public class AutoModels : LayoutModel
        {
            public IEnumerable<AutoModel> Autos { get; set; }
            public PagingInfo PagingInfo { get; set; }
        }

        public class AutoModel
        {
            public int AutoID { get; set; }
            public string Name { get; set; }
            //first 16 characters...
            public string ShortDescription { get; set; }
            public decimal Price { get; set; }
            public string Thumbnail { get; set; }
        }
        public class AutoDetailsModel : LayoutModel
        {
            public Auto Auto { get; set; }
        }
        #endregion

        #region Cat&DepModels
        public class CategoryModel:LayoutModel
        {
            public int CategoryID { get; set; }
            public string Name { get; set; }
        }
        public class DepartmentModel:LayoutModel
        {
            public int DepartmentID { get; set; }
            public string Name { get; set; }
            public IEnumerable<CategoryModel> Categories { get; set; }
        }
        public class IndexStoreModel : LayoutModel
        {
            public IEnumerable<DepartmentModel> Departments { get; set; }
        }
        #endregion
    }
}
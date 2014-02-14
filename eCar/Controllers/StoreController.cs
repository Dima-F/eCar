using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Service.Entities;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Controllers
{
    public partial class StoreController : LayoutController
    {
        // GET: /Store/

        public StoreController(IServices services) : base(services)
        {
        }
        [HttpGet]
        [OutputCache(CacheProfile = "CacheLong")]
        public ActionResult Index()
        {
            var departments = Services.Department.Query();
            var model = new IndexStoreModel()
                            {
                                Departments = departments.Select(d => new DepartmentModel
                                                                          {
                                                                              DepartmentID = d.DepartmentID,
                                                                              Name = d.Name,
                                                                              Categories =
                                                                                  d.Categories.Select(
                                                                                      c => new CategoryModel
                                                                                               {
                                                                                                   CategoryID =
                                                                                                       c.CategoryID,
                                                                                                   Name = c.Name
                                                                                               })
                                                                          })
                            };
            return View(model);
        }
        [HttpGet]
        [OutputCache(CacheProfile = "CacheBrowse")]
        public ActionResult Browse(int? categoryId, int? departmentId, int page = 1)
        {
            var autos = Services.Auto.BrowseDepartmentCategory(categoryId, departmentId);

            var models = new AutoModels
            {
                Autos = autos.Select( a=> new AutoModel
                                 {
                                     AutoID = a.AutoID,
                                     Name = a.Name,
                                     Price = a.Price,
                                     Thumbnail = a.Thumbnail,
                                     ShortDescription = a.Description.Length>25 
                                     ? a.Description.Substring(0,25)+"..."//later number of characters should be configured
                                     :a.Description
                                 }).OrderBy(a=>a.AutoID).Skip((page - 1) * PageSize).Take(PageSize),//конструкция для разбиения на страницы
                PagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = autos.Count() }//конструкция для разбиения на страницы
            };

            return View(models);
        }
        [OutputCache(CacheProfile = "CacheDetails")]
        public ActionResult Details(int id)
        {
            var auto = Services.Auto.SingleOrDefault(a => a.AutoID == id);
            return View(new AutoDetailsModel{Auto=auto});
        }
        //в отличие от главного меню, меню категорий и оттелов будет формироваться из базы данных.
        //для извлечения этих данных в оддельном методе нам необходима техника дочерних действий, которые
        //могут вызываться из любого контроллера (оддельно визуализируется от основной визуализации, виджет)
        [ChildActionOnly]//-этот атрибут не даст вызвать метод непосредственно из стоки запоса.
        public ActionResult CategoriesMenu(int? departmentId,int? categoryId)
        {
            IEnumerable<Category> categories = departmentId.HasValue
                                         ? Services.Category.Query(c => c.DepartmentID == departmentId)
                                         : (categoryId.HasValue
                                                ? Services.Category.Query(c => c.CategoryID == categoryId)
                                                : Services.Category.Query());
            return PartialView(categories);
        }
        [ChildActionOnly]
        public ActionResult DepartmentsMenu()
        {
            var departments = Services.Department.Query();
            return PartialView(departments);
        }
    }
}

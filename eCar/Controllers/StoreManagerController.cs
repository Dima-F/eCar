using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCar.Applicaton.Models.Service.Entities;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Controllers
{ 
    [Authorize(Roles = "Administrator")]
    public partial class StoreManagerController : LayoutController
    {
        public StoreManagerController(IServices services) : base(services) {}

        public ViewResult Index()
        {
            var autos = Services.Auto.Query();

            var models = new ListAutoModels
            {
                Autos = autos.Select( a=> new ListAutoModel
                                 {
                                     AutoID = a.AutoID,
                                     Name = a.Name,
                                     Price = a.Price,
                                     ShortDescription = a.Description.Length>30
                                     ? a.Description.Substring(0,30)+"..."//later number of characters should be configured
                                     :a.Description
                                 }).OrderBy(a=>a.AutoID) 
            };
            return View(models);
        }
        // GET: /StoreManager/Details/5
        public ViewResult Details(int id)
        {
            Auto auto = Services.Auto.SingleOrDefault(a=>a.AutoID==id);
            return View(new AutoDetailsModel{Auto = auto});
        }
        //
        // GET: /StoreManager/Create
        public ActionResult Create()
        {
            //Думаю имя CategoryId не случайно. Позже, при обратной отправке оно будет использоваться для определения свойства auto.CategoryId
            ViewBag.CategoryId = new SelectList(Services.Category.Query(), "CategoryId", "Name", String.Empty);
            return View();
        } 
        // POST: /StoreManager/Create
        //пока что метод разрешает добавить только 1 категорию в коллекцию Categories,но потом нужно будет переделать его и представление
        //чтобы добавлялось много категорий (many to many).Для этого может понадобится MultiSelectList или CheckBoxes
        [HttpPost]
        public ActionResult Create(Auto auto)
        {
            if (ModelState.IsValid)
            {
                Services.Auto.Insert(auto);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(Services.Category.Query(), "CategoryId", "Name", String.Empty);
            return View(auto);
        }
        // GET: /StoreManager/Edit/5
        public ActionResult Edit(int id)
        {
            Auto auto = Services.Auto.Query().Include(a=>a.Category).SingleOrDefault(a => a.AutoID == id);
            ViewBag.CategoryId = new SelectList(Services.Category.Query(), "CategoryId", "Name", auto.CategoryID);
            return View(new AutoDetailsModel{Auto=auto});
        }
        //
        // POST: /StoreManager/Edit/5
        // Не смотря на то, что представление типизировано по AutoDetailsModel, система привязки модели все равно подставит в качестве 
        // параметра методу действия корректное значение Auto. При изменении параметра метода действия на AutoDetailsModel, система привязки
        // оставит поле AutoDetailsModel.Auto пустым и сгенерируется исключение.
        // Второй параметр categoryId пришлось добавить (несмотря на трудности, которые могут возникнуть позже при модульном тестировании)
        // из за проблем обновления свойства auto.CategoryId из списка DropDownList
        [HttpPost]
        public ActionResult Edit(Auto auto,int categoryId)
        {
            if (ModelState.IsValid)
            {
                auto.CategoryID = categoryId;
                Services.Auto.Update(auto);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(Services.Category.Query(), "CategoryId", "Name", auto.CategoryID);
            return View(new AutoDetailsModel{Auto=auto});
        }
        // GET: /StoreManager/Delete/5
        public ActionResult Delete(int id)
        {
            Auto auto = Services.Auto.SingleOrDefault(a=>a.AutoID==id);
            return View(new AutoDetailsModel{Auto = auto});
        }
        // POST: /StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Auto auto = Services.Auto.SingleOrDefault(a=>a.AutoID==id);
            Services.Auto.Delete(auto);
            return RedirectToAction("Index");
        }
    }
}
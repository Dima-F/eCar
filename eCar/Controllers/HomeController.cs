using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Controllers
{
    public class HomeController : LayoutController
    {
        public HomeController(IServices services) : base(services)
        {
        }

        [HandleError]
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            //выбрать 6 наиболее дорогих машин
            var top5Autos = Services.Auto.Query().OrderByDescending(a => a.Price).Take(Services.Config.Current.ShowCarsInHomePage);
            var models = new StoreController.AutoModels
            {
                Autos = top5Autos.Select(a => new StoreController.AutoModel
                {
                    AutoID = a.AutoID,
                    Name = a.Name,
                    Price = a.Price,
                    Thumbnail = a.Thumbnail,
                    ShortDescription = a.Description.Substring(0, Services.Config.Current.ShortDescription) + "..."
                })
            };      
            return View(models);
        }
        public ActionResult MakeArchive()
        {
            string absoluteUrl = HttpContext.Server.MapPath("~/App_Data/ElmahErrors");
            try
            {
                Services.Cloud.ArchiveFolder(absoluteUrl);
                ViewBag.Message = "Everything is OK.";

            }
            catch (Exception exc)
            {
                ViewBag.Message = exc.Message;
            }

            return View();
        }
        public ActionResult AntiXss(string userName)
        {
            ViewBag.UserName = userName;
            return View();
        }
    }
}

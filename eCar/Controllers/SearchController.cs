using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Controllers
{
    public partial class SearchController : LayoutController
    {
        public SearchController(IServices services) : base(services){}
        [HttpGet]
        public ActionResult Index(string q)
        {
            var autos = Services.Auto.Query().ToList();
            var results = autos.Where(a => a.Name.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0)
                                       .Select(a => new SearchResultModel
                                           {
                                               AutoId = a.AutoID,
                                               Name = a.Name,
                                               ShortDescription = a.Description.Length > 25
                                                                      ? a.Description.Substring(0, 25) + "..."
                                                                  //later number of characters should be configured
                                                                      : a.Description
                                           });
            var model = new IndexModel { Results = results, QueryText = q };
            return View(model);
        }

    }
}

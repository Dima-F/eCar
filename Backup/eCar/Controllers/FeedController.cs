using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using eCar.Applicaton.Models;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Controllers
{
    /// <summary>
    /// Контроллер дает тольно возможность пользователям подписываться на рсс ленту. В даном случае это 
    /// лента 10 самых дорогих автомобилей на сайте.
    /// </summary>
    public class FeedController : Controller
    {
        private readonly IServices _services;

        public FeedController(IServices services)
        {
            _services = services;
        }

        public ActionResult Index()
        {
            var baseUri = new Uri(Request.Url.GetLeftPart(UriPartial.Authority));
            
            var autos =
                _services.Auto.Query()
                .OrderByDescending(a => a.Price)//позже надо сделать выборку 10 наиболее покупаемых автомобилей!
                .Take(10)
                .Select(a => new SyndicationItem(
                    a.Name,
                    a.Description,
                    new Uri(baseUri, Url.Action("Details", "Store", new { id = a.AutoID }, null))));

            var feed = new SyndicationFeed(
                title: _services.Config.Current.Heading,
                description: _services.Config.Current.Crossbar,
                feedAlternateLink: new Uri(baseUri, Url.Action("Index", "Feed")),
                items: autos);

            return new RssResult(feed);
        }
    }
}

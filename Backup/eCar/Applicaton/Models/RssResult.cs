using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace eCar.Applicaton.Models
{
    /// <summary>
    /// Класс представляет собой результат метода действия для рсс лент. В переопределенном методе
    /// ExecuteResult ленту, передану классу в качестве параметраметра, переобразовуют в нужный формат и добавляют в виходной поток ответа.
    /// </summary>
    public class RssResult:ActionResult
    {
        private readonly SyndicationFeed _feed;

        public RssResult(SyndicationFeed feed)
        {
            _feed = feed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";

            var formatter = new Rss20FeedFormatter(_feed);
            using (var writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                formatter.WriteTo(writer);
            }
        }
    }
}
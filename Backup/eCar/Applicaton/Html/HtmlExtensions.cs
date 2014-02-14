using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using eCar.Applicaton.Models.Service.Entities;
using eCar.Controllers;

namespace eCar.Applicaton.Html
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, StoreController.PagingInfo pagingInfo, Func<int?,int?,int, string> pageUrl)
        {
            var result = new StringBuilder();
            string categoryId = html.ViewContext.HttpContext.Request["categoryId"];
            string departmentId = html.ViewContext.HttpContext.Request["departmentId"];
            var c = string.IsNullOrEmpty(categoryId) ? (int?) null : int.Parse(categoryId);
            var d = string.IsNullOrEmpty(departmentId) ? (int?)null : int.Parse(departmentId);
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a"); // Construct an <a> tag 
                tag.MergeAttribute("href", pageUrl(c,d,i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
        public static IHtmlString Raw(this HtmlHelper htmlHelper, string html)
        {
            return MvcHtmlString.Create(html);
        }
        public static IHtmlString Concat(this HtmlHelper htmlHelper, params IHtmlString[] strings)
        {
            var concat = string.Join<IHtmlString>("", strings);
            return MvcHtmlString.Create(concat);
        }
        public static IHtmlString Blank(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Empty;
        }       
    }
}
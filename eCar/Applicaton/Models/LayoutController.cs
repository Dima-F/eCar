using System;
using System.Web.Mvc;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models
{
    public class LayoutController:Controller
    {
        protected readonly IServices Services;
        public int PageSize { get; set; }
        public LayoutController(IServices services)
        {
            PageSize = 4;//later it would be changed
            Services = services;
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var viewResult = filterContext.Result as ViewResultBase;
            if (viewResult == null) return;
            //if (viewResult.ViewData.Model == null)
            //    viewResult.ViewData.Model = new LayoutModel();

            //if (!(viewResult.ViewData.Model is LayoutModel))
            //    throw new InvalidCastException("View model must derive from LayoutModel in action " + filterContext.ActionDescriptor.ActionName);

            //var model = (LayoutModel)viewResult.ViewData.Model;
            //InitialiseBaseViewModel(model);
        }

        //private void InitialiseBaseViewModel(LayoutModel model)
        //{
            //var currentUser = Services.User.Current;
            //var config = Services.Config.Current;

            //model.Base = new LayoutModel.LayoutBaseModel
            //{
            //    FriendlyUsername = currentUser.FriendlyName,
            //    IsAuthenticated = currentUser.IsAuthenticated,
            //    IsAdmin = currentUser.IsAdmin,
            //    Theme = config.Theme,
            //    SiteTitle = config.Title,
            //    SiteMetaDescription = config.MetaDescription,
            //    SiteTagline = config.Tagline,
            //    SiteHeading = config.Heading,
            //    Crossbar = config.Crossbar,
            //    GoogleAnalyticsId = config.GoogleAnalyticsId,
            //    TwitterUsername = config.TwitterUsername
            //};
        //}
    }
}
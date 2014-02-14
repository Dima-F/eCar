using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmah;
using eCar.Applicaton.Infrastructure;

namespace eCar.Controllers
{
    public class ErrorController : Controller
    {
        public void LogJavaScriptError(string message)
        {
            ErrorSignal.FromCurrentContext().Raise(new JavaScriptException(message));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExperimProj.Models;

namespace ExperimProj.Controllers
{
    public class HomeController : LayoutController
    {
        public HomeController(ICalculator calculator) : base(calculator)
        {
        }

        public ActionResult Index()
        {
            ViewBag.Message = Calculator.Add(5, 6);

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
    public class MyCalculator:ICalculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
    public interface ICalculator
    {
        int Add(int x, int y);
    }
}

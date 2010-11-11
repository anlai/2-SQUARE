using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2SQUARE.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        //[Authorize]
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.Core.Domain;
using UCDArch.Web.Controller;

namespace _2SQUARE.Controllers
{
    [HandleError]
    public class HomeController : SuperController
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            var user = Repository.OfType<User>().Queryable.Where(a => a.UserName == CurrentUser.Identity.Name);


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

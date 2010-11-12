using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_Data;
using _2SQUARE.App_Data;
using UCDArch.Web.Controller;

namespace _2SQUARE.Controllers
{
    [HandleError]
    public class HomeController : SuperController
    {
        public ActionResult Index()
        {
            var db = new _2SquareDataDataContext();
            var project = db.Projects.ToList();

            return View();
        }

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

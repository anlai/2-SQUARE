using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.Helpers;

namespace _2SQUARE.Controllers
{
    public class InitializationController : ApplicationController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string password, bool caseStudy)
        {
            if (password != ConfigurationManager.AppSettings["InitializationPassword"])
            {
                ModelState.AddModelError("Password", "Password is invalid.");
                return View();
            }

            Initializer.Initilize(caseStudy:caseStudy);
            Message = "Database has been reinitialized";
            return RedirectToAction("Index", "Home");
        }
    }
}

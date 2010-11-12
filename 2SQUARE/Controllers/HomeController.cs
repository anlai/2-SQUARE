using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_Data;
using _2SQUARE.App_Data;
using System.Web.Mvc;
using _2SQUARE.Services;

namespace _2SQUARE.Controllers 
{
    [HandleError]
    public class HomeController : Controller 
    {
        private readonly IProjectService _projectService;

        public HomeController(IProjectService projectService)
        {
            _projectService = projectService;
        }

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

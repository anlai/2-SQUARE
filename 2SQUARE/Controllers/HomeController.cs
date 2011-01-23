using System.Linq;
using System.Web.Mvc;
using _2SQUARE.Models;
using _2SQUARE.Services;

namespace _2SQUARE.Controllers 
{
    [HandleError]
    public class HomeController : SuperController 
    {
        private readonly IProjectService _projectService;

        public HomeController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Index()
        {
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

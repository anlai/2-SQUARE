using System.Web.Mvc;
using _2SQUARE.Models;

namespace _2SQUARE.Controllers
{
    public class SuperController : Controller
    {
        public SquareEntities Db = new SquareEntities();

        protected string CurrentUserId
        {
            get { return User.Identity.Name; }
        }

        public string Message
        {
            get { return (string) TempData["Message"]; }
            set { TempData["Message"] = value; }
        }

        public string ErrorMessage
        {
            get { return (string) TempData["ErrorMessage"]; }
            set { TempData["ErrorMessage"] = value; }
        }
    }
}

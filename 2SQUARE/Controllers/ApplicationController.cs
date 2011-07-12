using System.Web.Mvc;

namespace _2SQUARE.Controllers
{
    public class ApplicationController : Controller
    {
        public SquareContext Db = new SquareContext();

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

        protected override void Dispose(bool disposing)
        {
            Db.Dispose();

            base.Dispose(disposing);
        }
    }
}

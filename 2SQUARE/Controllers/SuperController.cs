using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2SQUARE.Controllers
{
    public class SuperController : Controller
    {
        protected string CurrentUserId
        {
            get { return User.Identity.Name; }
        }
    }
}

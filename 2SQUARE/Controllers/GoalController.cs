using System;
using System.Web.Mvc;
using _2SQUARE.Models;

namespace _2SQUARE.Controllers
{
    public class GoalController : SuperController
    {
        public ActionResult Add(int id /*project step id*/)
        {
            var viewModel = AddGoalViewModel.Create(Db, id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(int id /*project step id*/, Goal goal)
        {
            // parameters are being passed in correctly, just need to save and redirect
            throw new NotImplementedException();
        }
    }
}

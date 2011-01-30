using System;
using System.Web.Mvc;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;

namespace _2SQUARE.Controllers
{
    public class GoalController : SuperController
    {
        private readonly IProjectService _projectService;

        public GoalController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Add(int id /*project step id*/)
        {
            var viewModel = AddGoalViewModel.Create(Db, id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(int id /*project step id*/, Goal goal)
        {
            Validation.Validate(goal, ModelState);

            if (ModelState.IsValid)
            {
                _projectService.AddGoal(id, goal);
            }

            // parameters are being passed in correctly, just need to save and redirect);
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Data.Objects;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;
using System.Linq;

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
            var viewModel = GoalViewModel.Create(Db, id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(int id /*project step id*/, Goal goal)
        {
            Validation.Validate(goal, ModelState);

            if (ModelState.IsValid)
            {
                _projectService.SaveGoal(id, goal);
                Message = string.Format(Messages.Saved, "Goal");
                return RedirectToAction("Step2", goal.SquareType.Name, new {id = id, projectId=goal.ProjectId});
            }

            var viewModel = GoalViewModel.Create(Db, id, goal);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SaveBusinessGoal(int id /*project step id*/, string businessGoal)
        {
            var projectStep = Db.ProjectSteps.Where(a => a.Id == id).Single();
            var goal = projectStep.Project.Goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Business).ToString()).SingleOrDefault();

            // if goal == null, then no current business goal, save as new
            if (goal == null)
            {
                goal = new Goal()
                           {
                               Description = businessGoal,
                               GoalTypeId = ((char)GoalTypes.Business).ToString(),
                               ProjectId = projectStep.ProjectId, 
                               SquareTypeId = projectStep.Step.SquareTypeId
                           };
                Db.AddToGoals(goal);
            }
            // if existing update
            else
            {
                goal.Description = businessGoal;
            }

            Validation.Validate(goal, ModelState);

            if (ModelState.IsValid)
            {
                Db.SaveChanges();
                Db.Refresh(RefreshMode.ClientWins, goal);   // objects were beign cached and showing stale data
                Message = string.Format(Messages.Saved, "Business goal");
            }
            else
            {
                ErrorMessage = string.Format(Messages.UnableSave, "business goal");
            }

            return RedirectToAction("Step2", projectStep.Step.SquareType.Name, new { id = id, projectId = projectStep.ProjectId });
        }

        /// <summary>
        /// Edit a goal
        /// </summary>
        /// <param name="id">Project step id</param>
        /// <param name="goalId"></param>
        /// <returns></returns>
        public ActionResult Edit(int id /* project step id */, int goalId)
        {
            var goal = _projectService.LoadGoal(goalId);
            var projectStep = Db.ProjectSteps.Where(a => a.Id == id).Single();

            if (goal == null)
            {
                ErrorMessage = string.Format(Messages.UnabletoLoad, "goal", goalId);
                return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller,
                                        new {id = projectStep.Id, projectId = projectStep.ProjectId});
            }

            var viewModel = GoalViewModel.Create(Db, id, goal);
            return View(viewModel);
        }

        /// <summary>
        /// Edit a goal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id /* project step id */, int goalId, Goal goal)
        {
            var existingGoal = _projectService.LoadGoal(goalId);
            var projectStep = Db.ProjectSteps.Where(a => a.Id == id).Single();

            if (existingGoal == null)
            {
                ErrorMessage = string.Format(Messages.UnabletoLoad, "goal", goalId);
                return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller,
                                        new { id = projectStep.Id, projectId = projectStep.ProjectId });                
            }

            existingGoal.Description = goal.Description;
            existingGoal.GoalTypeId = goal.GoalTypeId;

            Validation.Validate(existingGoal, ModelState);

            // if function does not return null, we are good for save);
            if (ModelState.IsValid)
            {
                _projectService.SaveGoal(id, existingGoal);
                Message = string.Format(Messages.Saved, "Goal");
                return this.RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller,
                                             new {id = projectStep.Id, projectId = projectStep.ProjectId});
            }

            ErrorMessage = string.Format(Messages.UnableSave, "goal");
            var viewModel = GoalViewModel.Create(Db, id, existingGoal);
            return View(viewModel);
        }

        public RedirectToRouteResult Delete(int id /*project step id*/, int goalId)
        {
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            _projectService.DeleteGoal(goalId);

            Message = string.Format(Messages.Deleted, "Goal");
            return this.RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller,
                                         new {id = projectStep.Id, projectId = projectStep.ProjectId});
        }
    }
}

using System;
using System.Data.Objects;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;
using System.Linq;
using Resources;

namespace _2SQUARE.Controllers
{
    public class GoalController : ApplicationController
    {
        private readonly IProjectService _projectService;

        public GoalController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Add a goal
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <returns></returns>
        public ActionResult Add(int id /*project step id*/)
        {
            // validate access
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            var viewModel = GoalViewModel.Create(Db, _projectService, id, CurrentUserId);
            return View(viewModel);
        }

        /// <summary>
        /// Add a goal
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="goal"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(int id, [Bind(Exclude="Id, GoalType")]Goal goal, string goalTypeId)
        {
            ModelState.Remove("goal.Project");
            ModelState.Remove("goal.SquareType");
            ModelState.Remove("goal.GoalType");

            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            if (!Db.GoalTypes.Where(a => a.Id == goalTypeId).Any())
            {
                ModelState.AddModelError("GoalType", "Goal type is required.");
            }

            if (ModelState.IsValid)
            {
                _projectService.SaveGoal(id, goal, null, goalTypeId);
                Message = string.Format(Messages.Saved, "Goal");
                return RedirectToAction("Step2", goal.SquareType.Name, new {id = id, projectId=goal.Project.Id});
            }

            var viewModel = GoalViewModel.Create(Db, _projectService, id, CurrentUserId, goal);
            return View(viewModel);
        }

        /// <summary>
        /// Save the business goal for security
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="businessGoal">The Business goal text</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveBusinessGoal(int id /*project step id*/, string businessGoal)
        {
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
            var goal = Db.Goals.Include("GoalType").Where(a => a.GoalType.Id == GoalTypes.Business).SingleOrDefault();

            // creating new goal
            if (goal == null)
            {
                goal = new Goal(){Name="Business", Description = businessGoal};

                _projectService.SaveGoal(id, goal, goalTypeId: GoalTypes.Business);
            }
            // updating the existing goal
            else
            {
                goal.Description = businessGoal;

                _projectService.SaveGoal(id, goal, goal.Id);
            }

            return RedirectToAction("Step2", projectStep.Step.SquareType.Name, new { id = id, projectId = projectStep.Project.Id });
        }

        /// <summary>
        /// Edit a goal
        /// </summary>
        /// <param name="id">Project step id</param>
        /// <param name="goalId">Id of goal to edit</param>
        /// <returns></returns>
        public ActionResult Edit(int id, int goalId)
        {
            var goal = _projectService.LoadGoal(goalId);
            var projectStep = Db.ProjectSteps.Where(a => a.Id == id).Single();

            if (goal == null)
            {
                ErrorMessage = string.Format(Messages.UnabletoLoad, "goal", goalId);
                return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller,
                                        new {id = projectStep.Id, projectId = projectStep.Project.Id});
            }

            var viewModel = GoalViewModel.Create(Db, _projectService, id, CurrentUserId, goal);
            return View(viewModel);
        }

        /// <summary>
        /// Edit a goal
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="goalId">Id of goal to edit</param>
        /// <param name="goal">goal with new values</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, int goalId, [Bind(Exclude="Id, GoalType")]Goal goal, string goalTypeId)
        {
            ModelState.Remove("goal.Project");
            ModelState.Remove("goal.SquareType");
            ModelState.Remove("goal.GoalType");

            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            if (!Db.GoalTypes.Where(a => a.Id == goalTypeId).Any())
            {
                ModelState.AddModelError("GoalType", "Goal type is required.");
            }
            
            var existingGoal = _projectService.LoadGoal(goalId);

            if (existingGoal == null)
            {
                ErrorMessage = string.Format(Messages.UnabletoLoad, "goal", goalId);
                return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller,
                                        new { id = projectStep.Id, projectId = projectStep.Project.Id });                
            }

            if (ModelState.IsValid)
            {
                _projectService.SaveGoal(id, goal, goalId, goalTypeId);
                Message = string.Format(Messages.Saved, "Goal");
                return this.RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller,
                                             new { id = projectStep.Id, projectId = projectStep.Project.Id });
            }

            ErrorMessage = string.Format(Messages.UnableSave, "goal");
            var viewModel = GoalViewModel.Create(Db, _projectService, id, CurrentUserId, existingGoal);
            viewModel.Goal.GoalType = Db.GoalTypes.Where(a => a.Id == goalTypeId).Single();
            return View(viewModel);
        }

        /// <summary>
        /// Delete a goal
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="goalId">Goal Id to delete</param>
        /// <returns></returns>
        public RedirectToRouteResult Delete(int id /*project step id*/, int goalId)
        {
            // this validates the security as well
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            _projectService.DeleteGoal(goalId);

            Message = string.Format(Messages.Deleted, "Goal");
            return this.RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller,
                                         new {id = projectStep.Id, projectId = projectStep.Project.Id});
        }
    }
}

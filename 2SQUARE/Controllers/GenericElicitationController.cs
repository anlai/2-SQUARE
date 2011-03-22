using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    public class GenericElicitationController : ApplicationController, IProcedureController
    {
        private readonly IProjectService _projectService;

        public GenericElicitationController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        public ActionResult Index(int id, int projectId)
        {
            try
            {
                var viewModel = GenericElicitationViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        public ActionResult Add(int id, int projectId)
        {
            try
            {
                var viewModel = RequirementViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Add(int id, int projectId, Requirement requirement)
        {
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            // save the requirement
            _projectService.SaveRequirement(projectId, projectStep.Step.SquareType, requirement, ModelState);

            if (ModelState.IsValid)
            {
                Message = string.Format(Messages.Saved, "Requirement");
                return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = projectStep.Id, projectId = projectStep.ProjectId });
            }

            var viewModel = RequirementViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement);
            return View(viewModel);
        }

        public ActionResult Edit(int id, int projectId, int requirementId)
        {
            try
            {
                var requirement = Db.Requirements.Where(a => a.id == requirementId).Single();

                var viewModel = RequirementViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, int projectId, int requirementId, Requirement requirement)
        {
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            var origRequirement = Db.Requirements.Where(a => a.id == requirementId).Single();

            // copy the values
            origRequirement.Name = requirement.Name;
            origRequirement.Requirement1 = requirement.Requirement1;
            origRequirement.RequirementId = requirement.RequirementId;

            // save the requirement
            _projectService.SaveRequirement(projectId, projectStep.Step.SquareType, origRequirement, ModelState);

            if (ModelState.IsValid)
            {
                Message = string.Format(Messages.Saved, "Requirement");
                return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = projectStep.Id, projectId = projectStep.ProjectId });
            }

            var viewModel = RequirementViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement);
            return View(viewModel);
        }

        public RedirectToRouteResult Delete(int id, int projectId, int requirementId)
        {
            _projectService.DeleteRequirement(projectId, requirementId);

            Message = string.Format(Messages.Deleted, "Requirement");

            return this.RedirectToAction(a => a.Index(id, projectId));

        }
    }
}

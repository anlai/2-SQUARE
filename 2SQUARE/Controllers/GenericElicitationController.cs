using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    /// <summary>
    /// This is the generic method for just entering/editing/deleting requirements from a project
    /// It allows people to perform any technique outside of the program and enter the final output here.
    /// </summary>
    public class GenericElicitationController : ApplicationController, IProcedureController
    {
        private readonly IProjectService _projectService;

        public GenericElicitationController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// View a list of the requirements
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

        /// <summary>
        /// Add a new requirement
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add in the new requirement
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="requirement">Requirement to save</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(int id, int projectId, Requirement requirement)
        {
            ModelState.Remove("requirement.Project");
            ModelState.Remove("requirement.SquareType");

            try
            {
                // load the project step
                var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

                if (ModelState.IsValid)
                {
                    _projectService.SaveRequirement(projectId, projectStep.Step.SquareType.Id, requirement);

                    Message = string.Format(Messages.Saved, "requirements");
                    return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = projectStep.Id, projectId = projectStep.Project.Id });
                }

                var viewModel = RequirementViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        /// <summary>
        /// Edit the requirement
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="requirementId">Requirement Id</param>
        /// <returns></returns>
        public ActionResult Edit(int id, int projectId, int requirementId)
        {
            try
            {
                var requirement = Db.Requirements.Where(a => a.Id == requirementId).Single();

                var viewModel = RequirementViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        /// <summary>
        /// Edit the requirement
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="requirementId">Requirement Id</param>
        /// <param name="requirement">Requirement</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, int projectId, int requirementId, Requirement requirement)
        {
            ModelState.Remove("requirement.Project");
            ModelState.Remove("requirement.SquareType");
            
            try
            {
                var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

                if (ModelState.IsValid)
                {
                    _projectService.SaveRequirement(projectId, projectStep.Step.SquareType.Id, requirement, requirementId);

                    Message = string.Format(Messages.Saved, "Requirement");
                    return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = projectStep.Id, projectId = projectStep.Project.Id });
                }

                var viewModel = RequirementViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        /// <summary>
        /// Delete a requirement
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="requirementId">Requirement Id</param>
        /// <returns></returns>
        public RedirectToRouteResult Delete(int id, int projectId, int requirementId)
        {
            try
            {
                _projectService.DeleteRequirement(projectId, requirementId, CurrentUserId);

                Message = string.Format(Messages.Deleted, "Requirement");

                return this.RedirectToAction(a => a.Index(id, projectId));
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }
    }
}

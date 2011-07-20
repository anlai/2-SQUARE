using System;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    public class RequirementDefectController : ApplicationController
    {
        private readonly IProjectService _projectService;

        public RequirementDefectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Creates a new requirement defect
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="requirementId">Requirement Id</param>
        /// <returns></returns>
        public ActionResult Create(int id, int projectId, int requirementId)
        {
            try
            {
                var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
                var requirement = Db.Requirements.Where(a => a.Id == requirementId).SingleOrDefault();

                if (requirement == null)
                {
                    Message = string.Format(Messages.UnabletoLoad, "requirement", requirementId);
                    return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = id, projectId = projectId });
                }

                var viewModel = RequirementDefectViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        /// <summary>
        /// Create a new requirement defect
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="requirementId">Requirement Id</param>
        /// <param name="defect">Defect</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(int id, int projectId, int requirementId, string defect)
        {
            ModelState.Clear();

            try
            {
                if (string.IsNullOrWhiteSpace(defect)) ModelState.AddModelError("", "No defect description given.");

                if (ModelState.IsValid)
                {
                    _projectService.SaveDefect(projectId, requirementId, defect, CurrentUserId);

                    Message = string.Format(Messages.Saved, "Defect");
                    var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
                    return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = id, projectId = projectId });
                }

                var requirement = Db.Requirements.Where(a => a.Id == requirementId).SingleOrDefault();
                var viewModel = RequirementDefectViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement, new RequirementDefect(){Description = defect});
                return View(viewModel);

            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        /// <summary>
        /// Mark a defect as resolved
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectId"></param>
        /// <param name="defectId"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult Resolve(int id, int projectId, int defectId)
        {
            try
            {
                _projectService.ResolveDefect(projectId, defectId, CurrentUserId);

                Message = string.Format(Messages.Saved, "Defect");
                var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
                return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = id, projectId = projectId });
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }

            // unknown error
            return this.RedirectToAction<ErrorController>(a => a.Index());
        }
    }
}

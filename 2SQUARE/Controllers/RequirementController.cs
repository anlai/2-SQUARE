using System;
using System.Collections.Generic;
using System.Data.Objects;
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
    public class RequirementController : ApplicationController
    {
        private readonly IProjectService _projectService;

        public RequirementController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Categorize a requirement
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="requirementId">Requirement Id</param>
        /// <returns></returns>
        public ActionResult Categorize(int id, int projectId, int requirementId)
        {
            try
            {
                var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
                var requirement = Db.Requirements.Include("Category").Where(a => a.Id == requirementId).Single();

                var viewModel = RequirementCategoryViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }

        [HttpPost]
        public ActionResult Categorize(int id, int projectId, int requirementId, int? categoryId, bool essential)
        {
            // no category selected
            if (!categoryId.HasValue) ModelState.AddModelError("Category", "Category is required.");

            try
            {
                var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

                if (ModelState.IsValid)
                {
                    _projectService.CategorizeRequirement(projectId, categoryId.Value, requirementId, essential, CurrentUserId);

                    Message = string.Format(Messages.Saved, "Categorization");
                    return this.RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = id, projectId = projectId });
                }

            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }

            var requirement = Db.Requirements.Include("Category").Where(a => a.Id == requirementId).Single();

            var viewModel = RequirementCategoryViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement);
            return View(viewModel);
        }

        /// <summary>
        /// Update the order of the requirements
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateRequirementOrder(int projectId, int squareTypeId, string requirements)
        {
            try
            {
                var reqIds = requirements.Split(',');
                var ids = reqIds.Select(a => Convert.ToInt32(a)).ToArray();

                _projectService.UpdateRequirementOrder(projectId, squareTypeId, ids, CurrentUserId);

                return Json(true);
            }
            catch (SecurityException)
            {
                return Json(false);
            }
        }

        /// <summary>
        /// Update the priority of the requirements
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdatePriority(int requirementId, int? priority)
        {
            try
            {
                _projectService.UpdateRequirementPriority(requirementId, priority, CurrentUserId);

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}

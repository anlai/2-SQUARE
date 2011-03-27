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
    public class RequirementDefectController : ApplicationController
    {
        private readonly IProjectService _projectService;

        public RequirementDefectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Create(int id, int projectId, int requirementId)
        {
            try
            {
                var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
                var requirement = Db.Requirements.Where(a => a.id == requirementId).SingleOrDefault();

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

        [HttpPost]
        public RedirectToRouteResult Resolve(int id, int projectId, int defectId)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Helpers;
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

        [HttpPost]
        public ActionResult Create(int id, int projectId, int requirementId, RequirementDefect defect)
        {
            defect.Requirement.Id = requirementId;
            //Validation.Validate(defect, ModelState);

            if (ModelState.IsValid)
            {
                Db.RequirementDefects.Add(defect);
                Db.SaveChanges();

                Message = string.Format(Messages.Saved, "Defect");

                var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
                return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = id, projectId = projectId });
            }

            var requirement = Db.Requirements.Where(a => a.Id == requirementId).SingleOrDefault();
            var viewModel = RequirementDefectViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement, defect);
            return View(viewModel);
        }

        [HttpPost]
        public RedirectToRouteResult Resolve(int id, int projectId, int defectId)
        {
            var defect = Db.RequirementDefects.Where(a => a.Id == defectId).SingleOrDefault();
            defect.Solved = true;

            Db.SaveChanges();

            Message = string.Format(Messages.Saved, "Defect");
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
            return RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = id, projectId = projectId });
        }
    }
}

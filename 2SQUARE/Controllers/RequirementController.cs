using System.Data.Objects;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
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

        public ActionResult Categorize(int id, int projectId, int requirementId)
        {
            try
            {
                var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
                var requirement = Db.Requirements.Where(a => a.id == requirementId).SingleOrDefault();

                if (requirement == null)
                {
                    Message = string.Format(Messages.UnabletoLoad, "requirement", requirementId);
                    return this.RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = id, projectId = projectId });
                }

                var viewModel = RequirementCategoryViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, requirement);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Categorize(int id, int projectId, int requirementId, Requirement requirement)
        {
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
            var origRequirement = Db.Requirements.Where(a => a.id == requirementId).SingleOrDefault();

            if (origRequirement == null)
            {
                Message = string.Format(Messages.UnabletoLoad, "requirement", requirementId);
                return this.RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = id, projectId = projectId });
            }

            origRequirement.CategoryId = requirement.CategoryId;
            origRequirement.Essential = requirement.Essential;

            if (origRequirement.CategoryId <= 0 || origRequirement.CategoryId == null)
            {
                ModelState.AddModelError("Category", string.Format(Messages.Required, "Category"));
            }

            if (ModelState.IsValid)
            {
                Db.SaveChanges();
                Db.Refresh(RefreshMode.StoreWins, origRequirement);

                Message = string.Format(Messages.Saved, "Categorization");
                return this.RedirectToAction(projectStep.Step.Action, projectStep.Step.Controller, new { id = id, projectId = projectId });
            }

            var viewModel = RequirementCategoryViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, origRequirement);
            return View(viewModel);
        }
    }
}

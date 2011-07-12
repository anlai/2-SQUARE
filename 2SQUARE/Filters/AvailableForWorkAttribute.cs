using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Models;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Filters
{
    public class AvailableForWorkAttribute : ActionFilterAttribute
    {
        private IProjectService _projectService = new ProjectService();
        private IValidationService _validationService = new ValidationService(new ProjectService());

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var urlHelper = new UrlHelper(filterContext.RequestContext);

            // if these are null, then the model binder will throw error anyways
            var projectId = Convert.ToInt32(filterContext.RequestContext.HttpContext.Request.Params["projectId"]);
            var id = Convert.ToInt32(filterContext.RouteData.Values["id"]); // project step id
            var logon = filterContext.RequestContext.HttpContext.User.Identity.Name;

            var db = new SquareContext();

            // load pstep
            var pStep = db.ProjectSteps.Where(a => a.Project.Id == projectId && a.Id == id).Single();

            // figure out if the current user has access

            // validate their step access

            Check.Require(pStep != null, "pstep is required.");

            var project = pStep.Project;

            if (!_projectService.IsStepWorking(pStep.Id))
            {
                // this project is not valid for working
                // admin needs to change status
                if (project.ProjectWorkers.Where(a => a.User.Username == logon 
                    && a.Role.Name == RoleNames.RoleProjectManager).Any())
                {
                    filterContext.Controller.TempData["ErrorMessage"] = string.Format(Messages.Manager_NotValidForWork,
                                                                                      pStep.Step.Order,
                                                                                      pStep.Step.SquareType.Name);
                    filterContext.Result = new RedirectResult(urlHelper.Action("ChangeStatus", "Project", new { id = pStep.Project.Id }));
                }
                else
                {
                    // regular user, can't access step
                    filterContext.Controller.TempData["ErrorMessage"] = string.Format(Messages.NotValidForWork,
                                                                                      pStep.Step.Order,
                                                                                      pStep.Step.SquareType.Name);
                    filterContext.Result = new RedirectResult(urlHelper.Action("Details", "Project", new { id = projectId }));
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
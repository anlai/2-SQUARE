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
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var urlHelper = new UrlHelper(filterContext.RequestContext);

            // if these are null, then the model binder will throw error anyways
            var projectId = Convert.ToInt32(filterContext.RequestContext.HttpContext.Request.Params["projectId"]);
            var id = Convert.ToInt32(filterContext.RouteData.Values["id"]); // step id
            var logon = filterContext.RequestContext.HttpContext.User.Identity.Name;

            var db = new SquareEntities();

            // load pstep
            var pStep = db.ProjectSteps.Where(a => a.ProjectId == projectId && a.StepId == id).Single();

            Check.Require(pStep != null, "pstep is required.");

            var project = pStep.Project;

            if (!pStep.DateStarted.HasValue || pStep.DateCompleted.HasValue)
            {
                // this project is not valid for working
                // admin needs to change status
                if (project.ProjectWorkers.Where(a => a.aspnet_Users.UserName == logon 
                    && a.aspnet_Roles.RoleName == RoleNames.RoleProjectManager).Any())
                {
                    filterContext.Result = new RedirectResult(urlHelper.Action("ChangeStatus", "Project", new { id = pStep.Id }));
                }
                else
                {
                    // regular user, can't access step
                    filterContext.Controller.ViewData["ErrorMessage"] = string.Format(Messages.NotValidForWork,
                                                                                      pStep.Step.Order,
                                                                                      pStep.Step.SquareType.Name);
                    filterContext.Result = new RedirectResult(urlHelper.Action("Details", "Project", new { id = projectId }));
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
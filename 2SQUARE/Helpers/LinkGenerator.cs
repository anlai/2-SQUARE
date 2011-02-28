using System.Web.Mvc;
using System.Web.Routing;
using _2SQUARE.Models;

namespace _2SQUARE.Helpers
{
    /// <summary>
    /// Generates links for the primary pages for the steps
    /// </summary>
    public class LinkGenerator
    {
        public static string CreateString(RequestContext requestContext, ProjectStep projectStep)
        {
            var step = projectStep.Step;

            var urlHelper = new UrlHelper(requestContext);
            return urlHelper.Action(step.Action, step.Controller,
                                         new {id = projectStep.Id, projectId = projectStep.ProjectId});
        }

        public static RedirectResult CreateRedirectResult(RequestContext requestContext, ProjectStep projectStep)
        {
            return new RedirectResult(CreateString(requestContext, projectStep));
        }
    }
}
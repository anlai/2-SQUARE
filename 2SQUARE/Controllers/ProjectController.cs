using System;
using System.Collections.Generic;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Models;
using _2SQUARE.Services;
using DesignByContract;
using MvcContrib;
using System.Linq;

namespace _2SQUARE.Controllers
{
    [Authorize]
    public class ProjectController : SuperController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Index()
        {
            var projects = _projectService.GetByUser(CurrentUserId);
            return View(projects);
        }

        public ActionResult Details(int id)
        {
            // check user's access
            if (!_projectService.HasAccess(id, CurrentUserId))
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "Project(" + id + ")")));
            }

            try
            {
                var viewModel = ProjectDetailsViewModel.Create(Db, _projectService, id, CurrentUserId);
                return View(viewModel);
            }
            // user is not authorized
            catch (SecurityException se)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(se.Message));
            }
        }

        /// <summary>
        /// Change the status of a project step
        /// </summary>
        /// <param name="id">Project step id</param>
        /// <returns></returns>
        public ActionResult ChangeStatus(int id)
        {
            // check user's access
            if (!_projectService.HasAccess(id, CurrentUserId))
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "Project(" + id + ")")));
            }

            var project = _projectService.GetProject(id, CurrentUserId);
            var viewModel = ChangeStatusViewModel.Create(project, _projectService);
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult UpdateStatus(int id /* project id */, int stepId, ProjectStepStatus projectStepStatus)
        {
            var step = _projectService.UpdateStatus(stepId, projectStepStatus, CurrentUserId);

            // determine if any steps change in their ability to be edited
            var changeSteps = new List<KeyValuePair<int, bool>>();
            foreach (var a in Db.ProjectSteps.Where(a => a.ProjectId == id))
            {
                changeSteps.Add(new KeyValuePair<int, bool>(a.Id, _projectService.CanStepChangeStatus(id: a.Id)));
            }

            return Json(changeSteps);
        }
    }
}

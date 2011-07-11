using System.Collections.Generic;
using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;
using System.Linq;
using Project = _2SQUARE.Core.Domain.Project;
using ProjectWorker = _2SQUARE.Core.Domain.ProjectWorker;

namespace _2SQUARE.Controllers
{
    [Authorize]
    public class ProjectController : ApplicationController
    {
        private readonly IProjectService _projectService;
        private readonly IProjectsService _projectsService;
        private readonly IValidationService _validationService;

        public ProjectController(IProjectService projectService, IProjectsService projectsService, IValidationService validationService)
        {
            _projectService = projectService;
            _projectsService = projectsService;
            _validationService = validationService;
        }

        /// <summary>
        /// Project Home Page, List of all Projects
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var projects = _projectsService.GetByUser(CurrentUserId);
            return View(projects);
        }

        /// <summary>
        /// Details of a specific project
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
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
        /// Create a new project
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: /Project/Create
        /// </summary>
        /// <param name="project">Project with new information</param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult Create([Bind(Exclude = "Id")]Project project)
        {
            //Validation.Validate(project, ModelState);

            if (ModelState.IsValid)
            {
                db.Projects.Add(project);

                Message = "Successfully created the project";

                return this.RedirectToAction(a => a.Index());
            }

            return View(project);
        }

        /// <summary>
        /// Edit a project
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            return View();
        }

        /// <summary>
        /// POST: /Project/Edit
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="project">Project with new information</param>
        /// <returns></returns>
        public ActionResult Edit(int id, Project project)
        {
            return View();
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
            var viewModel = Models.ChangeStatusViewModel.Create(project, _projectService);
            return View(viewModel);
        }

        /// <summary>
        /// Change the status of the project
        /// </summary>
        /// <remarks>
        /// Used for ajax requests
        /// </remarks>
        /// <param name="id">Project Id</param>
        /// <param name="stepId">Project Step Id</param>
        /// <param name="projectStepStatus">New Project Step Status</param>
        /// <returns>Json Result of validation information (blank=good)</returns>
        [HttpPost]
        public JsonResult UpdateStatus(int id /* project id */, int stepId, ProjectStepStatus projectStepStatus)
        {
            var validationResult = new ValidationChangeStatusResult() {IsValid = true};
            var step = Db.ProjectSteps.Where(a => a.Id == stepId).SingleOrDefault();

            if (step == null)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add("Unable to find step");
                return Json(validationResult);
            }

            // run validation on the step and the status to change to
            validationResult = _validationService.ValidateChangeStatus(step, projectStepStatus==ProjectStepStatus.Complete, projectStepStatus == ProjectStepStatus.Working);

            // if still valid, run the update
            if (validationResult.IsValid)
            {
                step = _projectService.UpdateStatus(stepId, projectStepStatus, CurrentUserId);

                // determine if any steps change in their ability to be edited
                var changeSteps = new List<KeyValuePair<int, bool>>();
                foreach (var a in Db.ProjectSteps.Where(a => a.ProjectId == id))
                {
                    validationResult.ChangeSteps.Add(new KeyValuePair<int, bool>(a.Id, _projectService.CanStepChangeStatus(id: a.Id)));
                }

            }

            // add the project step id
            validationResult.ProjectStepId = stepId;

            return Json(validationResult);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Security;
using System.Web.Mvc;
using CodeFirstMembershipDemoSharp.Data;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;
using System.Linq;
using Resources;
using Project = _2SQUARE.Core.Domain.Project;
using ProjectWorker = _2SQUARE.Core.Domain.ProjectWorker;

namespace _2SQUARE.Controllers
{
    [Authorize]
    public class ProjectController : ApplicationController
    {
        private readonly IProjectService _projectService;
        private readonly IValidationService _validationService;

        public ProjectController(IProjectService projectService, IValidationService validationService)
        {
            _projectService = projectService;
            _validationService = validationService;
        }

        /// <summary>
        /// Project Home Page, List of all Projects
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var projects = _projectService.GetByUser(CurrentUserId);
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
                return this.RedirectToAction<ErrorController>(a => a.Notauthorized(id));
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
            if (ModelState.IsValid)
            {
                _projectService.CreateProject(project.Name, project.Description, CurrentUserId);

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
            var project = _projectService.GetProject(id, CurrentUserId);

            return View(project);
        }

        /// <summary>
        /// POST: /Project/Edit
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="project">Project with new information</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, Project project)
        {
            var projectToEdit = Db.Projects.Where(a => a.Id == id).FirstOrDefault();

            if (projectToEdit != null && ModelState.IsValid)
            {
                projectToEdit.Name = project.Name;
                projectToEdit.Description = project.Description;

                Db.SaveChanges();

                return this.RedirectToAction(a => a.Details(id));
            }

            return View(project);
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
                return this.RedirectToAction<ErrorController>(a => a.Notauthorized(id));
            }

            var project = _projectService.GetProject(id, CurrentUserId);
            var viewModel = ChangeStatusViewModel.Create(project, _projectService);
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
            
            // load the project step
            var step = Db.ProjectSteps.Where(a => a.Id == stepId).SingleOrDefault();

            if (step == null)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add("Unable to find step");
                return Json(validationResult);
            }

            _projectService.UpdateStatus(stepId, projectStepStatus, CurrentUserId);

            validationResult.ProjectStepId = stepId;
        
            return Json(validationResult);
        }

        public ActionResult Permissions(int id)
        {
            var project = _projectService.GetProject(id, CurrentUserId);

            return View(project);
        }

        [HttpPost]
        public ActionResult RemovePermission(int id)
        {
            var worker = Db.ProjectWorkers.Include("Project").Where(a => a.Id == id).FirstOrDefault();

            var project = _projectService.GetProject(worker.Project.Id, CurrentUserId);

            Db.ProjectWorkers.Remove(worker);
            Db.SaveChanges();

            return this.RedirectToAction(a => a.Permissions(project.Id));
        }

        public ActionResult AddPermission(int id)
        {
            var viewModel = AddPermissionViewModel.Create(Db.Users.ToList(), Db.ProjectRoles.ToList());
            viewModel.ProjectId = id;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddPermission(int id, Guid userId, string roleId)
        {
            var permissionCheck = _projectService.GetProject(id, CurrentUserId);

            var project = Db.Projects.Where(a => a.Id == id).FirstOrDefault();
            var user = Db.Users.Where(a => a.UserId == userId).FirstOrDefault();
            var role = Db.ProjectRoles.Where(a => a.Id == roleId).FirstOrDefault();

            if (project == null || user == null || role == null) return this.RedirectToAction(a => a.Index());

            var worker = new ProjectWorker() {Project = project, Role = role, User = user};
            Db.ProjectWorkers.Add(worker);
            Db.SaveChanges();

            Message = "Permission added to project.";

            return this.RedirectToAction(a => a.Permissions(id));
        }
    }

    public class AddPermissionViewModel
    {
        public IList<User> Users { get; set; }
        public IList<ProjectRole> Roles { get; set; }

        public int ProjectId { get; set; }

        public static AddPermissionViewModel Create(List<User> users, List<ProjectRole> roles  )
        {
            var viewModel = new AddPermissionViewModel(){Users = users, Roles = roles};

            return viewModel;
        }
    }
}

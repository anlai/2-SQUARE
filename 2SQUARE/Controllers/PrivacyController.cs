using System;
using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Filters;
using _2SQUARE.Models;
using _2SQUARE.Services;
using DesignByContract;
using MvcContrib;
using System.Linq;

namespace _2SQUARE.Controllers
{
    public class PrivacyController : ApplicationController, ISquareTypeController
    {
        private readonly IProjectService _projectService;

        public PrivacyController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        #region Step 1
        /// <summary>
        /// Agree on Definitions
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step1(int id /* step id */, int projectId)
        {
            try
            {
                var viewModel = Step1ViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);

                // validate that this is a step 1 step
                if (viewModel.ProjectStep.Step.Order != 1 && viewModel.ProjectStep.Step.SquareType.Name == SquareTypes.Privacy) 
                    return this.RedirectToAction<ErrorController>(a => a.InvalidStep(string.Format(Messages.InvalidStep, id, 1)));

                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }
        #endregion  

        #region Step 2
        /// <summary>
        /// Identify Assets and Privacy Goals
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step2(int id /* project step id */, int projectId)
        {
            try
            {
                var viewModel = Step2ViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);

                if (viewModel.ProjectStep.Step.Order != 2 && viewModel.ProjectStep.Step.SquareType.Name == SquareTypes.Privacy) 
                    return this.RedirectToAction<ErrorController>(a => a.InvalidStep(string.Format(Messages.InvalidStep, id, 1)));

                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }
        #endregion

        #region Step 3
        /// <summary>
        /// Develop Artifacts
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step3(int id /*project step id*/, int projectId)
        {
            try
            {
                var viewModel = Step3ViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);

                // validate that this is a step 3 project step
                if (viewModel.ProjectStep.Step.Order != 3 && viewModel.ProjectStep.Step.SquareType.Name == SquareTypes.Privacy) 
                    return this.RedirectToAction<ErrorController>(a => a.InvalidStep(string.Format(Messages.InvalidStep, id, 3)));

                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }
        #endregion

        #region Step 4
        /// <summary>
        /// Risk Assessment
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step4(int id /*project step id*/, int projectId)
        {
            try
            {
                // load the project
                var project = _projectService.GetProject(projectId, CurrentUserId);

                // assesment type picked  out already)
                if (project.PrivacyAssessmentType != null)
                {
                    return RedirectToAction("Index", project.PrivacyAssessmentType.Controller, new { id = id, projectId = projectId });
                }

                var viewModel = Step4ViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        public RedirectToRouteResult SelectAssessmentType(int id /* project step id */, int projectId, int assessmentTypeId)
        {
            try
            {
                var assessmentType = Db.AssessmentTypes.Where(a => a.Id == assessmentTypeId).Single();

                _projectService.SetAssessmentType(projectId, assessmentType, CurrentUserId);

                return RedirectToAction("Index", assessmentType.Controller, new { id = id, projectId = projectId });
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
            catch (Exception)
            {
                ErrorMessage = "Unable to assign assessment type to project.";
                return this.RedirectToAction<SecurityController>(a => a.Step4(id, projectId));
            }
        }
        #endregion

        #region Step 5
        /// <summary>
        /// Select Elicitation Technique
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step5(int id, int projectId)
        {
            try
            {
                // load project
                var project = _projectService.GetProject(projectId, CurrentUserId);

                var viewModel = Step5ViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Step5(int id, int projectId, int elicitationId, string rationale)
        {
            try
            {
                var elicitationType = Db.ElicitationTypes.Where(a => a.Id == elicitationId).Single();

                Check.Require(elicitationType != null, "elicitationType is required.");

                _projectService.SetElicitationType(projectId, elicitationType, rationale, CurrentUserId);

                return this.RedirectToAction(a => a.Step5(id, projectId));
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
            catch (Exception)
            {
                ErrorMessage = "Unable to assign assessment type to project.";
                return this.RedirectToAction<PrivacyController>(a => a.Step5(id, projectId));
            }
        }
        #endregion

        #region Step 6
        /// <summary>
        /// Elicit Privacy Requirements
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step6(int id, int projectId)
        {
            try
            {
                var project = _projectService.GetProject(projectId, CurrentUserId);

                if (project.PrivacyElicitationType != null)
                {
                    return RedirectToAction("Index", project.PrivacyElicitationType.Controller, new { id = id, projectId = projectId });
                }

                return this.RedirectToAction<ProjectController>(a => a.Details(projectId));
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }
        #endregion

        #region Step 7
        /// <summary>
        /// Categorize Requirements
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step7(int id, int projectId)
        {
            try
            {
                var viewModel = Step7ViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }
        #endregion

        #region Step 8
        /// <summary>
        /// Prioritize Requirements
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step8(int id, int projectId)
        {
            try
            {
                var viewModel = Step8ViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        /// <summary>
        /// Updates the order of the requirements for the square type of a project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="SquareType"></param>
        /// <param name="requirements">Ordered list of requirement Ids</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateRequirementOrder(int projectId, int SquareType, string requirementIds)
        {
            try
            {
                var reqs = requirementIds.Split(',');
                var requirements = reqs.Select(a => Convert.ToInt32(a)).ToList();

                for (int i = 0; i < requirements.Count(); i++)
                {
                    var reqId = requirements[i];
                    var req = Db.Requirements.Where(a => a.Id == reqId && a.Project.Id == projectId).FirstOrDefault();
                    req.Order = i;
                }

                Db.SaveChanges();

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        /// <summary>
        /// Updates the priority of a requirement
        /// </summary>
        /// <param name="requirementId"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePriority(int requirementId, int priority)
        {
            var requirement = Db.Requirements.Where(a => a.Id == requirementId).SingleOrDefault();

            if (requirement == null) return Json(false);

            requirement.Priority = priority;

            Db.SaveChanges();

            return Json(true);
        }
        #endregion

        #region Step 9
        /// <summary>
        /// Inspect Requirements
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step9(int id, int projectId)
        {
            try
            {
                var viewModel = Step9ViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }
        #endregion
        

    }
}

﻿using System;
using System.Security;
using System.Web;
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
    [Authorize]
    public class SecurityController : ApplicationController, ISquareTypeController
    {
        private readonly IProjectService _projectService;
        private readonly IValidationService _validationService;

        public SecurityController(IProjectService projectService, IValidationService validationService)
        {
            _projectService = projectService;
            _validationService = validationService;
        }

        #region Step 1
        /// <summary>
        /// Agree on Definitions
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step1(int id /*project step id*/, int projectId)
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
                //return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());

                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }

        }
        #endregion

        #region Step 2
        /// <summary>
        /// Identify Security Goals
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step2(int id /*project step id*/, int projectId)
        {
            try
            {
                var viewModel = Step2ViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);

                // validate that this is a step 2 step
                if (viewModel.ProjectStep.Step.Order != 2 && viewModel.ProjectStep.Step.SquareType.Name == SquareTypes.Privacy) 
                    return this.RedirectToAction<ErrorController>(a => a.InvalidStep(string.Format(Messages.InvalidStep, id, 2)));

                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
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
        public ActionResult Step3(int id, int projectId)
        {
            try
            {
                var viewModel = Step3ViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);

                // validate that this is a step 3 project step
                if (viewModel.ProjectStep.Step.Order != 3 && viewModel.ProjectStep.Step.SquareType.Name == SquareTypes.Security) 
                    return this.RedirectToAction<ErrorController>(a => a.InvalidStep(string.Format(Messages.InvalidStep, id, 3)));

                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }
        #endregion

        #region Step 4
        /// <summary>
        /// Perform Risk Assessment
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step4(int id, int projectId)
        {
            try
            {
                // load the project
                var project = _projectService.GetProject(projectId, CurrentUserId);

                // assesment type picked  out already)
                if (project.SecurityAssessmentType != null)
                {
                    return RedirectToAction("Index", project.SecurityAssessmentType.Controller, new {id=id,projectId=projectId});
                }

                var viewModel = Step4ViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }

        /// <summary>
        /// Set the assessment type for security on the project
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="assessmentTypeId">Assessment Type Id</param>
        /// <returns></returns>
        public RedirectToRouteResult SelectAssessmentType(int id, int projectId, int assessmentTypeId)
        {
            try
            {
                // load project and validate access
                var project = _projectService.GetProject(projectId, CurrentUserId);

                // load the assessment type
                var assessmentType = Db.AssessmentTypes.Where(a => a.Id == assessmentTypeId).Single();

                _projectService.SetAssessmentType(projectId, assessmentTypeId, CurrentUserId);

                return RedirectToAction("Index", assessmentType.Controller, new { id = id, projectId = projectId });
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
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
                var viewModel = Step5ViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }

        /// <summary>
        /// Select Elicitation Technique
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="elicitationId">Elicitation Type Id</param>
        /// <param name="rationale">Rationale for selecting technique</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step5(int id, int projectId, int elicitationId, string rationale)
        {
            try
            {
                _projectService.SetElicitationType(projectId, elicitationId, rationale, CurrentUserId);

                return this.RedirectToAction(a => a.Step5(id, projectId));
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
            catch (Exception)
            {
                ErrorMessage = "Unable to assign assessment type to project.";
                return this.RedirectToAction<SecurityController>(a => a.Step5(id, projectId));
            }
        }
        #endregion

        #region Step 6
        /// <summary>
        /// Elicit Security Requirements
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

                if (project.SecurityElicitationType != null)
                {
                    return RedirectToAction("Index", project.SecurityElicitationType.Controller, new {id=id, projectId=projectId});
                }

                return this.RedirectToAction<ProjectController>(a => a.Details(projectId));
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
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
        public ActionResult Step7(int id, int projectId)
        {
            try
            {
                var viewModel = Step7ViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }
        #endregion

        #region Step 8
        /// <summary>
        /// Prioritize Requirements
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        public ActionResult Step8(int id, int projectId)
        {
            try
            {
                var viewModel = Step8ViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }
        #endregion

        #region Step 9
        /// <summary>
        /// Requirements Inspection
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult Step9(int id, int projectId)
        {
            try
            {
                var viewModel = Step9ViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }
        #endregion
    }
}

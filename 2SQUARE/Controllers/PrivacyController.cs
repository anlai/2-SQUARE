using System;
using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Filters;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;
using System.Linq;

namespace _2SQUARE.Controllers
{
    public class PrivacyController : ApplicationController
    {
        private readonly IProjectService _projectService;

        public PrivacyController(IProjectService projectService)
        {
            _projectService = projectService;
        }

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

        /// <summary>
        /// Develop Artifacts
        /// </summary>
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

        [AvailableForWork]
        public ActionResult Step4(int id /*project step id*/, int projectId)
        {
            try
            {
                // load the project
                var project = _projectService.GetProject(projectId, CurrentUserId);

                // assesment type picked  out already)
                if (project.PrivacyAssessmentId.HasValue)
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
                var assessmentType = Db.AssessmentTypes.Where(a => a.id == assessmentTypeId).Single();

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

        public ActionResult Step5()
        {
            throw new NotImplementedException();

            return View();
        }

        public ActionResult Step6()
        {
            throw new NotImplementedException();

            return View();
        }

        public ActionResult Step7()
        {
            throw new NotImplementedException();

            return View();
        }

        public ActionResult Step8()
        {
            throw new NotImplementedException();

            return View();
        }

        public ActionResult Step9()
        {
            throw new NotImplementedException();

            return View();
        }

    }
}

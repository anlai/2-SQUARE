using System;
using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Filters;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

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

        public ActionResult Step4()
        {
            throw new NotImplementedException();

            return View();
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

using System;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Filters;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    [Authorize]
    public class SecurityController : SuperController
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
                if (viewModel.Step.Step.Order != 1) return this.RedirectToAction<ErrorController>(a => a.InvalidStep(string.Format(Messages.InvalidStep, id, 1)));

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

                // validate that this is a step 1 step
                if (viewModel.ProjectStep.Step.Order != 2) return this.RedirectToAction<ErrorController>(a => a.InvalidStep(string.Format(Messages.InvalidStep, id, 2)));

                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }

        }
        #endregion

        #region Pending
        public ActionResult Step3()
        {
            throw new NotImplementedException();

            return View();
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
        #endregion
    }
}

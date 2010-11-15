using System;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    [Authorize]
    public class SecurityController : SuperController
    {
        private readonly IProjectService _projectService;

        public SecurityController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        #region Step 1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        public ActionResult Step1(int id /*step id*/, int projectId)
        {
            var viewModel = SecurityStep1ViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);

            // validate that this is a step 1 step
            if (viewModel.Step.Order != 1) return this.RedirectToAction<ErrorController>(a => a.InvalidStep(string.Format(Messages.InvalidStep, id, 1)));

            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult RemoveTerm(int id /*step id*/, int projectId)
        {
            return this.RedirectToAction(a => a.Step1(id, projectId));
        }
        #endregion

        public ActionResult Step2()
        {
            throw new NotImplementedException();

            return View();
        }

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

    }
}

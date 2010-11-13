using System;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.Models;
using _2SQUARE.Services;

namespace _2SQUARE.Controllers
{
    public class SecurityController : SuperController
    {
        private readonly IProjectService _projectService;

        public SecurityController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        #region Step 1
        public ActionResult Step1(int id, int projectId)
        {
            var viewModel = SecurityStep1ViewModel.Create(Db, id, projectId);
            return View(viewModel);
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

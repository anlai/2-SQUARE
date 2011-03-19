using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Models;
using _2SQUARE.Services;
using DesignByContract;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    public class PRAUCController : ApplicationController, IRiskAssessmentController
    {
        private readonly IProjectService _projectService;

        public PRAUCController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Index(int id, int projectId)
        {
            try
            {
                var viewModel = RiskAssessmentViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        public ActionResult Add(int id, int projectId)
        {
            return View();
        }
    }
}

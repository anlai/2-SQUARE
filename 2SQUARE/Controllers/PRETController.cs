using System;
using System.Collections.Generic;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    /// <summary>
    /// This is a controller for implementing a version of the computer aided
    /// Privacy Requirement Elicitation Technique as described in
    /// "Computer-Aided Privacy Requirements Elicitation Technique"
    /// by Seiya Miyazaki, Nancy Mead and Justin Zhan
    /// at the 2008 IEEE Asian-Pacific Services Computing Conference
    /// </summary>
    public class PRETController : ApplicationController, IProcedureController
    {
        private readonly IProjectService _projectService;

        public PRETController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Index(int id, int projectId)
        {
            try
            {
                var viewModel = PRETViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        public ActionResult Run(int id, int projectId)
        {
            try
            {
                var viewModel = PRETViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, true);
                return View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Run(int id, int projectId, List<PRETQuestionAnswer> pretQuestionAnswers)
        {
            var viewModel = PRETViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, true, pretQuestionAnswers);
            return View();
        }

    }


}

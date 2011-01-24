using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Filters;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using DesignByContract;
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
        /// 
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step1(int id /*step id*/, int projectId)
        {
            var viewModel = Step1ViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);

            // validate that this step is available
            var message = string.Empty;

            // validate that this is a step 1 step))))
            if (viewModel.Step.Step.Order != 1) return this.RedirectToAction<ErrorController>(a => a.InvalidStep(string.Format(Messages.InvalidStep, id, 1)));

            return View(viewModel);
        }
        #endregion

        #region Step 2
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [AvailableForWork]
        public ActionResult Step2(int id /*step id*/, int projectId)
        {
            throw new NotImplementedException();

            return View();
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

    public class Step2ViewModel
    {
        public ProjectStep ProjectStep { get; set; }
        public Project Project { get; set; }

        // needed for security
        public Goal BusinessGoal { get; set; }
        public List<Goal> SecurityGoals { get; set; }
        
        // needed for privacy
        public List<Goal> PrivacyGoals { get; set; }
        public List<Goal> Assets { get; set; }

        public static Step2ViewModel Create(SquareEntities db, IProjectService projectService, int projectStepId, int projectId, string loginId)
        {
            Check.Require(db != null, "Repository is required.");
            Check.Require(projectService != null, "projectService is required.");

            var project = projectService.GetProject(projectId, loginId);
            var projectStep = db.ProjectSteps.Where(a => a.Id == projectStepId).Single();

            Check.Require(project.id == projectStep.ProjectId, "Project mismatch with project step.");

            var viewModel = new Step2ViewModel()
                                {
                                    Project = project,
                                    ProjectStep = projectStep
                                };

            if (projectStep.Step.SquareType.Name == SquareTypes.Security)
            {
                // load the business goal
                viewModel.BusinessGoal = project.Goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Business).ToString()).SingleOrDefault();

                // load the security goals
                viewModel.SecurityGoals = project.Goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Security).ToString()).ToList();
            }
            else if (projectStep.Step.SquareType.Name == SquareTypes.Privacy)
            {
                viewModel.PrivacyGoals = project.Goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Privacy).ToString()).ToList();
                viewModel.Assets = project.Goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Asset).ToString()).ToList();
            }

            return viewModel;
        }
    }
}

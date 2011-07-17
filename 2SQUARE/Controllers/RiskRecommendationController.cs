using System.Linq;
using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    public class RiskRecommendationController : ApplicationController
    {
        private readonly IProjectService _projectService;

        public RiskRecommendationController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Create a new risk recommendation
        /// </summary>
        /// <param name="id">Risk Id</param>
        /// <param name="projectStepId"></param>
        /// <returns></returns>
        public ActionResult Create(int id, int projectStepId)
        {
            try
            {
                var projectStep = _projectService.GetProjectStep(projectStepId, CurrentUserId);
                var risk = Db.Risks.Include("Project").Include("AssessmentType").Where(a => a.Id == id).Single();

                // ensure the risk belongs to the project
                if (risk.Project.Id != projectStep.Project.Id) throw new SecurityException("Risk does not belong to project");

                var viewModel = RiskRecommendationViewModel.Create(projectStepId, risk);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }


        }

        /// <summary>
        /// Create risk recommendation
        /// </summary>
        /// <param name="id">Risk Id</param>
        /// <param name="projectStepId"></param>
        /// <param name="riskControl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(int id, int projectStepId, RiskRecommendation riskRecommendation)
        {
            // clear the modelstate
            ModelState.Remove("riskRecommendation.Risk");

            try
            {
                var projectStep = _projectService.GetProjectStep(projectStepId, CurrentUserId);
                var risk = Db.Risks.Include("Project").Include("AssessmentType").Where(a => a.Id == id).Single();

                // ensure the risk belongs to the project
                if (risk.Project.Id != projectStep.Project.Id) throw new SecurityException("Risk does not belong to project");

                riskRecommendation.Risk = risk;

                if (ModelState.IsValid)
                {
                    Db.RiskRecommendations.Add(riskRecommendation);
                    Db.SaveChanges();

                    Message = "Risk recommendation has been saved.";
                    return RedirectToAction("Index", risk.AssessmentType.Controller, new { id = projectStepId, projectId = risk.Project.Id });
                }

                var viewModel = RiskRecommendationViewModel.Create(projectStepId, risk, riskRecommendation);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        /// <summary>
        /// Edit a risk recommendation
        /// </summary>
        /// <param name="id">Risk Recommendation Id</param>
        /// <param name="projectStepId"></param>
        /// <returns></returns>
        public ActionResult Edit(int id, int projectStepId)
        {
            try
            {
                var projectStep = _projectService.GetProjectStep(projectStepId, CurrentUserId);
                var riskRecommendation = Db.RiskRecommendations
                                           .Include("Risk").Include("Risk.AssessmentType")
                                           .Include("Risk.Project")
                                           .Where(a => a.Id == id).Single();

                var viewModel = RiskRecommendationViewModel.Create(projectStepId, riskRecommendation.Risk, riskRecommendation);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        /// <summary>
        /// Edit a risk recommendation
        /// </summary>
        /// <param name="id">Risk Recommendation Id</param>
        /// <param name="projectStepId">Project Step Id</param>
        /// <param name="riskRecommendation">Risk Recommendation Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, int projectStepId, RiskRecommendation riskRecommendation)
        {
            // clear the modelstate
            ModelState.Remove("riskRecommendation.Risk");

            try
            {
                var projectStep = _projectService.GetProjectStep(projectStepId, CurrentUserId);
                var recommendationToEdit = Db.RiskRecommendations
                                             .Include("Risk").Include("Risk.Project")
                                             .Include("Risk.AssessmentType")
                                             .Where(a => a.Id == id).Single();

                // copy the values
                recommendationToEdit.Controls = riskRecommendation.Controls;
                recommendationToEdit.Impact = riskRecommendation.Impact;
                recommendationToEdit.Feasibility = riskRecommendation.Feasibility;

                if (ModelState.IsValid)
                {
                    Db.SaveChanges();
                    Message = "Risk recommendation has been updated.";
                    return RedirectToAction("Index", recommendationToEdit.Risk.AssessmentType.Controller, new { id = projectStepId, projectId = recommendationToEdit.Risk.Project.Id });
                }

                var viewModel = RiskRecommendationViewModel.Create(projectStepId, recommendationToEdit.Risk, riskRecommendation);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }


        }
    }
}

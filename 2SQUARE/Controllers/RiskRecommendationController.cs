using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.Helpers;
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
        /// 
        /// </summary>
        /// <param name="id">Risk Id</param>
        /// <param name="projectStepId"></param>
        /// <returns></returns>
        public ActionResult Create(int id, int projectStepId)
        {
            var risk = Db.Risks.Where(a => a.id == id).FirstOrDefault();

            if (risk == null)
            {
                Message = string.Format("Unable to load risk with id {0}", id);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            var viewModel = RiskRecommendationViewModel.Create(projectStepId, risk);

            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Risk Id</param>
        /// <param name="projectStepId"></param>
        /// <param name="riskControl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(int id, int projectStepId, RiskRecommendation riskRecommendation)
        {
            var risk = Db.Risks.Where(a => a.id == id).FirstOrDefault();

            if (risk == null)
            {
                Message = string.Format("Unable to load risk with id {0}", id);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            Validation.Validate(riskRecommendation, ModelState);

            if (ModelState.IsValid)
            {
                _projectService.SaveRiskRecommendation(riskRecommendation, id);
                return RedirectToAction("Index", risk.AssessmentType.Controller, new { id = projectStepId, projectId = risk.ProjectId });
            }

            var viewModel = RiskRecommendationViewModel.Create(projectStepId, risk, riskRecommendation);
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Risk Recommendation Id</param>
        /// <param name="projectStepId"></param>
        /// <returns></returns>
        public ActionResult Edit(int id, int projectStepId)
        {
            // load the risk control
            var riskRecommendation = Db.RiskRecommendations.Where(a => a.id == id).FirstOrDefault();

            if (riskRecommendation == null)
            {
                Message = string.Format("Unable to load risk recommendation with id {0}", id);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            var viewModel = RiskRecommendationViewModel.Create(projectStepId, riskRecommendation.Risk, riskRecommendation);

            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Risk Control Id</param>
        /// <param name="projectStepId"></param>
        /// <param name="riskControl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, int projectStepId, RiskRecommendation riskControl)
        {
            return View();
        }
    }
}

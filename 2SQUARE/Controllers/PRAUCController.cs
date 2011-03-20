using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using DesignByContract;
using MvcContrib;
using Resources;

namespace _2SQUARE.Controllers
{
    public class PRAUCController : ApplicationController, IRiskAssessmentController
    {
        private readonly IProjectService _projectService;

        public PRAUCController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Displays a list of the identified risks, sort them according to the risk level (high first,  low second)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
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
            try
            {
                var viewModel = PRAUCEditViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);

                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Add(int id, int projectId, Risk risk)
        {
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
            var likelihood = Db.RiskLevels.Where(a => a.id == risk.LikelihoodId).Single();
            var damage = Db.RiskLevels.Where(a => a.id == risk.DamageId).Single();

            var riskLevel = CalculateRiskLevel(likelihood, damage, risk.Cost);

            risk.Name = "Created using PRAUC controller";
            risk.ProjectId = projectId;
            risk.SsquareTypeId = projectStep.Step.SquareTypeId;
            risk.AssessmentTypeId = Db.AssessmentTypes.Where(a => a.Controller == AssessmentTypes.PrivacyRiskUbiquitousComputing && a.SquareTypeId == projectStep.Step.SquareTypeId).Select(a => a.id).Single();
            risk.RiskLevelId = riskLevel.id;

            Validate(risk, ModelState);

            if (ModelState.IsValid)
            {
                Db.Risks.AddObject(risk);
                Db.SaveChanges();

                Message = string.Format(Messages.Saved, "Risk");
                return this.RedirectToAction(a => a.Index(id, projectId));
            }

            var viewModel = PRAUCEditViewModel.Create(Db, _projectService, id, projectId, CurrentUserId, risk);
            return View(viewModel);
        }

        public ActionResult Edit(int id, int projectId, int riskId)
        {
            try
            {
                var risk = Db.Risks.Where(a => a.id == riskId).Single();

                var viewModel = PRAUCEditViewModel.Create(Db, _projectService, id, projectId, CurrentUserId, risk);

                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, int projectId, int riskId, Risk risk)
        {
            var origRisk = Db.Risks.Where(a => a.id == riskId).Single();

            // copy in the values that were altered
            origRisk.Description = risk.Description;
            origRisk.LikelihoodId = risk.LikelihoodId;
            origRisk.DamageId = risk.DamageId;
            origRisk.Cost = risk.Cost;

            // recalculate the new risk level
            var likelihood = Db.RiskLevels.Where(a => a.id == risk.LikelihoodId).Single();
            var damage = Db.RiskLevels.Where(a => a.id == risk.DamageId).Single();
            var riskLevel = CalculateRiskLevel(likelihood, damage, risk.Cost);
            origRisk.RiskLevelId = riskLevel.id;

            Validate(origRisk, ModelState);

            if (ModelState.IsValid)
            {
                Db.SaveChanges();

                Message = string.Format(Messages.Saved, "Risk");
                return this.RedirectToAction(a => a.Index(id, projectId));
            }

            var viewModel = PRAUCEditViewModel.Create(Db, _projectService, id, projectId, CurrentUserId, origRisk);
            return View(viewModel);
        }

        /// <summary>
        /// Calculate whether or not the protection should be implemented
        /// </summary>
        /// <remarks>
        /// Utlizes Hdan's Cost Benefit analysis
        /// 
        /// Refer to CMU/SEI-2009-SR-017
        ///          Privacy Risk Assessment Case Studies in Support of SQUARE (Pg. 14)
        /// 
        /// Considers Cost of disclosure against the likelihood of the disclosure and the damage
        /// </remarks>
        /// <param name="likelihood"></param>
        /// <param name="damage"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        private RiskLevel CalculateRiskLevel(RiskLevel likelihood, RiskLevel damage, int? cost)
        {
            Check.Require(likelihood != null, "likelihood is required.");
            Check.Require(damage != null, "damage is required.");
            Check.Require(cost.HasValue , "cost.HasValue is required.");
            Check.Require(cost >= 1 && cost <= 9 , "Cost must be between 1 and 9");

            // calculate LD
            var likelihoodDamage = likelihood.PLikelihood*damage.Damage;

            string riskLevelId = string.Empty;

            // if C<LD then return high
            if (cost < likelihoodDamage) riskLevelId = ((char)RiskLevelsEnum.High).ToString();
            // if C>=LD then return low
            else riskLevelId = ((char) RiskLevelsEnum.Low).ToString();

            return Db.RiskLevels.Where(a => a.id == riskLevelId).Single();
        }

        /// <summary>
        /// Validate the properties for the risk for this object
        /// </summary>
        /// <param name="risk"></param>
        /// <param name="modelState"></param>
        private void Validate(Risk risk, ModelStateDictionary modelState)
        {
            if (string.IsNullOrEmpty(risk.Name))
                modelState.AddModelError("Name", string.Format(Messages.Required, "Name"));

            if (string.IsNullOrEmpty(risk.LikelihoodId) && risk.Likelihood == null)
                modelState.AddModelError("Likelihood", string.Format(Messages.Required, "Likelihood"));

            if (string.IsNullOrEmpty(risk.DamageId) && risk.Damage == null)
                modelState.AddModelError("Damage", string.Format(Messages.Required, "Damage"));

            if (!risk.Cost.HasValue)
                modelState.AddModelError("Cost", string.Format(Messages.Required, "Cost"));
        }
    }
}

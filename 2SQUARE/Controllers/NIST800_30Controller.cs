using System;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;
using System.Linq;
using Resources;

namespace _2SQUARE.Controllers
{
    public class NIST800_30Controller : ApplicationController, IProcedureController
    {
        private readonly IProjectService _projectService;

        public NIST800_30Controller(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Index(int id /* project step id */, int projectId)
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

        public ActionResult Add(int id /* project step id */, int projectId)
        {
            try
            {
                var viewModel = NIST800_30EditViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);

                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Add(int id /* project step id */, int projectId, Risk risk)
        {
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);
            var likelihoodLevel = Db.RiskLevels.Where(a => a.Id == risk.Likelihood.Id).Single();
            var magnitudeLevel = Db.RiskLevels.Where(a => a.Id == risk.Magnitude.Id).Single();

            var riskLevel = CalculateRiskLevel(likelihoodLevel, magnitudeLevel);

            // set the properties that the user can't set
            risk.Project = projectStep.Project;
            risk.SquareType = projectStep.Step.SquareType;
            risk.RiskLevel = riskLevel;
            risk.AssessmentType = Db.AssessmentTypes.Where(a => a.Controller == AssessmentTypes.NIST800_30 && a.SquareType == projectStep.Step.SquareType).Single();

            Validate(risk, ModelState);

            if (ModelState.IsValid)
            {
                Db.Risks.Add(risk);
                Db.SaveChanges();

                Message = string.Format(Messages.Saved, "Risk");
                return this.RedirectToAction(a => a.Index(id, projectId));
            }

            var viewModel = NIST800_30EditViewModel.Create(Db, _projectService, id, projectId, CurrentUserId, risk);
            return View(viewModel);
        }

        public ActionResult Edit(int id /* project step id */, int projectId, int riskId)
        {
            try
            {
                var risk = Db.Risks.Where(a => a.Id == riskId).Single();

                var viewModel = NIST800_30EditViewModel.Create(Db, _projectService, id, projectId, CurrentUserId, risk);

                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Edit(int id /* project step id */, int projectId, int riskId, Risk risk)
        {
            var srcRisk = Db.Risks.Where(a => a.Id == riskId).Single();

            // copy the fields that could have been edited
            srcRisk.Name = risk.Name;
            srcRisk.Source = risk.Source;
            srcRisk.Vulnerability = risk.Vulnerability;
            srcRisk.Likelihood = risk.Likelihood;
            srcRisk.Impact = risk.Impact;
            srcRisk.Magnitude = risk.Magnitude;

            var likelihoodLevel = Db.RiskLevels.Where(a => a.Id == risk.Likelihood.Id).Single();
            var magnitudeLevel = Db.RiskLevels.Where(a => a.Id == risk.Magnitude.Id).Single();
            var riskLevel = CalculateRiskLevel(likelihoodLevel, magnitudeLevel);
            srcRisk.RiskLevel = riskLevel;

            Validate(srcRisk, ModelState);

            if (ModelState.IsValid)
            {
                Db.SaveChanges();
                Message = string.Format(Messages.Saved, "Risk");
                return this.RedirectToAction(a => a.Index(id, projectId));
            }

            var viewModel = NIST800_30EditViewModel.Create(Db, _projectService, id, projectId, CurrentUserId, srcRisk);
            return View(viewModel);
        }

        public JsonResult DetermineRiskLevel(string likelihood, string magnitude)
        {
            var likelihoodLevel = Db.RiskLevels.Where(a => a.Id == likelihood).Single();
            var magnitudeLevel = Db.RiskLevels.Where(a => a.Id == magnitude).Single();

            var riskLevel = CalculateRiskLevel(likelihoodLevel, magnitudeLevel);

            return Json(new {LevelName = riskLevel.Name, Color = riskLevel.Color}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Calculates the final risk level of the entire Risk object
        /// </summary>
        /// <remarks>
        /// Calculation is defined in NIST specification paper in Section 3.7.1
        ///     Risk-Level Matrix Pg. 25
        /// </remarks>
        /// <returns></returns>
        private RiskLevel CalculateRiskLevel(RiskLevel likelihood, RiskLevel magnitude)
        {
            var riskCalc = likelihood.SLikelihood*magnitude.Impact;
            var level = string.Empty;

            if (50 < riskCalc && riskCalc <= 100)
            {
                level = ((char)RiskLevelsEnum.High).ToString();
            }
            if (10 < riskCalc && riskCalc <= 50)
            {
                level = ((char)RiskLevelsEnum.Medium).ToString();
            }
            if (1 <= riskCalc && riskCalc <= 10)
            {
                level = ((char) RiskLevelsEnum.Low).ToString();
            }

            return Db.RiskLevels.Where(a => a.Id == level).SingleOrDefault();
        }

        private void Validate(Risk risk, ModelStateDictionary modelState)
        {
            if (string.IsNullOrEmpty(risk.Name))
                modelState.AddModelError("Name", string.Format(Messages.Required, "Name"));

            if (string.IsNullOrEmpty(risk.Source))
                modelState.AddModelError("Threat Source", string.Format(Messages.Required, "Name"));

            if (string.IsNullOrEmpty(risk.Vulnerability))
                modelState.AddModelError("Vulnerability", string.Format(Messages.Required, "Name"));

            if (string.IsNullOrEmpty(risk.Likelihood.Id) && risk.Likelihood == null)
                modelState.AddModelError("Likelihood", string.Format(Messages.Required, "Likelihood"));

            if (string.IsNullOrEmpty(risk.Magnitude.Id) && risk.Magnitude == null)
                modelState.AddModelError("Magnitude", string.Format(Messages.Required, "Magnitude"));

            if (risk.Impact.Id <= 0 && risk.Impact == null)
                modelState.AddModelError("Impact", string.Format(Messages.Required, "Impact"));
        }
    }
}

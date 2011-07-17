using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
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

        /// <summary>
        /// Display for all risk assessment work for this module
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
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

        /// <summary>
        /// Add an identified risk
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        public ActionResult Add(int id, int projectId)
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

        /// <summary>
        /// Add an identified risk
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="risk">Risk</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(int id, int projectId, Risk risk, string likelihoodId, int impactId, string magnitudeId)
        {
            // validate access
            if (!_projectService.HasAccess(projectId, CurrentUserId))
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }

            // remove modelstate validation
            ModelState.Remove("risk.Project");
            ModelState.Remove("risk.SquareType");
            ModelState.Remove("risk.AssessmentType");

            // load all the objects
            var projectStep = Db.ProjectSteps.Include("Project").Include("Project.SecurityAssessmentType")
                                             .Include("Project.PrivacyAssessmentType").Include("Step")
                                             .Include("Step.SquareType").Where(a => a.Id == id).Single();
            var likelihood = Db.RiskLevels.Where(a => a.Id == likelihoodId).SingleOrDefault();
            var impact = Db.Impacts.Where(a => a.Id == impactId).SingleOrDefault();
            var magnitude = Db.RiskLevels.Where(a => a.Id == magnitudeId).SingleOrDefault();

            // set up the object
            if (likelihood == null) ModelState.AddModelError("Likelihood", "Likelihood is required.");
            if (impact == null) ModelState.AddModelError("Impact", "Impact is required.");
            if (magnitude == null) ModelState.AddModelError("Magnitude", "Magnitude is required.");

            if (ModelState.IsValid)
            {
                risk.Likelihood = likelihood;
                risk.Impact = impact;
                risk.Magnitude = magnitude;

                risk.Project = projectStep.Project;
                risk.SquareType = projectStep.Step.SquareType;
                risk.RiskLevel = CalculateRiskLevel(likelihood, magnitude);

                if (projectStep.Step.SquareType.Name == SquareTypes.Security)
                {
                    risk.AssessmentType = projectStep.Project.SecurityAssessmentType;
                }
                else
                {
                    risk.AssessmentType = projectStep.Project.PrivacyAssessmentType;
                }

                Db.Risks.Add(risk);
                Db.SaveChanges();

                Message = string.Format(Messages.Saved, "Risk");
                return this.RedirectToAction(a => a.Index(id, projectId));
            }

            var viewModel = NIST800_30EditViewModel.Create(Db, _projectService, id, projectId, CurrentUserId, risk);
            return View(viewModel);
        }

        /// <summary>
        /// Edit an identified risk
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="riskId">Risk id to edit</param>
        /// <returns></returns>
        public ActionResult Edit(int id, int projectId, int riskId)
        {
            try
            {
                var risk = Db.Risks.Include("Likelihood").Include("Impact").Include("Magnitude").Where(a => a.Id == riskId).Single();

                var viewModel = NIST800_30EditViewModel.Create(Db, _projectService, id, projectId, CurrentUserId, risk);

                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        /// <summary>
        /// Edit the identified risk
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="riskId">Id of Risk</param>
        /// <param name="risk">Risk</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, int projectId, int riskId, Risk risk, string likelihoodId, int impactId, string magnitudeId)
        {
            // validate access
            if (!_projectService.HasAccess(projectId, CurrentUserId))
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }

            // remove modelstate validation
            ModelState.Remove("risk.Project");
            ModelState.Remove("risk.SquareType");
            ModelState.Remove("risk.AssessmentType");

            // load all the objects
            var srcRisk = Db.Risks.Where(a => a.Id == riskId).Single();
            var projectStep = Db.ProjectSteps.Include("Project").Include("Project.SecurityAssessmentType")
                                 .Include("Project.PrivacyAssessmentType").Include("Step")
                                 .Include("Step.SquareType").Where(a => a.Id == id).Single();
            var likelihood = Db.RiskLevels.Where(a => a.Id == likelihoodId).SingleOrDefault();
            var impact = Db.Impacts.Where(a => a.Id == impactId).SingleOrDefault();
            var magnitude = Db.RiskLevels.Where(a => a.Id == magnitudeId).SingleOrDefault();

            // set up the object
            if (likelihood == null) ModelState.AddModelError("Likelihood", "Likelihood is required.");
            if (impact == null) ModelState.AddModelError("Impact", "Impact is required.");
            if (magnitude == null) ModelState.AddModelError("Magnitude", "Magnitude is required.");

            // copy the fields that could have been edited
            srcRisk.Name = risk.Name;
            srcRisk.Source = risk.Source;
            srcRisk.Vulnerability = risk.Vulnerability;
            srcRisk.Likelihood = likelihood;
            srcRisk.Impact = impact;
            srcRisk.Magnitude = magnitude;
            srcRisk.RiskLevel = CalculateRiskLevel(likelihood, magnitude);

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
                level = RiskLevels.High;
            }
            if (10 < riskCalc && riskCalc <= 50)
            {
                level = RiskLevels.Medium;
            }
            if (1 <= riskCalc && riskCalc <= 10)
            {
                level = RiskLevels.Low;
            }

            return Db.RiskLevels.Where(a => a.Id == level).SingleOrDefault();
        }
    }
}

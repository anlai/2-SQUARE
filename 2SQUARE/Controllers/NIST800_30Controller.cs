using System;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;
using System.Linq;

namespace _2SQUARE.Controllers
{
    public class NIST800_30Controller : ApplicationController
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
                var viewModel = NIST800_30ViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);

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
            return View();
        }

        public JsonResult DetermineRiskLevel(string likelihood, string magnitude)
        {
            var likelihoodLevel = Db.RiskLevels.Where(a => a.id == likelihood).Single();
            var magnitudeLevel = Db.RiskLevels.Where(a => a.id == magnitude).Single();

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

            return Db.RiskLevels.Where(a => a.id == level).SingleOrDefault();
        }
    }
}

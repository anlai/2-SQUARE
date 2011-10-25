using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    public class GenericAssessmentController : ApplicationController, IProcedureController
    {
        private readonly IProjectService _projectService;

        public GenericAssessmentController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Index(int id, int projectId)
        {
            try
            {
                var viewModel = GenericAssessmentViewModel.Create(_projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }

        public ActionResult Create(int id, int projectId)
        {
            try
            {
                var viewModel = GenericRiskViewModel.Create(_projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
            
        }

        [HttpPost]
        public ActionResult Create(int id, int projectId, Risk risk, string riskLevelId)
        {
            if (!Db.RiskLevels.Any(a=>a.Id == riskLevelId)) ModelState.Remove("risk.RiskLevel");
            ModelState.Remove("risk.Project");
            ModelState.Remove("risk.SquareType");
            ModelState.Remove("risk.AssessmentType");

            if (ModelState.IsValid)
            {
                var step = Db.ProjectSteps.Include("Step").Include("Step.SquareType").Where(a => a.Id == id).Single();

                _projectService.CreateRisk(projectId, step.Step.SquareType.Id, CurrentUserId, risk.Name, risk.Source, risk.Vulnerability, riskLevelId);
                Message = "Risk created.";
                return this.RedirectToAction(a => a.Index(id, projectId));
            }
            
            var viewModel = GenericRiskViewModel.Create(_projectService, projectId, id, CurrentUserId, risk);
            return View(viewModel);
        }

        public ActionResult Edit(int id, int projectStepId)
        {
            try
            {
                var risk = Db.Risks.Include("Project").Include("RiskLevel").Where(a => a.Id == id).Single();
                var viewModel = GenericRiskViewModel.Create(_projectService, risk.Project.Id, projectStepId, CurrentUserId, risk);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, int projectStepId, Risk risk, string riskLevelId)
        {
            if (!Db.RiskLevels.Any(a => a.Id == riskLevelId)) ModelState.Remove("risk.RiskLevel");
            ModelState.Remove("risk.Project");
            ModelState.Remove("risk.SquareType");
            ModelState.Remove("risk.AssessmentType");

            var riskToEdit = Db.Risks.Include("SquareType").Include("AssessmentType").Include("Project").Include("RiskLevel").Where(a => a.Id == id).Single();

            if (ModelState.IsValid)
            {
                var riskLevel = Db.RiskLevels.Where(a => a.Id == riskLevelId).SingleOrDefault();

                riskToEdit.Name = risk.Name;
                riskToEdit.Source = risk.Source;
                riskToEdit.Vulnerability = risk.Vulnerability;
                riskToEdit.RiskLevel = Db.RiskLevels.Where(a => a.Id == riskLevelId).Single();

                Db.SaveChanges();
                
                return this.RedirectToAction(a => a.Index(projectStepId, riskToEdit.Project.Id));
            }

            var viewModel = GenericRiskViewModel.Create(_projectService, risk.Project.Id, projectStepId, CurrentUserId, riskToEdit);
            return View(viewModel);
        }

        public RedirectToRouteResult Delete(int id, int projectStepid)
        {
            var risk = Db.Risks.Include("Project").Where(a => a.Id == id).Single();

            var projectId = risk.Project.Id;

            Db.Risks.Remove(risk);
            Db.SaveChanges();
            Message = "Risk deleted.";
            return this.RedirectToAction(a => a.Index(projectStepid, projectId));
        }
    }

    public class GenericAssessmentViewModel : ViewModelBase
    {
        public IEnumerable<Risk> Risks { get; set; }

        public static GenericAssessmentViewModel Create(IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            var viewModel = new GenericAssessmentViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);
            viewModel.Risks = viewModel.Project.Risks;

            return viewModel;
        }
    }

    public class GenericRiskViewModel : ViewModelBase
    {
        public Risk Risk { get; set; }
        public IEnumerable<RiskLevel> RiskLevels { get; set; }

        public static GenericRiskViewModel Create(IProjectService projectService, int projectId, int projectStepId, string userId, Risk risk = null)
        {
            var context = new SquareContext();

            var viewModel = new GenericRiskViewModel() {Risk = risk ?? new Risk(), RiskLevels = context.RiskLevels.ToList()};
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);

            return viewModel;
        }
    }
}

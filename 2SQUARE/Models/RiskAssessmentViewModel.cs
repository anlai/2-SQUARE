using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class RiskAssessmentViewModel : ViewModelBase
    {
        public IEnumerable<Risk> Risks { get; set; }

        public static RiskAssessmentViewModel Create(SquareContext db, IProjectService projectService, int projectStepId, int projectId, string userId)
        {
            Check.Require(db != null, "db is required.");

            var projectStep = projectService.GetProjectStep(projectStepId, userId);

            var viewModel = new RiskAssessmentViewModel()
            {
                ProjectStep = projectStep,
                Project = projectService.GetProject(projectId, userId),
                Risks = db.Risks.Include("Likelihood").Include("Impact").Include("Magnitude").Include("RiskLevel")
                                .Where(a => a.Project.Id == projectStep.Project.Id 
                                            && a.SquareType.Id == projectStep.Step.SquareType.Id)
                                            .OrderByDescending(a=>a.RiskLevel.Order).ToList()
            };

            return viewModel;
        }
    }
}
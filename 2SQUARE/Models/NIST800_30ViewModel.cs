using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class NIST800_30ViewModel : StepViewModelBase
    {
        public IEnumerable<Risk> Risks { get; set; }

        public static NIST800_30ViewModel Create(SquareEntities db, IProjectService projectService, int projectStepId, int projectId, string userId)
        {
            Check.Require(db != null, "db is required.");

            var projectStep = projectService.GetProjectStep(projectStepId, userId);

            var viewModel = new NIST800_30ViewModel()
                                {
                                    ProjectStep = projectStep,
                                    Project = projectService.GetProject(projectId, userId),
                                    Risks = db.Risks.Where(a=>a.ProjectId == projectStep.ProjectId && a.SsquareTypeId == projectStep.Step.SquareTypeId).ToList()
                                };

            return viewModel;
        }
    }
}
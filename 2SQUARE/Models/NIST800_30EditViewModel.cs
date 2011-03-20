using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Helpers;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class NIST800_30EditViewModel : StepViewModelBase
    {
        public IEnumerable<RiskLevel> RiskLevels { get; set; }
        public IEnumerable<Impact> Impacts { get; set; }
        public Risk Risk { get; set; }

        public string RiskLevelColor { get; set; }

        public static NIST800_30EditViewModel Create(SquareEntities db, IProjectService projectService, int projectStepId, int projectId, string userId, Risk risk = null)
        {
            Check.Require(db != null, "db is required.");

            var viewModel = new NIST800_30EditViewModel()
                                {
                                    ProjectStep = projectService.GetProjectStep(projectStepId, userId),
                                    Project = projectService.GetProject(projectId, userId),
                                    RiskLevels = db.RiskLevels.OrderBy(a=>a.Order).ToList(),
                                    Impacts = db.Impacts.ToList(),
                                    Risk = risk ?? new Risk(),
                                    RiskLevelColor = string.Empty
                                };

            // figure out the risk level color, if not null
            if (risk != null)
            {
                viewModel.RiskLevelColor = risk.RiskLevel != null ? risk.RiskLevel.Color : string.Empty;
            }

            Check.Ensure(viewModel.Risk.ProjectId == viewModel.Project.id, "Risk does not belong to the intended project.");

            return viewModel;
        }
    }
}
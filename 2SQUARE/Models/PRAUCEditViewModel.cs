using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class PRAUCEditViewModel : StepViewModelBase
    {
        public Risk Risk { get; set; }
        public IEnumerable<RiskLevel> RiskLevels { get; set; }

        public static PRAUCEditViewModel Create(SquareEntities db, IProjectService projectService, int projectStepId, int projectId, string userId, Risk risk = null)
        {
            var viewModel = new PRAUCEditViewModel()
                                {
                                    ProjectStep = projectService.GetProjectStep(projectStepId, userId),
                                    Project = projectService.GetProject(projectId, userId),
                                    RiskLevels = db.RiskLevels.OrderBy(a=>a.Order).ToList(),
                                    Risk = risk ?? new Risk()
                                };

            Check.Ensure(viewModel.Risk.ProjectId == viewModel.Project.id, "Risk does not belong to the intended project.");

            return viewModel;
        }
    }
}
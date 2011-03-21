using System.Collections.Generic;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Services;
using DesignByContract;
using System.Linq;

namespace _2SQUARE.Models
{
    public class Step5ViewModel : StepViewModelBase
    {
        public IEnumerable<ElicitationType> ElicitationTypes { get; set; }

        public static Step5ViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");
            Check.Require(userId != null, "userId is required.");

            var project = projectService.GetProject(projectId, userId);
            var projectStep = projectService.GetProjectStep(projectStepId, userId);

            Check.Ensure(project.id == projectStep.ProjectId, Messages.ProjectStepMismatch);

            var viewModel = new Step5ViewModel()
                                {
                                    Project = project, ProjectStep = projectStep,
                                    ElicitationTypes = db.ElicitationTypes.Where(a=>a.SquareTypeId == projectStep.Step.SquareTypeId).ToList()
                                };

            return viewModel;
        }
    }
}
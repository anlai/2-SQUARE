using System.Collections.Generic;
using System.Linq;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class Step4ViewModel : StepViewModelBase
    {
        public IEnumerable<AssessmentType> AssessmentTypes { get; set; }

        public static Step4ViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(db != null, "db is required.");

            var project = projectService.GetProject(projectId, userId);
            var projectStep = db.ProjectSteps.Where(a => a.Id == projectStepId).Single();

            Check.Require(project.id == projectStep.ProjectId, Messages.ProjectStepMismatch);

            var viewModel = new Step4ViewModel()
                                {
                                    Project = project,
                                    ProjectStep = projectStep,
                                    AssessmentTypes = db.AssessmentTypes.Where(a=>a.SquareTypeId == projectStep.Step.SquareTypeId)
                                };

            return viewModel;
        }
    }
}
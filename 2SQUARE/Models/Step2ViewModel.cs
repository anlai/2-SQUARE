using System.Collections.Generic;
using System.Linq;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Helpers;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class Step2ViewModel : StepViewModelBase
    {
        // needed for security
        public Goal BusinessGoal { get; set; }
        public List<Goal> SecurityGoals { get; set; }
        
        // needed for privacy
        public List<Goal> PrivacyGoals { get; set; }
        public List<Goal> Assets { get; set; }

        public static Step2ViewModel Create(SquareEntities db, IProjectService projectService, int projectStepId, int projectId, string loginId)
        {
            Check.Require(db != null, "Repository is required.");
            Check.Require(projectService != null, "projectService is required.");
            Check.Require(loginId != null, "loginId is required.");

            var project = projectService.GetProject(projectId, loginId);
            var projectStep = db.ProjectSteps.Where(a => a.Id == projectStepId).Single();

            Check.Ensure(project.id == projectStep.ProjectId, Messages.ProjectStepMismatch);

            var viewModel = new Step2ViewModel()
                                {
                                    Project = project,
                                    ProjectStep = projectStep
                                };

            if (projectStep.Step.SquareType.Name == SquareTypes.Security)
            {
                // load the business goal
                viewModel.BusinessGoal = project.Goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Business).ToString()).SingleOrDefault();

                // load the security goals
                viewModel.SecurityGoals = project.Goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Security).ToString()).ToList();
            }
            else if (projectStep.Step.SquareType.Name == SquareTypes.Privacy)
            {
                viewModel.PrivacyGoals = project.Goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Privacy).ToString()).ToList();
                viewModel.Assets = project.Goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Asset).ToString()).ToList();
            }

            return viewModel;
        }
    }
}
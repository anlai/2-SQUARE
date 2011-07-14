using System.Collections.Generic;
using System.Linq;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Helpers;
using _2SQUARE.Services;
using DesignByContract;
using Resources;

namespace _2SQUARE.Models
{
    public class Step2ViewModel : ViewModelBase
    {
        // needed for security
        public Goal BusinessGoal { get; set; }
        public List<Goal> SecurityGoals { get; set; }
        
        // needed for privacy
        public List<Goal> PrivacyGoals { get; set; }
        public List<Goal> Assets { get; set; }

        public static Step2ViewModel Create(SquareContext db, IProjectService projectService, int projectStepId, int projectId, string loginId)
        {
            Check.Require(db != null, "Repository is required.");

            var viewModel = new Step2ViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, loginId);

            if (viewModel.ProjectStep.Step.SquareType.Name == SquareTypes.Security)
            {
                // load the business goal
                viewModel.BusinessGoal = viewModel.Project.Goals.Where(a => a.GoalType.Id == GoalTypes.Business).SingleOrDefault();

                // load the security goals
                viewModel.SecurityGoals = viewModel.Project.Goals.Where(a => a.GoalType.Id == GoalTypes.Security).ToList();
            }
            else if (viewModel.ProjectStep.Step.SquareType.Name == SquareTypes.Privacy)
            {
                viewModel.PrivacyGoals = viewModel.Project.Goals.Where(a => a.GoalType.Id == GoalTypes.Privacy).ToList();
                viewModel.Assets = viewModel.Project.Goals.Where(a => a.GoalType.Id == GoalTypes.Asset).ToList();
            }

            return viewModel;
        }
    }
}
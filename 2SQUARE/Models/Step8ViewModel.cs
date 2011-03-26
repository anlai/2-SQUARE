using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class Step8ViewModel : ViewModelBase
    {
        public IEnumerable<Requirement> Requirements { get; set; }

        public static Step8ViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");

            var viewModel = new Step8ViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);

            viewModel.Requirements = viewModel.Project.Requirements.Where(a => a.SquareTypeId == viewModel.ProjectStep.Step.SquareTypeId).ToList();

            return viewModel;
        }
    }
}
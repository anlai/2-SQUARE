using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class Step9ViewModel : ViewModelBase
    {
        public static Step9ViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");

            var viewModel = new Step9ViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);

            return viewModel;
        }
    }
}
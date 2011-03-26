using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class Step7ViewModel : ViewModelBase
    {
        public static Step7ViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");

            var viewModel = new Step7ViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);

            return viewModel;
        }
    }
}
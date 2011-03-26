using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class RequirementViewModel : ViewModelBase
    {
        public Requirement Requirement { get; set; }

        public static RequirementViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string userId, Requirement requirement = null)
        {
            Check.Require(db != null, "db is required.");

            var viewModel = new RequirementViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);
            viewModel.Requirement = requirement ?? new Requirement();

            return viewModel;
        }
    }
}
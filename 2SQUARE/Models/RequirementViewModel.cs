using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class RequirementViewModel : ViewModelBase
    {
        public Requirement Requirement { get; set; }

        public static RequirementViewModel Create(SquareContext db, IProjectService projectService, int projectId, int projectStepId, string userId, Requirement requirement = null)
        {
            Check.Require(db != null, "db is required.");

            var viewModel = new RequirementViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);
            viewModel.Requirement = requirement ?? new Requirement(){RequirementId = string.Format("R{0}", viewModel.Project.Requirements.Count + 1)};

            return viewModel;
        }
    }
}
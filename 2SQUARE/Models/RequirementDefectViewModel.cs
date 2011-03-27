using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class RequirementDefectViewModel : ViewModelBase
    {
        public Requirement Requirement { get; set; }
        public RequirementDefect RequirementDefect { get; set; }

        public static RequirementDefectViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string userId, Requirement requirement, RequirementDefect requirementDefect = null)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");
            Check.Require(requirement != null, "requirement is required.");

            var viewModel = new RequirementDefectViewModel(){Requirement = requirement, RequirementDefect = requirementDefect ?? new RequirementDefect()};
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);

            return viewModel;
        }
    }
}
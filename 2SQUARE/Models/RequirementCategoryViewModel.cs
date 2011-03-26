using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class RequirementCategoryViewModel : ViewModelBase
    {
        public Requirement Requirement { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public static RequirementCategoryViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string userId, Requirement requirement)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");
            Check.Require(requirement != null, "requirement is required.");

            var viewModel = new RequirementCategoryViewModel() {Requirement = requirement};
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);

            viewModel.Categories = viewModel.Project.Categories.Where(a => a.SquareTypeId == viewModel.ProjectStep.Step.SquareTypeId).ToList();

            return viewModel;
        }
    }
}

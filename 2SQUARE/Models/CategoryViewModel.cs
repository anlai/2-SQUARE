using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class CategoryViewModel : ViewModelBase
    {
        public IEnumerable<Category> Categories { get; set; }

        public static CategoryViewModel Create(SquareContext db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(projectService != null, "projectService is required.");

            var viewModel = new CategoryViewModel() {};
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);

            // extract the categoriest from the project
            viewModel.Categories = db.Categories.Where(a => a.SquareType == viewModel.ProjectStep.Step.SquareType && a.Project.Id == projectId).ToList();

            return viewModel;
        }
    }

    public class CategoryEditViewModel : ViewModelBase
    {
        public Category Category { get; set; }

        public static CategoryEditViewModel Create(SquareContext db, IProjectService projectService, int projectId, int projectStepId, string userId, Category category = null)
        {
            Check.Require(projectService != null, "projectService is required.");

            var viewModel = new CategoryEditViewModel() { Category = category ?? new Category()};
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);

            return viewModel;
        }
    }
}
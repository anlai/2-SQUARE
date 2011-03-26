using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class Step7ViewModel : ViewModelBase
    {
        public IEnumerable<Requirement> CategorizedRequirements { get; set; }
        public IEnumerable<Requirement> UncategorizedRequirements { get; set; }

        public static Step7ViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");

            var viewModel = new Step7ViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);

            viewModel.CategorizedRequirements = db.Requirements.Where(a => a.Category != null && a.SquareTypeId == viewModel.ProjectStep.Step.SquareTypeId && a.ProjectId == projectId).ToList();
            viewModel.UncategorizedRequirements = db.Requirements.Where(a => a.Category == null && a.SquareTypeId == viewModel.ProjectStep.Step.SquareTypeId && a.ProjectId == projectId).ToList();

            return viewModel;
        }
    }
}
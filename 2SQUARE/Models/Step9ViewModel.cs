using System.Collections.Generic;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;
using System.Linq;

namespace _2SQUARE.Models
{
    public class Step9ViewModel : ViewModelBase
    {
        public IEnumerable<Requirement> Requirements { get; set; }

        public static Step9ViewModel Create(SquareContext db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");

            var viewModel = new Step9ViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);
            viewModel.Requirements =
                db.Requirements.Where(
                    a => a.Project.Id == projectId && a.SquareType == viewModel.ProjectStep.Step.SquareType).ToList();

            return viewModel;
        }
    }
}
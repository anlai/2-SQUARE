using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class Step8ViewModel : ViewModelBase
    {
        public IEnumerable<Requirement> Requirements { get; set; }

        public static Step8ViewModel Create(SquareContext db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");

            var viewModel = new Step8ViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);

            viewModel.Requirements = db.Requirements.Include("SquareType").Include("Category").Where(a => a.Project.Id == projectId && a.SquareType.Id == viewModel.ProjectStep.Step.SquareType.Id).ToList();

            return viewModel;
        }
    }
}
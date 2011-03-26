using System.Collections.Generic;
using _2SQUARE.Services;
using DesignByContract;
using System.Linq;

namespace _2SQUARE.Models
{
    public class GenericElicitationViewModel : ViewModelBase
    {
        public IEnumerable<Requirement> Requirements { get; set; }

        public static GenericElicitationViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(db != null, "db is required.");

            var viewModel = new GenericElicitationViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);
            viewModel.Requirements = viewModel.Project.Requirements.Where(a => a.SquareTypeId == viewModel.ProjectStep.Step.SquareTypeId).ToList();

            return viewModel;
        }
    }
}
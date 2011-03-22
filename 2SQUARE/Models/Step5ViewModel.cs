using System.Collections.Generic;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Services;
using DesignByContract;
using System.Linq;

namespace _2SQUARE.Models
{
    public class Step5ViewModel : StepViewModelBase
    {
        public IEnumerable<ElicitationType> ElicitationTypes { get; set; }

        public static Step5ViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(db != null, "db is required.");

            var viewModel = new Step5ViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);
            viewModel.ElicitationTypes = db.ElicitationTypes.Where(a => a.SquareTypeId == viewModel.ProjectStep.Step.SquareTypeId).ToList();

            return viewModel;
        }
    }
}
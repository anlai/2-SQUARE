using System.Collections.Generic;
using System.Linq;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class Step4ViewModel : ViewModelBase
    {
        public IEnumerable<AssessmentType> AssessmentTypes { get; set; }

        public static Step4ViewModel Create(SquareContext db, IProjectService projectService, int projectId, int projectStepId, string userId)
        {
            Check.Require(db != null, "db is required.");

            var viewModel = new Step4ViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);
            viewModel.AssessmentTypes = db.AssessmentTypes.Include("SquareType").Where(a => a.SquareType.Id == viewModel.ProjectStep.Step.SquareType.Id);
            return viewModel;
        }
    }
}
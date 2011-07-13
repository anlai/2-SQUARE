using System.Collections.Generic;
using System.Linq;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Filters;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class Step1ViewModel : ViewModelBase
    {
        public IEnumerable<ProjectTerm> ProjectTerms { get; set; }

        public bool ProjectManager { get; set; }
        public bool Stakeholder { get; set; }
        public bool RequirementsEngineer { get; set; }

        public static Step1ViewModel Create(SquareContext db, IProjectService projectService, int projectStepId, int projectId, string loginId)
        {
            Check.Require(db != null, "Square Entities is required.");

            var viewModel = new Step1ViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, loginId);

            var projectTerms = db.ProjectTerms.Where(a => a.Project.Id == projectId && a.SquareType.Id == viewModel.ProjectStep.Step.SquareType.Id).ToList();
            viewModel.ProjectTerms = projectTerms;

            var roles = projectService.UserRoles(projectId, loginId);
            viewModel.ProjectManager = roles.Contains(RoleNames.RoleProjectManager);
            viewModel.Stakeholder = roles.Contains(RoleNames.RoleStakeholder);
            viewModel.RequirementsEngineer = roles.Contains(RoleNames.RoleRequirementsEngineer);

            return viewModel;
        }
    }
}
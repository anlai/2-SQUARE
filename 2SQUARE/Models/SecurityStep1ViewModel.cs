using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Filters;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class SecurityStep1ViewModel
    {
        public Step Step { get; set; }
        public Project Project { get; set; }
        public IEnumerable<ProjectTerm> ProjectTerms { get; set; }

        public bool ProjectManager { get; set; }
        public bool Stakeholder { get; set; }
        public bool RequirementsEngineer { get; set; }

        public static SecurityStep1ViewModel Create(SquareEntities db, IProjectService projectService, int stepId, int projectId, string loginId)
        {
            var viewModel = new SecurityStep1ViewModel()
                                {
                                    Step = db.Steps.Where(a=>a.id == stepId && a.Order == 1).Single(),
                                    Project = db.Projects.Where(a=>a.id == projectId).Single()
                                };

            var projectTerms = db.ProjectTerms.Where(a => a.ProjectId == projectId && a.SquareTypeId == viewModel.Step.SquareTypeId).ToList();
            viewModel.ProjectTerms = projectTerms;

            var roles = projectService.UserRoles(projectId, loginId);
            viewModel.ProjectManager = roles.Contains(RoleNames.RoleProjectManager);
            viewModel.Stakeholder = roles.Contains(RoleNames.RoleStakeholder);
            viewModel.RequirementsEngineer = roles.Contains(RoleNames.RoleRequirementsEngineer);

            return viewModel;
        }
    }
}
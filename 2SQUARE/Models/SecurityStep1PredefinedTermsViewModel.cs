using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Services;

namespace _2SQUARE.Models
{
    public class SecurityStep1PredefinedTermsViewModel
    {
        public Step Step { get; set; }
        public Project Project { get; set; }
        public IEnumerable<Term> PendingTerms { get; set; }

        public static SecurityStep1PredefinedTermsViewModel Create(SquareEntities db, IProjectService projectService, int stepId, int projectId, string loginId)
        {
            var viewModel = new SecurityStep1PredefinedTermsViewModel()
                                {
                                    Step = db.Steps.Where(a => a.id == stepId && a.Order == 1).Single(),
                                    Project = db.Projects.Where(a => a.id == projectId).Single()
                                };

            var projectTerms = db.ProjectTerms.Where(a => a.ProjectId == projectId && a.SquareTypeId == viewModel.Step.SquareTypeId).ToList();
            var pt = projectTerms.Select(a => a.Term).ToList();
            var pendingTerms = db.Terms.Where(a => !pt.Contains(a.Name) && a.IsActive).ToList();

            viewModel.PendingTerms = pendingTerms;

            return viewModel;
        }
    }
}
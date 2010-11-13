using System.Collections.Generic;
using System.Linq;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class SecurityStep1ViewModel
    {
        public Step Step { get; set; }
        public Project Project { get; set; }
        public IEnumerable<Term> PendingTerms { get; set; }
        public IEnumerable<ProjectTerm> ProjectTerms { get; set; }

        public static SecurityStep1ViewModel Create(SquareEntities db, int stepId, int projectId)
        {
            Check.Require(db != null, "Square Entity object is required.");

            var viewModel = new SecurityStep1ViewModel()
                                {
                                    Step = db.Steps.Where(a=>a.id == stepId && a.Order == 1).Single(),
                                    Project = db.Projects.Where(a=>a.id == projectId).Single()
                                };

            var projectTerms = db.ProjectTerms.Where(a => a.ProjectId == projectId).ToList();
            var pt = projectTerms.Select(a => a.Term).ToList();
            var pendingTerms = db.Terms.Where(a => !pt.Contains(a.Name) && a.IsActive).ToList();

            viewModel.PendingTerms = pendingTerms;
            viewModel.ProjectTerms = projectTerms;

            return viewModel;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class ProjectTermPredefinedTermsViewModel
    {
        public Step Step { get; set; }
        public Project Project { get; set; }
        public IEnumerable<Term> PredefinedTerms { get; set; }

        public static ProjectTermPredefinedTermsViewModel Create(SquareContext db, IProjectService projectService, int stepId, int projectId, string loginId)
        {
            Check.Require(db != null, "Square Entities is required.");
            Check.Require(projectService != null, "projectService is required.");
            Check.Require(loginId != null, "loginId is required.");

            var viewModel = new ProjectTermPredefinedTermsViewModel()
                                {
                                    Step = db.Steps.Where(a => a.Id == stepId && a.Order == 1).Single(),
                                    Project = db.Projects.Where(a => a.Id == projectId).Single()
                                };

            var projectTerms = db.ProjectTerms.Where(a => a.Project.Id == projectId && a.SquareType == viewModel.Step.SquareType).ToList();
            var pt = projectTerms.Select(a => a.Term).ToList();
            var predefinedTerms = db.Terms.Where(a => !pt.Contains(a.Name) && a.IsActive && a.SquareType == viewModel.Step.SquareType).ToList();

            viewModel.PredefinedTerms = predefinedTerms;

            return viewModel;
        }
    }
}
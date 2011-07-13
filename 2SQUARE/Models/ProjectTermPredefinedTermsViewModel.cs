using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class ProjectTermPredefinedTermsViewModel
    {
        public ProjectStep ProjectStep { get; set; }
        public Project Project { get; set; }
        public IEnumerable<Term> PredefinedTerms { get; set; }

        public static ProjectTermPredefinedTermsViewModel Create(SquareContext db, IProjectService projectService, int projectStepId, int projectId, string loginId)
        {
            Check.Require(db != null, "Square Entities is required.");
            Check.Require(projectService != null, "projectService is required.");
            Check.Require(loginId != null, "loginId is required.");

            var viewModel = new ProjectTermPredefinedTermsViewModel()
                                {
                                    ProjectStep = db.ProjectSteps.Include("Step").Include("Step.SquareType").Where(a => a.Id == projectStepId).Single(),
                                    Project = db.Projects.Where(a => a.Id == projectId).Single()
                                };

            // load the project step
            var projectStep = viewModel.ProjectStep;

            // load all terms used in project
            var projectTerms = db.ProjectTerms.Where(a => a.Project.Id == projectId && a.SquareType.Id == projectStep.Step.SquareType.Id).ToList();

            // load all terms
            var pt = projectTerms.Select(a => a.Term).ToList();

            // terms that have not been used
            var predefinedTerms = db.Terms.Include("Definitions").Where(a => !pt.Contains(a.Name) && a.IsActive && a.SquareType.Id == projectStep.Step.SquareType.Id).ToList();

            viewModel.PredefinedTerms = predefinedTerms;

            return viewModel;
        }
    }
}
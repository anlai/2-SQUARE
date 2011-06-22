using System.Linq;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class ProjectTermAddNewTermViewModel
    {
        public Step Step { get; set; }
        public ProjectTerm ProjectTerm { get; set; }
        
        public static ProjectTermAddNewTermViewModel Create(SquareEntities db, int stepId, ProjectTerm projectTerm)
        {
            Check.Require(db != null, "Square Entities is required.");
            Check.Require(projectTerm != null, "projectTerm is required.");

            var projectStep = db.ProjectSteps.Where(a => a.Id == stepId).Single();
            projectTerm.SquareType = projectStep.Step.SquareType;
            var viewModel = new ProjectTermAddNewTermViewModel() {Step = projectStep.Step, ProjectTerm = projectTerm};

            return viewModel;
        }
    }
}
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

            var step = db.Steps.Where(a => a.id == stepId).Single();
            projectTerm.SquareType = step.SquareType;
            var viewModel = new ProjectTermAddNewTermViewModel() {Step = step, ProjectTerm = projectTerm};

            return viewModel;
        }
    }
}
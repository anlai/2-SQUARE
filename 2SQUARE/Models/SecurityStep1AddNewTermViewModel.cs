using System.Linq;

namespace _2SQUARE.Models
{
    public class SecurityStep1AddNewTermViewModel
    {
        public Step Step { get; set; }
        public ProjectTerm ProjectTerm { get; set; }
        
        public static SecurityStep1AddNewTermViewModel Create(SquareEntities db, int stepId, ProjectTerm projectTerm)
        {
            var step = db.Steps.Where(a => a.id == stepId).Single();
            projectTerm.SquareType = step.SquareType;
            var viewModel = new SecurityStep1AddNewTermViewModel() {Step = step, ProjectTerm = projectTerm};

            return viewModel;
        }
    }
}
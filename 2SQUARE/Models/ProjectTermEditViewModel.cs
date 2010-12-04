using System.Collections.Generic;
using System.Linq;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class ProjectTermEditViewModel
    {
        public ProjectTerm ProjectTerm { get; set; }
        public IEnumerable<Definition> Definitions { get; set; }
        public int StepId { get; set; }

        public static ProjectTermEditViewModel Create(SquareEntities db, int projectTermId, int stepId)
        {
            Check.Require(db != null, "Square Entities is required.");

            var projectTerm = db.ProjectTerms.Where(a => a.id == projectTermId).Single();

            var viewModel = new ProjectTermEditViewModel()
                                {
                                    ProjectTerm = projectTerm,
                                    Definitions = db.Definitions.Where(a => a.Term.Name == projectTerm.Term).ToList(),
                                    StepId = stepId
                                };

            return viewModel;
        }
    }
}
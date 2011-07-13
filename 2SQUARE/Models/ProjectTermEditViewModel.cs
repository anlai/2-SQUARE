using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Core.Domain;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class ProjectTermEditViewModel
    {
        public ProjectTerm ProjectTerm { get; set; }
        public IEnumerable<Definition> Definitions { get; set; }
        public int ProjectStepId { get; set; }

        public static ProjectTermEditViewModel Create(SquareContext db, int projectTermId, int projectStepId)
        {
            Check.Require(db != null, "Square Entities is required.");

            var projectTerm = db.ProjectTerms.Include("Project").Include("SquareType").Where(a => a.Id == projectTermId).Single();

            var viewModel = new ProjectTermEditViewModel()
                                {
                                    ProjectTerm = projectTerm,
                                    Definitions = db.Definitions.Where(a => a.Term.Name == projectTerm.Term).ToList(),
                                    ProjectStepId = projectStepId
                                };

            return viewModel;
        }
    }
}
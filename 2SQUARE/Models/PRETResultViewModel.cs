using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Core.PRET;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class PRETResultViewModel : ViewModelBase
    {
        public IEnumerable<PRETLaw> PretLaws { get; set; }
        public IEnumerable<PRETRequirement> PretRequirements { get; set; }

        public static PRETResultViewModel Create(SquareContext db, IProjectService projectService, int projectId, int projectStepId, string userId, int[] laws)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");

            laws = laws ?? new int[0];

            var viewModel = new PRETResultViewModel()
                                {
                                    PretLaws = db.PRETLaws.Where(a=>laws.Contains(a.Id)).ToList(),
                                    PretRequirements = db.PretRequirements.Where(a=>laws.Contains(a.Law.Id)).ToList()
                                };

            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);
            return viewModel;
        }
    }
}
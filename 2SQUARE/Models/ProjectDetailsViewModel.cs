using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class ProjectDetailsViewModel
    {
        public Project Project { get; set; }
        public IEnumerable<SquareType> SquareTypes { get; set; }
        public IEnumerable<ProjectStep> ProjectSteps { get; set; }

        public static ProjectDetailsViewModel Create(SquareContext db, IProjectService projectService, int id, string loginId)
        {
            Check.Require(db != null, "SquareContext is required.");
            Check.Require(projectService != null, "Project service is required.");
            Check.Require(!string.IsNullOrEmpty(loginId), "login id is required.");

            var viewModel = new ProjectDetailsViewModel()
                                {
                                    Project = projectService.GetProject(id, loginId),
                                    SquareTypes = db.SquareTypes.ToList()
                                    //ProjectSteps = SquareContext.ProjectSteps.Where(a=>a.Project.id == id).ToList()
                                };

            return viewModel;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class GoalViewModel
    {
        public ProjectStep ProjectStep { get; set; }
        public Goal Goal { get; set; }
        public IEnumerable<GoalType> GoalTypes { get; set; }

        public static GoalViewModel Create(SquareContext db, IProjectService projectService, int projectStepId, string login, Goal goal = null)
        {
            Check.Require(db != null, "db is required.");

            var projectStep = projectService.GetProjectStep(projectStepId, login);

            if (goal == null)
            {
                goal = new Goal()
                           {
                               Project = projectStep.Project,
                               SquareType = projectStep.Step.SquareType
                           };
            }

            var viewModel = new GoalViewModel()
                                {
                                    ProjectStep = projectStep,
                                    Goal = goal,
                                    GoalTypes = db.GoalTypes.Include("SquareType").Where(a=>a.SquareType.Id == projectStep.Step.SquareType.Id).ToList()
                                };

            return viewModel;
        }
    }
}
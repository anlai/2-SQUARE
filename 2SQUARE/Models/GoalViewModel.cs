using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Core.Domain;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class GoalViewModel
    {
        public ProjectStep ProjectStep { get; set; }
        public Goal Goal { get; set; }
        public IEnumerable<GoalType> GoalTypes { get; set; }

        public static GoalViewModel Create(SquareContext db, int projectStepId, Goal goal = null)
        {
            Check.Require(db != null, "db is required.");

            var projectStep = db.ProjectSteps.Where(a => a.Id == projectStepId).Single();

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
                                    GoalTypes = db.GoalTypes.Where(a=>a.SquareType == projectStep.Step.SquareType).ToList()
                                };

            return viewModel;
        }
    }
}
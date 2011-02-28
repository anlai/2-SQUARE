using System.Collections.Generic;
using System.Linq;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class GoalViewModel
    {
        public ProjectStep ProjectStep { get; set; }
        public Goal Goal { get; set; }
        public IEnumerable<GoalType> GoalTypes { get; set; }

        public static GoalViewModel Create(SquareEntities db, int projectStepId, Goal goal = null)
        {
            Check.Require(db != null, "db is required.");

            var projectStep = db.ProjectSteps.Where(a => a.Id == projectStepId).Single();

            if (goal == null)
            {
                goal = new Goal()
                           {
                               ProjectId = projectStep.ProjectId,
                               SquareTypeId = projectStep.Step.SquareTypeId
                           };
            }

            var viewModel = new GoalViewModel()
                                {
                                    ProjectStep = projectStep,
                                    Goal = goal,
                                    GoalTypes = db.GoalTypes.Where(a=>a.SquareTypeId == projectStep.Step.SquareTypeId).ToList()
                                };

            return viewModel;
        }
    }
}
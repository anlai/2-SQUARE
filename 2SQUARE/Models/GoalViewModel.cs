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

            var projectStep = db.ProjectSteps
                                .Include("Project").Include("Step").Include("Step.SquareType")
                                .Where(a => a.Id == projectStepId).Single();

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
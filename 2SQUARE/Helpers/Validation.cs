using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Models;

namespace _2SQUARE.Helpers
{
    /// <summary>
    /// Validates objects are ready for persisting
    /// </summary>
    public class Validation
    {
        public static void Validate(ProjectTerm projectTerm, ModelStateDictionary modelState)
        {
            if (string.IsNullOrEmpty(projectTerm.Term)) modelState.AddModelError("Term", string.Format(Messages.Required, "Term"));
            if (string.IsNullOrEmpty(projectTerm.Definition)) modelState.AddModelError("Definition", string.Format( Messages.Required, "Definition"));
            if (string.IsNullOrEmpty(projectTerm.Source)) modelState.AddModelError("Source", string.Format(Messages.Required, "Source"));
        }

        public static void Validate(Goal goal, ModelStateDictionary modelState)
        {
            if (string.IsNullOrEmpty(goal.Description)) modelState.AddModelError("Description", string.Format(Messages.Required, "Description"));
            if (string.IsNullOrEmpty(goal.GoalTypeId) && goal.GoalType == null) modelState.AddModelError("Goal Type", string.Format(Messages.Required, "Goal Type"));
        }
    }
}
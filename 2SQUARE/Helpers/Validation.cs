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
            if (string.IsNullOrEmpty(projectTerm.Term)) modelState.AddModelError("Term", Required("Term"));
            if (string.IsNullOrEmpty(projectTerm.Definition)) modelState.AddModelError("Definition", Required("Definition"));
            if (string.IsNullOrEmpty(projectTerm.Source)) modelState.AddModelError("Source", Required("Source"));
        }

        public static void Validate(Goal goal, ModelStateDictionary modelState)
        {
            if (string.IsNullOrEmpty(goal.Description)) modelState.AddModelError("Description", Required("Description"));
            if (string.IsNullOrEmpty(goal.GoalTypeId) && goal.GoalType == null) modelState.AddModelError("Goal Type", Required("Goal Type"));
        }

        public static void Validate(Artifact artifact, ModelStateDictionary modelState)
        {
            if (string.IsNullOrEmpty(artifact.Name)) modelState.AddModelError("Name", Required("Name"));
            if (artifact.ArtifactType == null && artifact.ArtifactTypeId <= 0) modelState.AddModelError("Artifact Type", Required("Artifact Type"));
        }

        public static void Validate(RiskRecommendation riskRecommendation, ModelStateDictionary modelState)
        {
            if (string.IsNullOrEmpty(riskRecommendation.Controls)) modelState.AddModelError("Controls", Required("Controls"));
        }

        public static void Validate(Requirement requirement, ModelStateDictionary modelState)
        {
            if (string.IsNullOrWhiteSpace(requirement.Name)) modelState.AddModelError("Name", Required("Name"));
            if (string.IsNullOrWhiteSpace(requirement.Requirement1)) modelState.AddModelError("Requirement", Required("Requirement"));

            if (requirement.Project == null && requirement.ProjectId<=0) modelState.AddModelError("Project", Required("Project"));
            if (requirement.SquareType == null && requirement.SquareTypeId <=0) modelState.AddModelError("Square Type", Required("Square Type"));
        }

        public static void Validate(Category category, ModelStateDictionary modelState)
        {
            if (string.IsNullOrEmpty(category.Name)) modelState.AddModelError("Name", Required("Name"));
        }

        public static void Validate(RequirementDefect defect, ModelStateDictionary modelState)
        {
            if (string.IsNullOrWhiteSpace(defect.Description)) modelState.AddModelError("Description", Required("Description"));
        }

        private static string Required(string field)
        {
            return string.Format(Messages.Required,field);
        }
    }
}
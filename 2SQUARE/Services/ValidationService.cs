using System;
using System.Collections.Generic;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using DesignByContract;
using System.Linq;

public class ValidationService : IValidationService
{
    private readonly IProjectService _projectService;

    public ValidationService(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public ValidationChangeStatusResult ValidateChangeStatus(ProjectStep projectStep, bool complete = false, bool working = false)
    {
        Check.Require(projectStep != null, "projectStep is required.");
        Check.Require(complete || working, "The status must be moved to either working or complete status.");

        var result = new ValidationChangeStatusResult();

        switch (projectStep.Step.Order)
        {
            case 1:
                result.IsValid = working ? Step1Start(projectStep, result.Warnings, result.Errors) : 
                        (complete ? Step1Complete(projectStep, result.Warnings, result.Errors) : false
                        );
                break;
            case 2:
                result.IsValid = working ? Step2Start(projectStep, result.Warnings, result.Errors) :
                        (complete ? Step2Complete(projectStep, result.Warnings,  result.Errors) : false
                        );
                break;
            case 3:
                result.IsValid = working ? Step3Start(projectStep, result.Warnings, result.Errors) :
                        (complete ? Step3Complete(projectStep, result.Warnings, result.Errors) : false
                        );
                break;
            case 4:
                result.IsValid = working ? Step4Start(projectStep, result.Warnings, result.Errors) :
                        (complete ? Step4Complete(projectStep, result.Warnings, result.Errors) : false
                        );
                break;
            case 5:
                result.IsValid = working ? Step5Start(projectStep, result.Warnings, result.Errors) :
                        (complete ? Step5Complete(projectStep, result.Warnings, result.Errors) : false
                        );
                break;
            case 6:
                result.IsValid = working ? Step6Start(projectStep, result.Warnings, result.Errors) :
                        (complete ? Step6Complete(projectStep, result.Warnings, result.Errors) : false
                        );
                break;
            case 7:
                result.IsValid = working ? Step7Start(projectStep, result.Warnings, result.Errors) :
                        (complete ? Step7Complete(projectStep, result.Warnings, result.Errors) : false
                        );
                break;
            case 8:
                result.IsValid = working ? Step8Start(projectStep, result.Warnings, result.Errors) :
                        (complete ? Step8Complete(projectStep, result.Warnings, result.Errors) : false
                        );
                break;
            case 9:
                result.IsValid = working ? Step9Start(projectStep, result.Warnings, result.Errors) :
                        (complete ? Step9Complete(projectStep, result.Warnings, result.Errors) : false
                        );
                break;
            default:
                result = null;  // an invalid step was passed
                break;
        }

        Check.Require(result != null, "Result cannot be null, an invalid project step was passed to validation.");

        return result;
    }
    #region Validate Change Status Helpers
    /// <summary>
    /// Step 1 starting validation
    /// </summary>
    /// <remarks>
    /// There are no preconditions to get step 1 started.
    /// </remarks>
    /// <param name="projectStep"></param>
    /// <param name="warnings"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    private bool Step1Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        Check.Require(projectStep != null, "projectStep is required.");

        if (projectStep.Project.ProjectSteps.Any(a => a.DateStarted.HasValue))
        {
            warnings.Add("Other steps are active, making this step active may prevent you from completing or working on other steps.");
        }

        return true;
    }

    /// <summary>
    /// Step 1 completion validation
    /// </summary>
    /// <remarks>
    /// Validation for step 1 of Privacy and Security are the same, there is an agreed upon
    /// list of terms
    /// </remarks>
    /// <param name="projectStep"></param>
    /// <param name="warnings"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    private bool Step1Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        Check.Require(projectStep != null, "projectStep is required.");
        Check.Require(warnings != null, "warnings is required.");
        Check.Require(errors != null, "errors is required.");

        // this is the only exit criteria, there are a set of terms
        if (projectStep.Project.ProjectTerms.Where(a => a.SquareTypeId == projectStep.Step.SquareTypeId).Count() <= 0)
        {
            errors.Add(string.Format("There are no definitions for {0} in this project.", projectStep.Step.SquareType.Name));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Step 2 starting validation
    /// </summary>
    /// <remarks>
    /// This step has the prereq that step 1 is completed
    /// </remarks>
    /// <param name="projectStep"></param>
    /// <param name="warnings"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    private bool Step2Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        Check.Require(projectStep != null, "projectStep is required.");
        Check.Require(warnings != null, "warnings is required.");
        Check.Require(errors != null, "errors is required.");

        // check for step 1's completion
        var steps = _projectService.GetProjectSteps(projectStep.ProjectId, projectStep.Step.SquareType);
        if (steps.Where(a => a.Step.Order == 1 && a.Complete).Any())
        {
            return true;
        }
        
        // step 1 is not completed
        errors.Add("Step 1 has not been completed.");

        return false;
    }

    /// <summary>
    /// Step 2 completion validation
    /// </summary>
    /// <remarks>
    /// For security step 2 is required to have a business process and one or more security goals.
    /// For privacy step 2 is required to have one or more privacy goals
    /// </remarks>
    /// <param name="projectStep"></param>
    /// <param name="warnings"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    private bool Step2Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        Check.Require(projectStep != null, "projectStep is required.");
        Check.Require(warnings != null, "warnings is required.");
        Check.Require(errors != null, "errors is required.");

        // check the goals
        var goals = projectStep.Project.Goals;

        // deal with security checking
        if (projectStep.Step.SquareType.Name == SquareTypes.Security)
        {
            // check for business goal
            if (!goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Business).ToString()).Any())
            {
                errors.Add("No business goal was found.");
            }

            // check for security goal
            if (!goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Security).ToString()).Any())
            {
                errors.Add("No security goals were found.");
            }

            // no errors approve the change
            if (errors.Count == 0) return true;
            
            // there is at least one problem
            return false;
        }
        
        // deal with privacy
        if (projectStep.Step.SquareType.Name == SquareTypes.Privacy)
        {
            if (!goals.Where(a => a.GoalTypeId == ((char)GoalTypes.Privacy).ToString()).Any())
            {
                errors.Add("No privacy goals were found.");
                return false;
            }

            // no errors approve the change
            return true;
        }

        // default error, shouldn't hit this unless we added a new square type
        errors.Add("A project step with a square type other than security or privacy has been provided.");
        return false;
    }

    private bool Step3Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step3Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step4Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step4Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step5Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step5Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step6Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step6Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step7Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step7Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step8Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step8Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step9Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }

    private bool Step9Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        warnings.Add("Validation has not been added yet.");
        return false;
    }
    #endregion

    public ValidationChangeStatusResult ValidateCompletion(ProjectStep projectStep)
    {
        var result = new ValidationChangeStatusResult();

        switch (projectStep.Step.Order)
        {
            case 1: result.IsValid = Step1Complete(projectStep, result.Warnings, result.Errors);
                break;
            default:
                result.IsValid = false;
                result.Errors.Add("Validation has not been added for the step.");
                break;
        }

        return result;
    }

    public bool ValidateRoleAccess(List<string> userRoles, ProjectStep projectStep)
    {
        //todo: write in rules for each step on who can access
        throw new NotImplementedException();
    }


}
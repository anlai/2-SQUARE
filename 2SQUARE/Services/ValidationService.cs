﻿using System;
using System.Collections.Generic;
using _2SQUARE.Models;
using _2SQUARE.Services;
using DesignByContract;
using System.Linq;

public class ValidationService : IValidationService
{
    public ValidationResult Validate(ProjectStep projectStep, bool complete = false, bool working = false)
    {
        Check.Require(projectStep != null, "projectStep is required.");
        Check.Require(complete || working, "The status must be moved to either working or complete status.");

        var result = new ValidationResult();

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

    /// <summary>
    /// Step 1 starting validation
    /// </summary>
    /// <remarks>
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

        // this is the only exit criteria, there are a set of terms
        if (projectStep.Project.ProjectTerms.Where(a => a.SquareTypeId == projectStep.Step.SquareTypeId).Count() <= 0)
        {
            errors.Add("There are no definitions for this project.");
            return false;
        }

        return true;
    }

    private bool Step2Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step2Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step3Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step3Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step4Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step4Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step5Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step5Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step6Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step6Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step7Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step7Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step8Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step8Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step9Start(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }

    private bool Step9Complete(ProjectStep projectStep, List<string> warnings, List<string> errors)
    {
        throw new NotImplementedException();
    }
}
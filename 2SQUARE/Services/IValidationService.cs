﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using _2SQUARE.Models;

namespace _2SQUARE.Services
{
    public interface IValidationService
    {
        ValidationChangeStatusResult ValidateChangeStatus(ProjectStep projectStep, bool complete, bool working);

        ValidationChangeStatusResult ValidateCompletion(ProjectStep projectStep);
    }

    public class ValidationChangeStatusResult
    {

        public ValidationChangeStatusResult()
        {
            IsValid = false;
            Warnings = new List<string>();
            Errors = new List<string>();
            ChangeSteps = new List<KeyValuePair<int, bool>>();
        }

        public bool IsValid { get; set; }
        public List<string> Warnings { get; set; }
        public List<string> Errors { get; set; }

        public List<KeyValuePair<int, bool>> ChangeSteps { get; set; }
    }
}
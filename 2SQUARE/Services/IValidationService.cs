using System;
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
        RedirectToRouteResult ValidateForWork(ProjectStep projectStep, string login, Step step, Project project, out string message);
    }

    public class ValidationChangeStatusResult
    {
        public ValidationChangeStatusResult()
        {
            IsValid = false;
            Warnings = new List<string>();
            Errors = new List<string>();
        }

        public bool IsValid { get; set; }
        public List<string> Warnings { get; set; }
        public List<string> Errors { get; set; }
    }
}

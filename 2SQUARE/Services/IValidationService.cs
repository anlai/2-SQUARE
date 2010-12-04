using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2SQUARE.Models;

namespace _2SQUARE.Services
{
    public interface IValidationService
    {
        ValidationResult Validate(ProjectStep projectStep, bool complete, bool working);
    }

    public class ValidationResult
    {
        public ValidationResult()
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

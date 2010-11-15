﻿using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Services;

namespace _2SQUARE.Models
{
    public class ProjectTermPredefinedTermsViewModel
    {
        public Step Step { get; set; }
        public Project Project { get; set; }
        public IEnumerable<Term> PredefinedTerms { get; set; }

        public static ProjectTermPredefinedTermsViewModel Create(SquareEntities db, IProjectService projectService, int stepId, int projectId, string loginId)
        {
            var viewModel = new ProjectTermPredefinedTermsViewModel()
                                {
                                    Step = db.Steps.Where(a => a.id == stepId && a.Order == 1).Single(),
                                    Project = db.Projects.Where(a => a.id == projectId).Single()
                                };

            var projectTerms = db.ProjectTerms.Where(a => a.ProjectId == projectId && a.SquareTypeId == viewModel.Step.SquareTypeId).ToList();
            var pt = projectTerms.Select(a => a.Term).ToList();
            var predefinedTerms = db.Terms.Where(a => !pt.Contains(a.Name) && a.IsActive && a.SquareTypeId == viewModel.Step.SquareTypeId).ToList();

            viewModel.PredefinedTerms = predefinedTerms;

            return viewModel;
        }
    }
}
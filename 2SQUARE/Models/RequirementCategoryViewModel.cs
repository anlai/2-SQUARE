﻿using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class RequirementCategoryViewModel : ViewModelBase
    {
        public Requirement Requirement { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public static RequirementCategoryViewModel Create(SquareContext db, IProjectService projectService, int projectId, int projectStepId, string userId, Requirement requirement)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");
            Check.Require(requirement != null, "requirement is required.");

            var viewModel = new RequirementCategoryViewModel() {Requirement = requirement};
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, userId);

            viewModel.Categories = db.Categories.Where(a => a.SquareType.Id == viewModel.ProjectStep.Step.SquareType.Id && a.Project.Id == projectId).ToList();

            return viewModel;
        }
    }
}

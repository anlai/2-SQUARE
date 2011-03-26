﻿using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class RiskAssessmentViewModel : ViewModelBase
    {
        public IEnumerable<Risk> Risks { get; set; }

        public static RiskAssessmentViewModel Create(SquareEntities db, IProjectService projectService, int projectStepId, int projectId, string userId)
        {
            Check.Require(db != null, "db is required.");

            var projectStep = projectService.GetProjectStep(projectStepId, userId);

            var viewModel = new RiskAssessmentViewModel()
            {
                ProjectStep = projectStep,
                Project = projectService.GetProject(projectId, userId),
                Risks = db.Risks.Where(a => a.ProjectId == projectStep.ProjectId 
                                            && a.SsquareTypeId == projectStep.Step.SquareTypeId)
                                            .OrderByDescending(a=>a.RiskLevel.Order).ToList()
            };

            return viewModel;
        }
    }
}
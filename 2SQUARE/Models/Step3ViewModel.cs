using System;
using System.Collections.Generic;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Services;
using DesignByContract;
using System.Linq;

namespace _2SQUARE.Models
{
    public class Step3ViewModel : StepViewModelBase
    {
        public List<Artifact> Artifacts { get; set; }

        public static Step3ViewModel Create(SquareEntities db, IProjectService projectService, int projectStepId, int projectId, string currentUserId)
        {
            Check.Require(db != null, "db is required.");
            Check.Require(projectService != null, "projectService is required.");

            var project = projectService.GetProject(projectId, currentUserId);
            var projectStep = db.ProjectSteps.Where(a => a.Id == projectStepId).Single();

            Check.Require(project.id == projectStep.ProjectId, Messages.ProjectStepMismatch);

            // load the valid type of artifacts
            var viewModel = new Step3ViewModel()
                                {
                                    Project = project,
                                    ProjectStep = projectStep,
                                    Artifacts =
                                        db.Artifacts.Where(
                                            a =>
                                            a.ProjectId == projectId &&
                                            a.ArtifactType.SquareTypeId == projectStep.Step.SquareTypeId).OrderBy(a=>a.ArtifactTypeId).ThenByDescending(a=>a.DateCreated).ToList()
                                };

            return viewModel;
        }
    }
}
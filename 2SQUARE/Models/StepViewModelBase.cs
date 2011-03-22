﻿using _2SQUARE.App_GlobalResources;
using _2SQUARE.Models;
using _2SQUARE.Services;
using DesignByContract;

public class StepViewModelBase
{
    public Project Project { get; set; }
    public ProjectStep ProjectStep { get; set; }

    public void SetProjectInfo(IProjectService projectService, int projectId, int projectStepId, string userId)
    {
        Check.Require(projectService != null, "projectService is required.");
        Check.Require(!string.IsNullOrWhiteSpace(userId), "userId is required.");

        this.Project = projectService.GetProject(projectId, userId);
        this.ProjectStep = projectService.GetProjectStep(projectStepId, userId);

        Check.Ensure(this.Project != null, "this.Project is required.");
        Check.Ensure(this.ProjectStep != null, "this.ProjectStep is required.");
        Check.Ensure(this.Project.id == this.ProjectStep.ProjectId, Messages.ProjectStepMismatch);
    }
}
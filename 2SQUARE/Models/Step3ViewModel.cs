using System;
using System.Collections.Generic;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;
using System.Linq;

namespace _2SQUARE.Models
{
    public class Step3ViewModel : ViewModelBase
    {
        public List<Artifact> Artifacts { get; set; }

        public static Step3ViewModel Create(SquareContext db, IProjectService projectService, int projectStepId, int projectId, string currentUserId)
        {
            Check.Require(db != null, "db is required.");

            var viewModel = new Step3ViewModel();
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, currentUserId);


            viewModel.Artifacts = db.Artifacts.Where(
                                    a =>
                                    a.Project.Id == projectId &&
                                    a.ArtifactType.SquareType == viewModel.ProjectStep.Step.SquareType)
                                    .OrderBy(a => a.ArtifactType).ThenByDescending(a => a.DateCreated).ToList();

            return viewModel;
        }
    }
}
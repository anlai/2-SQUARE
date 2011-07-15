using System;
using System.Collections.Generic;
using System.Web.Mvc;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;
using DesignByContract;
using System.Linq;

namespace _2SQUARE.Models
{
    public class ArtifactViewModel
    {
        public Artifact Artifact { get; set; }
        public List<ArtifactType> ArtifactTypes { get; set; }

        public ProjectStep ProjectStep { get; set; }

        public static ArtifactViewModel Create(SquareContext db, IProjectService projectService, Artifact artifact, int projectStepId, string loginId)
        {
            Check.Require(db != null, "Repository is required.");

            var projectStep = projectService.GetProjectStep(projectStepId, loginId);

            var viewModel = new ArtifactViewModel()
                                {
                                    ArtifactTypes = db.ArtifactTypes.Where(a=>a.SquareType.Id == projectStep.Step.SquareType.Id).ToList(),
                                    ProjectStep = projectStep,
                                    Artifact = artifact ?? new Artifact()
                                };

            return viewModel;
        }
    }
}
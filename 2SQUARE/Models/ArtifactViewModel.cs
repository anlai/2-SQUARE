using System;
using System.Collections.Generic;
using System.Web.Mvc;
using _2SQUARE.Core.Domain;
using DesignByContract;
using System.Linq;

namespace _2SQUARE.Models
{
    public class ArtifactViewModel
    {
        public Artifact Artifact { get; set; }
        public List<ArtifactType> ArtifactTypes { get; set; }

        public ProjectStep ProjectStep { get; set; }

        public static ArtifactViewModel Create(SquareContext db, Artifact artifact, int projectStepId)
        {
            Check.Require(db != null, "Repository is required.");

            var projectStep = db.ProjectSteps.Where(a => a.Id == projectStepId).Single();

            var viewModel = new ArtifactViewModel()
                                {
                                    ArtifactTypes = db.ArtifactTypes.Where(a=>a.SquareType == projectStep.Step.SquareType).ToList(),
                                    ProjectStep = projectStep,
                                    Artifact = artifact ?? new Artifact()
                                };

            return viewModel;
        }
    }
}
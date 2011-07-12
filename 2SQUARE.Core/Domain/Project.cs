using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Project : DomainObject
    {
        public Project()
        {
            DateCreated = DateTime.Now;

            Artifacts = new List<Artifact>();
            Categories = new List<Category>();
            Goals = new List<Goal>();
            ProjectSteps = new List<ProjectStep>();
            ProjectTerms = new List<ProjectTerm>();
            ProjectWorkers = new List<ProjectWorker>();
            Requirements = new List<Requirement>();
            Risks = new List<Risk>();
        }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public string SecurityElicitationRationale { get; set; }
        public string PrivacyElicitationRationale { get; set; }

        public AssessmentType SecurityAssessmentType { get; set; }
        public AssessmentType PrivacyAssessmentType { get; set; }

        public ElicitationType SecurityElicitationType { get; set; }
        public ElicitationType PrivacyElicitationType { get; set; }

        public ICollection<Artifact> Artifacts { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<ProjectStep> ProjectSteps { get; set; }
        public ICollection<ProjectTerm> ProjectTerms { get; set; }
        public ICollection<ProjectWorker> ProjectWorkers { get; set; }
        public ICollection<Requirement> Requirements { get; set; }
        public ICollection<Risk> Risks { get; set; }
    }
}

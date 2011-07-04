using System;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Project : DomainObject
    {
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
    }
}

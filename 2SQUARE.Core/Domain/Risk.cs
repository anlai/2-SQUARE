using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Risk : DomainObject
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public string Source { get; set; }
        public string Vulnerability { get; set; }
        public string Action { get; set; }
        public int Order { get; set; }

        [Required]
        public Project Project { get; set; }
        [Required]
        public SquareType SquareType { get; set; }
        [Required]
        public AssessmentType AssessmentType { get; set; }
        public Impact Impact { get; set; }

        public RiskLevel Likelihood { get; set; }
        public RiskLevel Damage { get; set; }
        public RiskLevel Magnitude { get; set; }
        public RiskLevel RiskLevel { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class AssessmentType : DomainObject
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        public string Source { get; set; }
        [StringLength(50)]
        [Required]
        public string Controller { get; set; }

        [Required]
        public SquareType SquareType { get; set; }
    }
}

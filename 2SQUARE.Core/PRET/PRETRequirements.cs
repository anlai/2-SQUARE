using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.PRET
{
    public class PRETRequirements : DomainObject
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Requirement { get; set; }
        [StringLength(100)]
        [Required]
        public string Source { get; set; }

        [Required]
        public PRETLaw Law { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class ElicitationType : DomainObject
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        [Required]
        public string Controller { get; set; }
        public string Description { get; set; }
        public string Strengths { get; set; }
        public string Weaknesses { get; set; }

        [Required]
        public SquareType SquareType { get; set; }
    }
}

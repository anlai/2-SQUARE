using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class ProjectTerm : DomainObject
    {
        [StringLength(100)]
        [Required]
        public string Term { get; set; }
        [Required]
        public string Definition { get; set; }
        [Required]
        public string Source { get; set; }

        [Required]
        public Project Project { get; set; }
        [Required]
        public SquareType SquareType { get; set; }
    }
}

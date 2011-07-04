using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Term : DomainObject
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public SquareType SquareType { get; set; }
    }
}

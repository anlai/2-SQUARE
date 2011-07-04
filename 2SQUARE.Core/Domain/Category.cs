using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Category : DomainObject
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        public Project Project { get; set; }
        public SquareType SquareType { get; set; }
    }
}

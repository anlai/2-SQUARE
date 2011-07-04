using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Step : DomainObject
    {
        public int Order { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        [Required]
        public string Controller { get; set; }
        [StringLength(50)]
        [Required]
        public string Action { get; set; }

        [Required]
        public SquareType SquareType { get; set; }
    }
}

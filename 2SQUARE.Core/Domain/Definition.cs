using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Definition : DomainObject
    {
        [Required]
        public string Description { get; set; }
        [StringLength(200)]
        [Required]
        public string Source { get; set; }
        public bool IsActive { get; set; }
        
        [Required]
        public Term Term { get; set; }
    }
}

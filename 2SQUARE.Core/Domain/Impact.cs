using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Impact : DomainObject
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
    }
}

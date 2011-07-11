using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class ProjectRole
    {
        [StringLength(2)]
        [Required]
        public string Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
    }
}

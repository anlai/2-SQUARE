using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class RequirementDefect : DomainObject
    {
        [Required]
        public string Description { get; set; }
        public bool Solved { get; set; }

        [Required]
        public Requirement Requirement { get; set; }
    }
}

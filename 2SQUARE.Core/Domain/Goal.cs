using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Goal : DomainObject
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public SquareType SquareType { get; set; }
        [Required]
        public GoalType GoalType { get; set; }
        [Required]
        public Project Project { get; set; }
    }
}

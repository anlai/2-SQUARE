using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class GoalType
    {
        public char Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public SquareType SquareType { get; set; }
    }
}

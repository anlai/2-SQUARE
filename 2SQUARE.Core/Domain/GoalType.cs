using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class GoalType
    {
        [Key]
        [StringLength(1)]
        [Required]
        public string Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public SquareType SquareType { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class RiskLevel : DomainObject
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(10)]
        [Required]
        public string Color { get; set; }

        public decimal SLikelihood { get; set; }
        public int PLikelihood { get; set; }
        public int Impact { get; set; }
        public int Damage { get; set; }
        public int Order { get; set; }
    }
}

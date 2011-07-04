using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class RiskRecommendation : DomainObject
    {
        [Required]
        public string Controls { get; set; }

        public string Impact { get; set; }
        public string Feasibility { get; set; }

        [Required]
        public Risk Risk { get; set; }
    }
}

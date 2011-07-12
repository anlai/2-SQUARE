using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Term : DomainObject
    {
        public Term()
        {
            Definitions = new List<Definition>();
        }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public SquareType SquareType { get; set; }

        public ICollection<Definition> Definitions { get; set; }
    }
}

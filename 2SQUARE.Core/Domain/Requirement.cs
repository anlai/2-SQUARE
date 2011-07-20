using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace _2SQUARE.Core.Domain
{
    public class Requirement : DomainObject
    {
        public Requirement()
        {
            RequirementDefects = new List<RequirementDefect>();
        }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string RequirementText { get; set; }
        /// <summary>
        /// Used for user to define a specific id if they want
        /// </summary>
        [StringLength(10)]
        public string RequirementId { get; set; }

        public int? Priority { get; set; }
        public bool Essential { get; set; }
        public int? Order { get; set; }

        [Required]
        public Project Project { get; set; }
        [Required]
        public SquareType SquareType { get; set; }
        public Category Category { get; set; }

        public ICollection<RequirementDefect> RequirementDefects { get; set; }
    }
}

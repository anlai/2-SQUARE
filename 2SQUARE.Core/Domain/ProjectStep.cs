using System;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class ProjectStep : DomainObject
    {
        [Required]
        public Project Project { get; set; }

        public Step Step { get; set; }

        public DateTime? DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public bool Complete { get; set; }
    }
}

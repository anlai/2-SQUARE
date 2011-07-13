using System;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class ProjectStep : DomainObject
    {
        public ProjectStep()
        {
            Complete = false;
        }

        public int Id { get; set; }

        [Required]
        public Project Project { get; set; }

        public Step Step { get; set; }

        public DateTime? DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public bool Complete { get; set; }
    }
}

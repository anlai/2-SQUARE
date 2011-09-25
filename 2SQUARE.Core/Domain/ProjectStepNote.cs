using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace _2SQUARE.Core.Domain
{
    public class ProjectStepNote : DomainObject
    {
        public ProjectStepNote()
        {
            DateCreated = DateTime.Now;
        }

        [Required]
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }

        [Required]
        public ProjectStep ProjectStep { get; set; }
    }
}

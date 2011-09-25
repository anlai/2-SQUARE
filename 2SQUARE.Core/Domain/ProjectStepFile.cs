using System;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class ProjectStepFile : DomainObject
    {
        public ProjectStepFile()
        {
            DateCreated = DateTime.Now;
        }

        [Required]
        public byte[] Contents { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string ContentType { get; set; }

        public string Notes { get; set; }
        public DateTime DateCreated { get; set; }

        [Required]
        public ProjectStep ProjectStep { get; set; }
    }
}

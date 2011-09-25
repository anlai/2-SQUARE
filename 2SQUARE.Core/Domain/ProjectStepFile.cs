using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace _2SQUARE.Core.Domain
{
    public class ProjectStepFile : DomainObject
    {
        [Required]
        public byte[] Contents { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string ContentType { get; set; }

        [Required]
        public ProjectStep ProjectStep { get; set; }
    }
}

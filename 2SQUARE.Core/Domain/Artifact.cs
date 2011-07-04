using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace _2SQUARE.Core.Domain
{
    public class Artifact : DomainObject
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Data { get; set; }
        
        public string ContentType { get; set; }
        
        public ArtifactType ArtifactType { get; set; }
        public Project Project { get; set; }
        
        [StringLength(50)]
        [Required]
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

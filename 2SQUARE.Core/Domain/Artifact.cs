using System;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.Domain
{
    public class Artifact : DomainObject
    {
        public Artifact()
        {
            DateCreated = DateTime.Now;
        }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Data { get; set; }
        
        public string ContentType { get; set; }
        
        [Required]
        public ArtifactType ArtifactType { get; set; }
        [Required]
        public Project Project { get; set; }
        
        [StringLength(50)]
        [Required]
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

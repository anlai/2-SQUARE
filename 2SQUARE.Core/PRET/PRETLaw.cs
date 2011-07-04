using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.PRET
{
    public class PRETLaw : DomainObject
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<PRETAnswer> Answers { get; set; }
    }
}

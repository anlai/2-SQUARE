using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core.PRET
{
    public class PRETLaw : DomainObject
    {
        public PRETLaw()
        {
            Answers = new List<PRETAnswer>();
            Requirements = new List<PRETRequirement>();
        }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<PRETAnswer> Answers { get; set; }
        public ICollection<PRETRequirement> Requirements { get; set; }
    }
}

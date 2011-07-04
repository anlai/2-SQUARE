using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace _2SQUARE.Core.PRET
{
    public class PRETAnswer : DomainObject
    {
        [Required]
        public string Answer { get; set; }

        public PRETQuestion Question { get; set; }
        public ICollection<PRETLaw> Laws { get; set; }
    }
}

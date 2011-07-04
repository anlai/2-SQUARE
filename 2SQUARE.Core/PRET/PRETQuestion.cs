using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace _2SQUARE.Core.PRET
{
    public class PRETQuestion : DomainObject
    {
        [Required]
        public string Question { get; set; }
        public int Order { get; set; }
        public bool SubQuestion { get; set; }

        public PRETQuestion ParentQuestion { get; set; }
    }
}

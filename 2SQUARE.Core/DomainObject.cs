using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace _2SQUARE.Core
{
    public class DomainObject
    {
        [Key]
        public int Id { get; set; }
    }
}

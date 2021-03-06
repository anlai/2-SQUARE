﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace _2SQUARE.Core.PRET
{
    public class PRETQuestion : DomainObject
    {
        public PRETQuestion()
        {
            Children = new List<PRETQuestion>();
            PretAnswers = new List<PRETAnswer>();
        }

        [Required]
        public string Question { get; set; }
        public int Order { get; set; }
        public bool SubQuestion { get; set; }

        public PRETQuestion ParentQuestion { get; set; }

        /// <summary>
        /// Children questions
        /// </summary>
        public ICollection<PRETQuestion> Children { get; set; }

        public ICollection<PRETAnswer> PretAnswers { get; set; }
    }
}

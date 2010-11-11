using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;

namespace _2SQUARE.Core.Domain
{
    public class SquareType : DomainObject
    {
        public SquareType()
        {
            Steps = new List<Step>();
        }

        [NotNull]
        [Length(50)]
        public virtual string Name { get; set; }

        public virtual IList<Step> Steps { get; set; }
    }

    public class SquareTypeMap : ClassMap<SquareType>
    {
        public SquareTypeMap()
        {
            ReadOnly();

            Id(x => x.Id);
            
            Map(x => x.Name);
            HasMany(x => x.Steps).Inverse();
        }
    }
}

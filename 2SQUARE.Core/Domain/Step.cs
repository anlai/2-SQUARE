using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;

namespace _2SQUARE.Core.Domain
{
    public class Step : DomainObject
    {
        public virtual int Order { get; set; }
        [NotNull]
        [Length(50)]
        public virtual string Name { get; set; }
        [Length(500)]
        public virtual string Description { get; set; }
        [NotNull]
        public virtual SquareType SquareType { get; set; }
    }

    public class StepMap : ClassMap<Step>
    {
        public StepMap()
        {
            Id(x => x.Id);

            Map(x => x.Order).Column("[Order]");
            Map(x => x.Name);
            Map(x => x.Description);

            References(x => x.SquareType);
        }
    }
}

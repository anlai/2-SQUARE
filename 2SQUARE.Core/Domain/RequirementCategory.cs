﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;

namespace _2SQUARE.Core.Domain
{
    public class RequirementCategory : DomainObject
    {
        [NotNull]
        [Length(50)]
        public virtual string Name { get; set; }

        public virtual IList<Step> Steps { get; set; }
    }

    public class RequirementCategoryMap : ClassMap<RequirementCategory>
    {
        public RequirementCategoryMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);

            HasMany(x => x.Steps).Inverse();
        }
    }
}

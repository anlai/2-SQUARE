using System;
using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;

namespace _2SQUARE.Core.Domain
{
    public class Role : DomainObjectWithTypedId<Guid>
    {
        [NotNull]
        [Length(50)]
        public virtual string Name { get; set; }
    }

    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Table("aspnet_Roles");

            ReadOnly();

            Id(x => x.Id).Column("RoleId");
            Map(x => x.Name);
        }
    }
}

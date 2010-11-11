using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace _2SQUARE.Core.Domain
{
    public class User : DomainObjectWithTypedId<Guid>
    {
        public virtual string UserName { get; set; }
        public virtual string LoweredUserName { get; set; }
        public virtual DateTime LastActivityDate { get; set; }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("aspnet_Users");
            ReadOnly();

            Id(x => x.Id).Column("UserId");

            Map(x => x.UserName);
            Map(x => x.LoweredUserName);
            Map(x => x.LastActivityDate);
        }
    }

}

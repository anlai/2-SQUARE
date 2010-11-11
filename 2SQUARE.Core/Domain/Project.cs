using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace _2SQUARE.Core.Domain
{
    public class Project : DomainObject
    {
        public virtual string Name { get; set; }
        public virtual DateTime DateCreated { get; set; }

        public virtual IList<ProjectWorker> Workers { get; set; }
    }

    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.DateCreated);

            HasMany(x => x.Workers).Inverse();
        }
    }
}

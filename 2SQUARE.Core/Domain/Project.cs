using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace _2SQUARE.Core.Domain
{
    public class Project : DomainObject
    {
        public Project()
        {
           // Workers = new List<ProjectWorker>();
            ProjectSteps = new List<ProjectStep>();

            DateCreated= new DateTime();
        }

        [Required]
        [Length(50)]
        public virtual string Name { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual string Description { get; set; }

        //public virtual IList<ProjectWorker> Workers { get; set; }
        public virtual IList<ProjectStep> ProjectSteps { get; set; }
    }

    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.DateCreated);

            //HasMany(x => x.Workers).Inverse();
            HasMany(x => x.ProjectSteps).Inverse();
        }
    }
}

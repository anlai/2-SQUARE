using System;
using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;

namespace _2SQUARE.Core.Domain
{
    public class ProjectStep : DomainObject
    {
        public ProjectStep()
        {
            SetDefaults();
        }

        public ProjectStep(Project project, Step step)
        {
            Project = project;
            Step = step;

            SetDefaults();
        }

        private void SetDefaults()
        {
            DateStarted = DateTime.Now;
            Complete = false;
        }

        [NotNull]
        public virtual Project Project { get; set; }
        [NotNull]
        public virtual Step Step { get; set; }
        public virtual DateTime DateStarted { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual bool Complete { get; set; }
    }

    public class ProjectStepMap : ClassMap<ProjectStep>
    {
        public ProjectStepMap()
        {
            Id(x => x.Id);

            References(x => x.Project);
            References(x => x.Step);
            Map(x => x.DateStarted);
            Map(x => x.DateCreated);
            Map(x => x.Complete);
        }
    }
}

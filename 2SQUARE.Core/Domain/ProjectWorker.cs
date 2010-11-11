using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;

namespace _2SQUARE.Core.Domain
{
    public class ProjectWorker : DomainObject
    {
        [NotNull]
        public virtual User User { get; set; }
        [NotNull]
        public virtual Project Project { get; set; }
        [NotNull]
        public virtual Role Role { get; set; }
    }

    public class ProjectWorkerMap : ClassMap<ProjectWorker>
    {
        public ProjectWorkerMap()
        {
            Id(x => x.Id);

            References(x => x.User);
            References(x => x.Project);
            References(x => x.Role);
        }
    }
}

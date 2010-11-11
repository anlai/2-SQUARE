using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace _2SQUARE.Core.Domain
{
    public class ProjectWorker : DomainObject
    {
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
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

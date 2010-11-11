using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace _2SQUARE.Core.Domain
{
    public class ProjectTerm : DomainObject
    {
        [Required]
        [Length(100)]
        public virtual string Term { get; set; }
        [Required]
        public virtual string Definition { get; set; }
        [Required]
        [Length(200)]
        public virtual string Source { get; set; }
        [NotNull]
        public virtual Project Project { get; set; }
    }

    public class ProjectTermMap : ClassMap<ProjectTerm>
    {
        public ProjectTermMap()
        {
            Id(x => x.Id);

            Map(x => x.Term);
            Map(x => x.Definition);
            Map(x => x.Source);
            References(x => x.Project);
        }
    }
}

using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace _2SQUARE.Core.Domain
{
    public class Definition : DomainObject
    {
        public Definition()
        {
            IsActive = true;
        }

        [Required]
        public virtual string Description { get; set; }
        [Required]
        [Length(200)]
        public virtual string Source { get; set; }
        public virtual bool IsActive { get; set; }
        [NotNull]
        public virtual Term Term { get; set; }
    }

    public class DefinitionMap : ClassMap<Definition>
    {
        public DefinitionMap()
        {
            ReadOnly();

            Id(x => x.Id);

            Map(x => x.Description);
            Map(x => x.Source);
            Map(x => x.IsActive);
            References(x => x.Term);
        }
    }

}

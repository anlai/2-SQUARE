using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace _2SQUARE.Core.Domain
{
    public class Term : DomainObject
    {
        public Term()
        {
            IsActive = true;
        }

        [Required]
        [Length(100)]
        public virtual string Name { get; set; }
        [NotNull]
        public virtual SquareType SquareType { get; set; }
        public virtual bool IsActive { get; set; }
    }

    public class TermMap : ClassMap<Term>
    {
        public TermMap()
        {
            ReadOnly();

            Id(x => x.Id);

            Map(x => x.Name);
            References(x => x.SquareType);
            Map(x => x.IsActive);
        }
    }

}

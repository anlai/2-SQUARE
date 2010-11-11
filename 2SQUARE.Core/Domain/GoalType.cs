using FluentNHibernate.Mapping;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace _2SQUARE.Core.Domain
{
    public class GoalType : DomainObject
    {
        public GoalType()
        {
            IsActive = true;
        }

        [Required]
        [Length(50)]
        public virtual string Name { get; set; }
        public virtual bool IsActive { get; set; }
    }

    public class GoalTypeMap : ClassMap<GoalType>
    {
        public GoalTypeMap()
        {
            ReadOnly();
            
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.IsActive);
        }
    }
}

using CodeFirstMembershipDemoSharp.Data;

namespace _2SQUARE.Core.Domain
{
    public class ProjectWorker : DomainObject
    {
        public User User { get; set; }
        public Project Project { get; set; }
        public ProjectRole Role { get; set; }
    }
}

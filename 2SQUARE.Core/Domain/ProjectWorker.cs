using _2SQUARE.Core.Aspnet;

namespace _2SQUARE.Core.Domain
{
    public class ProjectWorker : DomainObject
    {
        public aspnet_User User { get; set; }
        public Project Project { get; set; }
        public Role Role { get; set; }
    }
}

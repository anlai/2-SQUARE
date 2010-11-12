using _2SQUARE.Services;
using Castle.Windsor;


namespace _2SQUARE
{
    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            container.AddComponent("projectService", typeof (IProjectService), typeof (ProjectService));
        }
    }
}
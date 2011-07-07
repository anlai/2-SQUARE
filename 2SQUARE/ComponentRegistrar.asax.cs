using _2SQUARE.Services;
using Castle.Windsor;


namespace _2SQUARE
{
    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            container.AddComponent("projectsService", typeof(IProjectsService), typeof(ProjectsService));

            container.AddComponent("projectService", typeof (IProjectService), typeof (ProjectService));
            container.AddComponent("validationService", typeof (IValidationService), typeof (ValidationService));
        }
    }
}
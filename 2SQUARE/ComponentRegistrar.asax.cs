using _2SQUARE.Services;
using Castle.Windsor;
using UCDArch.Core.CommonValidator;
using UCDArch.Core.NHibernateValidator.CommonValidatorAdapter;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;

namespace _2SQUARE
{
    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            //Add your components here
            container.AddComponent("validator", typeof(IValidator), typeof(Validator));
            container.AddComponent("dbContext", typeof(IDbContext), typeof(DbContext));

            container.AddComponent("projectService", typeof (IProjectService), typeof (ProjectService));

            AddRepositoriesTo(container);
        }

        private static void AddRepositoriesTo(IWindsorContainer container)
        {
            container.AddComponent("repository", typeof(IRepository), typeof(Repository));
            container.AddComponent("genericRepository", typeof(IRepository<>), typeof(Repository<>));
            container.AddComponent("typedRepository", typeof(IRepositoryWithTypedId<,>),
                                   typeof(RepositoryWithTypedId<,>));
        }
    }
}
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CommandHandler.Commands.Common;

namespace CommandHandler
{
    public class WindsorCommandInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().IncludeNonPublicTypes()
                .BasedOn<IANTILCommand>().WithServiceDefaultInterfaces().LifestyleTransient());
            container.Register(Component.For<CommandHandler>().LifestyleTransient());
            container.Register(Component.For<Controller>().LifestyleTransient());
            container.Register(Component.For<CommandHandlerHelper>());
        }
    }
}

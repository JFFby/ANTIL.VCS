using ANTIL.Domain.CastleWndsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace HttpCommandHandler.Windsdor
{
    public class WinsdorHttpInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(new WindsorNhibernateInstaller());
            container.Register(Component.For<HttpController>());
            container.Register(Component.For<HttpCommandHandler>());
        }
    }
}

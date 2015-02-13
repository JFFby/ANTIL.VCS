using System.ComponentModel;
using Castle.Windsor;

namespace HttpCommandHandler.Windsdor
{
    public static class IOC 
    {
        private static WindsorContainer container;

        static IOC()
        {
            container = new WindsorContainer();
            container.Install(new WinsdorHttpInstaller());
        }

        public static T Resolve<T>() where T :class 
        {
            return container.Resolve<T>();
        }
    }
}

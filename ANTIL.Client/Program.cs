using System;
using System.IO;
using Castle.Windsor;
using CommandHandler;

namespace ANTIL.Client
{
    class Program
    {
        private static WindsorContainer IOC;

        static void Main(string[] args)
        {
            InstallContainer();
            var commandHandler = IOC.Resolve<CommandHandler.CommandHandler>();
            commandHandler.Run();
        }

        private static void InstallContainer()
        {
            IOC = new WindsorContainer();
            IOC.Install(new WindsorCommandInstaller());
        }
    }
}

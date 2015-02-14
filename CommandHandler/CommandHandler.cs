using System;
using CommandHandler.Entites;
using CommandHandler.Helpers;

namespace CommandHandler
{
    public class CommandHandler
    {
        private readonly Controller controller;
        private readonly ConsoleHelper ch;
        private readonly CommandHandlerHelper cmdHelper;
        private readonly AntilStorageHelper storageHelper;

        public CommandHandler(Controller controller, CommandHandlerHelper cmdHelper, AntilStorageHelper storageHelper)
        {
            this.controller = controller;
            this.cmdHelper = cmdHelper;
            this.storageHelper = storageHelper;
            ch = new ConsoleHelper();
        }

        public void Run()
        {
            var antilStore = storageHelper.InitAntilStore();

            ch.WriteLine("Hello, I'm ANTIL and I'm Version Control System");
            ch.WriteLine("Use 'help' command to see all my features :)");
            do
            {
                ConsoleWrite(antilStore);
                var command = Console.ReadLine();

                cmdHelper.ExecuteMethod(controller, command);

            } while (true);

        }

        private void ConsoleWrite(AntilStoreItem antilStore)
        {
            var storeItem = storageHelper.GetCdPath(antilStore);
            Console.Write(storeItem.Cd);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(storeItem.ProjectName);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" > ");
        }
    }
}

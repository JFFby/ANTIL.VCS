using System;
using System.IO;
using System.Xml.Linq;
using CommandHandler.Helpers;

namespace CommandHandler
{
    /// <summary>
    /// Думаю не имеет смысла реализовавывать у CommandHandler никаких интерфейсов ибо
    /// мложно себе представить себе другую реализацию этого класса
    /// З.Ы. как прочтёшь, удаляй
    /// </summary>
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
                var cd = storageHelper.GetCdPath(antilStore);
                Console.Write(cd + " > ");
                var command = Console.ReadLine();

                cmdHelper.ExecuteMethod(controller, command);

            } while (true);

        }
    }
}

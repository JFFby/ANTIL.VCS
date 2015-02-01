using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Schema;
using CommandHandler.Commands.Exit;

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

        public CommandHandler(Controller controller)
        {
            this.controller = controller;
        }

        public void Run()
        {
            var antilStore = InitAntilStore();

            WriteLine("Hello, I'm ANTIL and I'm Version Control System");
            WriteLine("Use 'help' command to see all my features :)");
            do
            {
                var cd = GetCd(antilStore);
                Console.Write(cd + " > ");
                var command = Console.ReadLine();

                ExecuteMethod(command);

            } while (true);

        }

        private CommandItem ParseCommand(string command)
        {
            string[] commandArgs = command.Split(' ');
            var args = new List<string>();

            for (int i = 1; i < commandArgs.Length; ++i)
            {
                args.Add(commandArgs[i]);
            }

            return new CommandItem
            {
                Commant = ProcessCommand(commandArgs[0]),
                Args = new object[] { args }
            };
        }

        private void ExecuteMethod(string command)
        {
            var comandItem = ParseCommand(command);

            if (comandItem.Commant.Length == 0)
                return;

            MethodInfo methodInfo = controller.GetType().GetMethod(comandItem.Commant);
            if (methodInfo != null && methodInfo.IsPublic)
            {
                methodInfo.Invoke(controller, comandItem.Args);
            }
            else
            {
                WriteLine("I don't know this command.", ConsoleColor.Red);
            }
        }

        public void WriteLine(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("ANTIL: " + msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Write(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.Write("ANTIL: " + msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private string ProcessCommand(string command)
        {
            if (command.Length > 0)
                return command.ToLower().Replace(command[0].ToString(),
                    command[0].ToString().ToUpper());
            else
                return command;

        }

        private AntilStoreItem InitAntilStore()
        {
            string path = "ANTIL.xml";

            var file = new FileInfo(path);
            if (!file.Exists)
            {
                var document = new XDocument(
                    new XElement("Cd", new XAttribute("path", string.Empty))
                    );

                document.Save(path);

                return new AntilStoreItem
                {
                    Store = new FileInfo(path),
                    Cd = string.Empty
                };
            }

            string cd = XDocument.Load(path).Element("Cd").Attribute("path").Value;

                return new AntilStoreItem
            {
                Store = file,
                Cd = cd 
            };
        }

        private string GetCd(AntilStoreItem item)
        {
            var path = "ANTIL.xml";
            var cd = string.Empty;
            var newFileRevision = new FileInfo(path);

            if (newFileRevision.LastWriteTime == item.Store.LastWriteTime)
                cd = item.Cd;
            else
            {
                cd = XDocument.Load(path).Element("Cd").Attribute("path").Value;
                item.Store = newFileRevision;
            }

            item.Cd = cd;
            return cd;

        }
    }
}

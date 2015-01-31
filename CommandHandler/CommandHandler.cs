using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
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
            WriteLine("Hello, I'm ANTIL and I'm Version Control System");
            do
            {
                Console.Write("> ");
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

        public void ExecuteMethod(string command)
        {
            var comandItem = ParseCommand(command);

            if (comandItem.Commant.Length == 0)
                return;

            MethodInfo methodInfo = controller.GetType().GetMethod(comandItem.Commant);
            if (methodInfo != null)
            {
                Console.Write("\n\n");
                methodInfo.Invoke(controller, comandItem.Args);
                Console.Write("\n\n");
            }
            else
            {
                Console.Write("\n\n");
                WriteLine("I don't know this command.", ConsoleColor.Red);
                Console.Write("\n\n");
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
    }
}

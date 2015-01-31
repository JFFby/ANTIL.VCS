using System;
using System.Collections.Generic;
using System.Reflection;
using CommandHandler.Commands.Exit;

namespace CommandHandler
{
    /// <summary>
    /// Думаю не имеет смысла реализовавывать у CommandHandler никаких интерфейсов ибо
    /// мложно себе представить себе другую реализацию этого класса
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
            WriteLine("Hello, I'm ANTIL");
            do
            {
                Write("> ");
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
                Commant = commandArgs[0].ToLower(),
                Args = new object[] { args }
            };
        }

        public void ExecuteMethod(string command)
        {
            var comandItem = ParseCommand(command);

            MethodInfo methodInfo = controller.GetType().GetMethod(comandItem.Commant);
            if(methodInfo != null)
                methodInfo.Invoke(controller, comandItem.Args);
            else
                WriteLine("ANTIL: I don't know this command.",ConsoleColor.Red);
        }

        public void WriteLine(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Write(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}

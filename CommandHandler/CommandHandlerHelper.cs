using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandHandler
{
    public class CommandHandlerHelper
    {

        public bool ExecuteMethod(object methodHandler, string command)
        {
            var comandItem = ParseCommand(command);

            if (comandItem.Commant.Length == 0)
                return false;

            return Execute(methodHandler, comandItem.Commant, comandItem.Args);
        }

        public bool ExecuteMethod(object methodHandler, ICollection<string> commandLine)
        {
            var commandItem = ParseCommand(commandLine);
            return Execute(methodHandler, commandItem.Commant, commandItem.Args);
        }

        public bool IsMethodExist(object methodHandler, string method)
        {
            return methodHandler.GetType().GetMethod(ProcessCommand(method)) != null;
        }

        private CommandItem ParseCommand(ICollection<string> command)
        {
            var args = new List<string>();

            for (int i = 1; i < command.Count; ++i)
            {
                args.Add(command.ToList()[i]);
            }

            return new CommandItem
            {
                Commant = ProcessCommand(command.ToList()[0]),
                Args = new object[] { args }
            };
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

        private string ProcessCommand(string command)
        {
            if (command.Length > 0)
                return command.ToLower().Replace(command[0].ToString(),
                    command[0].ToString().ToUpper());
            else
                return command;
        }

        private static bool Execute(object methodHandler, string command, object[] args)
        {
            var result = false;
            MethodInfo methodInfo = methodHandler.GetType().GetMethod(command);
            if (methodInfo != null && methodInfo.IsPublic)
            {
                methodInfo.Invoke(methodHandler, args);
                result = true;
            }
            else
            {
                var ch = new ConsoleHelper();
                ch.WriteLine("I don't know this command.",ConsoleColor.Red);
            }

            return result;
        }
    }
}

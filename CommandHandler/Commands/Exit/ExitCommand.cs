using System;
using System.Collections.Generic;
using CommandHandler.Commands.Common;

namespace CommandHandler.Commands.Exit
{
    public class ExitCommand : BaseCommand, IExitCommand
    {
        public void Execute(ICollection<string> args)
        {
            WriteLine("Bye");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}

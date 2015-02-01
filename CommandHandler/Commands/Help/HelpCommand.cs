using System;
using System.Collections.Generic;
using CommandHandler.Commands.Common;

namespace CommandHandler.Commands.Help
{
    public class HelpCommand: BaseCommand, IHelpCommand
    {
        public void Execute(ICollection<string> args)
        {
            Console.WriteLine("\n");
            ch.WriteLine("Parametrs: [require], <optional>");
            ch.WriteLine("Avaliable commands:\n");
            ch.WriteLine("1) exit      - Exit from app");
            ch.WriteLine("2) cd        - Go to the some directory");
            ch.WriteLine("\t\t[path]     - Path");
            ch.WriteLine("\t\t<clear>    - Clear current path");
            Console.WriteLine("\n");
        }
    }
}

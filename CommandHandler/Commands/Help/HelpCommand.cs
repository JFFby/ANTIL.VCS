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
            ch.WriteLine("\t\t[path] | [project [project name]]    - Path");
            ch.WriteLine("\t\t<clear>    - Clear current path");
            ch.WriteLine("3) dir       - List of folders and files contained in the current folder");
            ch.WriteLine("4) init      - Initialize ANTIL repository in current directory");
            ch.WriteLine("\t\t<...>      - Project name");
            ch.WriteLine("5) list      - List of existing repositories");
            ch.WriteLine("6) register  - Register a new user");
            ch.WriteLine("\t\t[username]    - user name");
            Console.WriteLine("\n");
        }
    }
}

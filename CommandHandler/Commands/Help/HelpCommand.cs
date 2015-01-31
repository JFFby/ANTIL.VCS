using System.Collections.Generic;
using CommandHandler.Commands.Common;

namespace CommandHandler.Commands.Help
{
    public class HelpCommand: BaseCommand, IHelpCommand
    {
        public void Execute(ICollection<string> args)
        {
            WriteLine("Avaliable commands:\n");
            WriteLine("1) exit      - Exit from app");
            WriteLine("2) cd [path] - Go to the some directory");
        }
    }
}

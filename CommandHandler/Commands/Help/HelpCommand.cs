using System.Collections.Generic;
using CommandHandler.Commands.Common;

namespace CommandHandler.Commands.Help
{
    public class HelpCommand: BaseCommand, IHelpCommand
    {
        public void Execute(ICollection<string> args)
        {
            WriteLine("Avaliable commands:\n");
            WriteLine("exit - Exit from app");
        }
    }
}

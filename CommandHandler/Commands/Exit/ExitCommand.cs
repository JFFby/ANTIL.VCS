using System;
using System.Collections.Generic;
using System.Threading;
using CommandHandler.Commands.Common;

namespace CommandHandler.Commands.Exit
{
    public class ExitCommand : BaseCommand, IExitCommand
    {
        public void Execute(ICollection<string> args)
        {
            ch.Write("Bye");
            for (int i = 0; i < 4; ++i)
            {
                Thread.Sleep(300);
                Console.Write(".");
            }
            Environment.Exit(0);
        }
    }
}

using System;
using System.Collections.Generic;
using CommandHandler.Commands.Common;

namespace CommandHandler.Commands.Status
{
    public class StatusCommand : BaseCommand, IStatusCommand
    {
        public void Execute(ICollection<string> args)
        {
            ch.WriteLine("Not implemented yet",ConsoleColor.Red);
        }
    }
}

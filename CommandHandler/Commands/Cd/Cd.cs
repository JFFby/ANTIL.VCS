using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandHandler.Commands.Common;

namespace CommandHandler.Commands.Cd
{
    public class Cd : BaseCommand, ICd
    {
        public void Execute(ICollection<string> args)
        {
            var path = string.Empty;
            if (args.Count > 0 && args.ToList()[0].Length > 0)
                path = args.ToList()[0];
            else
            {
                WriteLine("Bad arguments",ConsoleColor.Red);
                return;
            }

            var dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                WriteLine("You are in " + dir.FullName + " now", ConsoleColor.Green);
            }
            else
            {
                WriteLine("Folder does not exist",ConsoleColor.Red);
            }
        }
    }
}

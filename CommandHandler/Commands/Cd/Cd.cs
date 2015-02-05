using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.Cd
{
    public class Cd : BaseCommand, ICd
    {
        private readonly CommandHandlerHelper cmdHelper;
        private readonly AntilStorageHelper storageHelper;

        public Cd(CommandHandlerHelper cmdHelper, AntilStorageHelper storageHelper)
        {
            this.cmdHelper = cmdHelper;
            this.storageHelper = storageHelper;
        }

        public void Execute(ICollection<string> args)
        {
            if (args.Count > 0 && cmdHelper.IsMethodExist(this, args.ToList()[0]))
            {
                cmdHelper.ExecuteMethod(this, args);
                return;
            }

            ExrcuteCdCommandWithOutArgs(args);
        }

        private void ExrcuteCdCommandWithOutArgs(ICollection<string> args)
        {
            var path = string.Empty;
            if (args.Count > 0 && args.ToList()[0].Length > 0)
                path = args.ToList()[0];
            else
            {
                ch.WriteLine("Bad arguments", ConsoleColor.Red);
                return;
            }

            var dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                ch.WriteLine("You are in " + dir.FullName + " now", ConsoleColor.Green);
                storageHelper.SetCd(dir.FullName);
            }
            else
            {
                ch.WriteLine("Folder does not exist", ConsoleColor.Red);
            }
        }

        public void Clear(ICollection<string> args)
        {
            storageHelper.SetCd(string.Empty);
            ch.WriteLine("Current location is clear", ConsoleColor.Green);
        }


    }
}

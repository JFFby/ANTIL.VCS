using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            ExecuteCdCommandWithOutArgs(args);
        }

        private void ExecuteCdCommandWithOutArgs(ICollection<string> args)
        {
            var path = string.Empty;
            if (args.Count > 0 && args.ToList()[0].Length > 0)
                path = string.Join(" ", args);
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

        public void Project(ICollection<string> args)
        {
            if (!(args.Count > 0))
            {
                ch.WriteLine("Enter project name",ConsoleColor.Red);
                return;
            }

            foreach (var project in storageHelper.GetProjects())
            {
                if (project.Name == args.ToList()[0])
                {
                    storageHelper.SetCd(project.Path);
                    ch.WriteLine("You are in "+project.Name +" repository now",ConsoleColor.Green);
                    return;
                }
            }

            ch.WriteLine("This project doesn't exist",ConsoleColor.Red);
        }
    }
}

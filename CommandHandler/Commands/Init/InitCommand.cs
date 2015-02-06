using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.Init
{
    public class InitCommand : BaseCommand, IInitCommand
    {
        private readonly AntilStorageHelper storageHelper;
        private const string subPath = ".ANTIL";
        private string cdPath;

        public InitCommand(AntilStorageHelper storageHelper)
        {
            this.storageHelper = storageHelper;
            cdPath = storageHelper.GetCdPath();
        }

        public void Execute(ICollection<string> args)
        {
            var dir = new DirectoryInfo(cdPath);

            if(!IsValidData(dir))
                return;

            try
            {
                dir.CreateSubdirectory(subPath);
                var antilDir = new DirectoryInfo(cdPath + subPath);
                antilDir.Attributes = FileAttributes.Hidden;
            }
            catch (Exception ex)
            {
                ch.WriteLine(ex.Message,ConsoleColor.Red);
            }

            ch.WriteLine("Repository was nitialized",ConsoleColor.Green);
        }

        private bool IsValidData(DirectoryInfo dir)
        {

            if (!dir.Exists)
            {
                ch.WriteLine("Some error in 'Init' command", ConsoleColor.Red);
                return false;
            }

            if (dir.GetDirectories().Any(x => x.Name == subPath))
            {
                ch.WriteLine("This repository was already initialized", ConsoleColor.Red);
                return false;
            }

            return true;
        }
    }
}

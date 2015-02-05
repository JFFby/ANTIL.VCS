using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.Init
{
    public class Init : BaseCommand, IInit
    {
        private readonly AntilStorageHelper storageHelper;
        private const string subPath = ".ANTIL";
        private string cdPath;

        public Init(AntilStorageHelper storageHelper)
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
                var splitter = cdPath.ToCharArray()[cdPath.Length -1].ToString() == "\\" ? string.Empty  : "\\";
                dir.CreateSubdirectory(subPath);
                var antilDir = new DirectoryInfo(cdPath + splitter + subPath);
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

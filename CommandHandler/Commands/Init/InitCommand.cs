using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.Init
{
    public class InitCommand : BaseCommand, IInitCommand
    {
        private readonly AntilStorageHelper storageHelper;
        private readonly RepositoryXMLHelper repositoryHelper;
        private const string subPath = ".ANTIL";
        private string cdPath;

        public InitCommand(AntilStorageHelper storageHelper, RepositoryXMLHelper repositoryHelper)
        {
            this.storageHelper = storageHelper;
            this.repositoryHelper = repositoryHelper;
            cdPath = storageHelper.GetCdPath();
        }

        public void Execute(ICollection<string> args)
        {
            var dir = CreateRepositoryCatalog();

            if (dir == null)
                return;

            repositoryHelper.CreateRepoStorage(dir.FullName, args);
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

        private DirectoryInfo CreateRepositoryCatalog()
        {
            var dir = new DirectoryInfo(cdPath);

            if (!IsValidData(dir))
                return null;

            DirectoryInfo antilDir = null; 
            try
            {
                dir.CreateSubdirectory(subPath);
                antilDir = new DirectoryInfo(cdPath + subPath);
                antilDir.Attributes = FileAttributes.Hidden;
            }
            catch (Exception ex)
            {
                ch.WriteLine(ex.Message, ConsoleColor.Red);
            }

            ch.WriteLine("Repository was nitialized", ConsoleColor.Green);

            return antilDir ;
        }
    }
}

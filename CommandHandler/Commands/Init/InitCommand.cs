using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandHandler.Commands.Commit;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.Init
{
    public class InitCommand : BaseCommand, IInitCommand
    {
        private readonly AntilStorageHelper storageHelper;
        private readonly RepositoryXMLHelper repositoryHelper;
        private readonly ICommitCommand commitCommand;
        private const string subPath = ".ANTIL";
        private string cdPath;

        public InitCommand(AntilStorageHelper storageHelper,
            RepositoryXMLHelper repositoryHelper,
            ICommitCommand commitCommand)
        {
            this.storageHelper = storageHelper;
            this.repositoryHelper = repositoryHelper;
            this.commitCommand = commitCommand;
        }

        public void Execute(ICollection<string> args)
        {
            cdPath = storageHelper.GetCdPath();
            var dir = CreateRepositoryCatalog();

            if (dir == null)
                return;

            var projName = repositoryHelper.ProcessProjectName(dir.FullName, args);
            storageHelper.AddProject(dir.FullName.Replace(subPath, ""), projName);
            repositoryHelper.CreateRepoStorage(dir.FullName, projName);

            commitCommand.Execute(new List<string>(){"init"});
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

            return antilDir;
        }
    }
}

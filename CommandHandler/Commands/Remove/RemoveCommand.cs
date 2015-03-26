using System;
using System.Collections.Generic;
using System.Linq;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;
using System.IO;

namespace CommandHandler.Commands.Remove
{
    public class RemoveCommand : BaseCommand, IRemoveCommand
    {
        private readonly RepositoryXMLHelper repositoryhHelper;

        public RemoveCommand(RepositoryXMLHelper repositoryhHelper)
        {
            this.repositoryhHelper = repositoryhHelper;
        }

        public void Execute(ICollection<string> args)
        {
            if (string.IsNullOrEmpty(repositoryhHelper.Project.Name))
            {
                ch.WriteLine("You need to initialize a repository first.", ConsoleColor.Red);
                return;
            }
            
            if (args.Count != 1)
            {
                ch.WriteLine("Bad arguments. Type \"help\" to see the reference");
                return;
            }
            else if (args.ToArray()[0] == "-a")
            {
                RemoveAllFiles();
            }
            else
            {
                FileInfo file = new FileInfo(repositoryhHelper.Project.Path + args.ToArray()[0]);
                if (file.Exists)
                    RemoveFile(file, true);
                else
                {
                    ch.WriteLine("There's no such file in commit!", ConsoleColor.Red);
                    return;
                }
            }
        }

        private void RemoveFile(FileInfo file, bool single = false)
        {
            var doc = repositoryhHelper.CheckForNewCommitSection();
            var newCommit = doc.Descendants("Commit")
                .First(e => e.Attribute("id").Value == "new");
            if (newCommit.Elements("File").Any(e => e.Element("fullName").Value == file.ShotFileName(repositoryhHelper.Project.Path)))
            {
                newCommit.Elements("File")
                    .First(e => e.Element("fullName").Value == file.ShotFileName(repositoryhHelper.Project.Path)).Remove();
                doc.Save(repositoryhHelper.PathToSave);
                ch.WriteLine(string.
                    Format("\t {0} was removed from repository index", file.ShotFileName(repositoryhHelper.Project.Path)), ConsoleColor.Green);
            }
            else if (single)
            {
                ch.WriteLine(string.
                Format("\t {0} not in the repository index", file.ShotFileName(repositoryhHelper.Project.Path)), ConsoleColor.Red);
            }
        }

        private void RemoveAllFiles()
        {
            IEnumerable<FileInfo> files = repositoryhHelper.GetFiles(repositoryhHelper.Project.Path);
            foreach (var file in files)
            {
                RemoveFile(file);
            }
        }
    }
}

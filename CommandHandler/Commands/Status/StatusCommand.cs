using System;
using System.Collections.Generic;
using System.Linq;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.Status
{
    public class StatusCommand : BaseCommand, IStatusCommand
    {
        private readonly RepositoryXMLHelper repoHelper;

        public StatusCommand(RepositoryXMLHelper repoHelper)
        {
            this.repoHelper = repoHelper;
        }

        public void Execute(ICollection<string> args)
        {
            if (string.IsNullOrEmpty(repoHelper.Project.Name))
            {
                ch.WriteLine("You need to initialize a repository first.", ConsoleColor.Red);
                return;
            }
            repoHelper.CheckForNewCommitSection();
            repoHelper.RemoveRepitionFromNewCommit();
            var repoView = repoHelper.GetRepositoryFilesFromXML();
            var files = repoHelper.GetFiles(repoHelper.Project.Path);
            var newCommitFiles = repoHelper.GetNewCommitFiles();

            repoHelper.ClearIndexFromRemovedFiles(files.Select(f => f.ShotFileName(repoHelper.Project.Path)),newCommitFiles);

            foreach (var file in files)
            {
                if (repoView.Any(f => f.Name == file.ShotFileName(repoHelper.Project.Path) && f.LAstWriteTime == file.LastWriteTime))
                    continue;

                if (repoView.Any(f => f.Name == file.ShotFileName(repoHelper.Project.Path) && f.LAstWriteTime != file.LastWriteTime))
                {
                    ch.WrtieModified(file.ShotFileName(repoHelper.Project.Path), ConsoleColor.Red);
                    continue;
                }

                if (repoView.All(f => f.Name != file.ShotFileName(repoHelper.Project.Path))
                    && newCommitFiles.All(c => c.Name != file.ShotFileName(repoHelper.Project.Path)))
                {
                    ch.WrtieAdded(file.ShotFileName(repoHelper.Project.Path), ConsoleColor.Red);
                    continue;
                }

                if (newCommitFiles.Any(f => f.Name == file.ShotFileName(repoHelper.Project.Path) && f.LAstWriteTime == file.LastWriteTime))
                {
                    var model =
                        newCommitFiles.First(f => f.Name == file.ShotFileName(repoHelper.Project.Path) && f.LAstWriteTime == file.LastWriteTime);
                    ch.WriteLine(string.Format("\t{0}: {1}", model.Status, model.Name), ConsoleColor.Green);
                    continue;
                }

                ch.WriteLine(string.Format("\twtf?      {0}", file.ShotFileName(repoHelper.Project.Path)), ConsoleColor.DarkCyan);
            }

            foreach (var model in repoView)
            {
                if (files.All(f => f.ShotFileName(repoHelper.Project.Path) != model.Name)
                    && newCommitFiles.All(c => c.Name != model.Name))
                {
                    ch.WrtieRemoved(model.Name,ConsoleColor.Red);
                }
                else if (newCommitFiles.Any(c => c.Name == model.Name))
                {
                    ch.WrtieRemoved(model.Name,ConsoleColor.Green);
                }
            }
        }
    }
}

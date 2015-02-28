using System;
using System.Collections.Generic;
using System.IO;
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
            var repoView = repoHelper.GetRepositoryFilesFromXML();
            var files = repoHelper.GetFiles(repoHelper.Project.Path);
            repoHelper.CheckForNewCommitSection();
            var newCommitFiles = repoHelper.GetNewCommitFiles();

            foreach (var file in files)
            {
                if(repoView.Any( f=> f.FullName == file.FullName && f.LAstWriteTime == file.LastWriteTime))
                    continue;

                if (repoView.Any(f => f.FullName == file.FullName && f.LAstWriteTime != file.LastWriteTime))
                {
                    ch.WriteLine(string.Format("\t modified: {0}",file.FullName),ConsoleColor.Red);
                    continue;
                }

                if (repoView.All(f => f.FullName != file.FullName))
                {
                    ch.WriteLine(string.Format("\t added: {0}",file.FullName),ConsoleColor.Red);
                    continue;
                }

                if (newCommitFiles.Any(f => f.FullName == file.FullName && f.LAstWriteTime == file.LastWriteTime))
                {
                    var model =
                        newCommitFiles.First(f => f.FullName == file.FullName && f.LAstWriteTime == file.LastWriteTime);
                    ch.WriteLine(string.Format("\t {0}: {1}",model.Status,model.FullName),ConsoleColor.Green);
                    continue;
                }

                ch.WriteLine(string.Format("\t wtf? {0}",file.FullName),ConsoleColor.DarkCyan);
            }

            Console.WriteLine("");
        }

        private void CheckForRemoval(IEnumerable<FileInfo> files)
        {
            var repoFiles = repoHelper.GetRepositoryFilesFromXML();
            foreach (var repoFile in repoFiles)
            {
                if (files.All(f => f.FullName != repoFile.FullName))
                {
                    ch.WriteLine(string.Format("\t removed: {0}", repoFile.FullName), ConsoleColor.Red);
                }
            }
        }
    }
}

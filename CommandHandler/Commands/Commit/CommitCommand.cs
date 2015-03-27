using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CommandHandler.Commands.Common;
using CommandHandler.Entites;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.Commit
{
    public class CommitCommand : BaseCommand, ICommitCommand
    {
        private readonly RepositoryXMLHelper repository;
        private readonly HttpHelper httpHelper;
        private readonly AntilStorageHelper storageHelper;

        public CommitCommand(RepositoryXMLHelper repository,
            HttpHelper httpHelper,
            AntilStorageHelper storageHelper)
        {
            this.repository = repository;
            this.httpHelper = httpHelper;
            this.storageHelper = storageHelper;
        }

        public void Execute(ICollection<string> args)
        {
            if (args.Count < 1)
            {
                ch.WriteLine("Not enough args ", ConsoleColor.Red);
                return;
            }

            List<FileViewModel> files = null;
            var commitName = args.ToList()[0];
            string parent = string.Empty;
            string comment = string.Empty;

            if (args.ToList()[0] == "init")
            {
                files = repository.GetiInitFiles().ToList();
                comment = "Initial commit";
            }
            else
            {
                repository.RemoveRepitionFromNewCommit();
                repository.ClearIndexFromRemovedFiles(repository.GetFiles(repository.Project.Path)
                    .Select(f => f.ShotFileName(repository.Project.Path)), repository.GetNewCommitFiles());
                files = repository.GetNewCommitFiles().ToList();
                comment = args.Count > 1 ? args.ToList()[0] : string.Empty;
                parent = repository.GetParentCommitId();
            }

            var respseInfo = (ResponseInfo)httpHelper.SendMeta(commitName, parent, storageHelper.UserName, repository.Project.Name, comment);

            if (respseInfo.StatusCode == HttpStatusCode.OK)
            {
                var fileCounter = 0;
                foreach (var file in files)
                {
                    var fileResponse = httpHelper.SendFile(file, respseInfo.CommitId);
                    if (fileResponse.StatusCode != HttpStatusCode.OK)
                    {
                        ch.WriteLine(fileResponse.Description, ConsoleColor.Red);
                        continue;
                    }
                    ++fileCounter;
                }

                ch.WriteLine(string.Format("{0}/{1} files was added", fileCounter, files.Count));

                if (args.ToList()[0] != "init")
                    repository.UpdateNewCommitSection(commitName, respseInfo.CommitId);
            }
            else
            {
                ch.WriteLine(respseInfo.Description, ConsoleColor.Red);
            }
        }
    }
}

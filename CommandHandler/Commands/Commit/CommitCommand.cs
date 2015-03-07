using System.Collections.Generic;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.Commit
{
    public class CommitCommand : BaseCommand, ICommitCommand
    {
        private RepositoryXMLHelper repository;

        public CommitCommand(RepositoryXMLHelper repository)
        {
            this.repository = repository;
        }

        public void Execute(ICollection<string> args)
        {
            repository.RemoveRepitionFromNewCommit();
            // дальше должна быть передача на сервер
        }
    }
}

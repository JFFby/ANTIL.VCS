using System.Collections.Generic;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.List
{
    public class ListCommand : BaseCommand, IListCommand
    {
        private readonly AntilStorageHelper storageHelper;

        public ListCommand(AntilStorageHelper storageHelper)
        {
            this.storageHelper = storageHelper;
        }

        public void Execute(ICollection<string> args)
        {
            var currentProj = storageHelper.GetProjectName().Replace("[","").Replace("]","").Trim();
            foreach (var project in storageHelper.GetProjects())
            {
                if (project.Name == currentProj)
                {
                    ch.WriteLine("* "+project.Name);
                    continue;
                }

                ch.WriteLine("  " + project.Name);
            }
        }
    }
}

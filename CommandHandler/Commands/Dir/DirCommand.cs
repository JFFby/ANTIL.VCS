using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.Dir
{
    public class DirCommand : BaseCommand, IDirCommand
    {
        private readonly AntilStorageHelper storageHelper;

        public DirCommand(AntilStorageHelper storageHelper)
        {
            this.storageHelper = storageHelper;
        }

        public void Execute(ICollection<string> args)
        {
            var path = storageHelper.GetCdPath();

            if (path != string.Empty)
            {
                var dir = new DirectoryInfo(path);

                if (dir.Exists)
                {
                    int f = 0, d = 0;
                    Console.WriteLine(" ");

                    if (dir.GetDirectories().Count() > 1)
                        ch.WriteLine("Directories: ");
                    foreach (var subDir in dir.GetDirectories())
                    {
                        ch.WriteLine(" + " + subDir.Name);
                        ++d;
                    }

                    if (dir.GetFiles().Count() > 1)
                        ch.WriteLine("Files:");

                    foreach (var file in dir.GetFiles())
                    {
                        ch.WriteLine("   " + file.Name);
                        ++f;
                    }

                    ch.WriteLine(string.Format("{0} directories and {1} files", d, f));
                    Console.WriteLine(" ");

                    return;
                }
            }

            ch.WriteLine("Some error", ConsoleColor.Red);
        }
    }
}

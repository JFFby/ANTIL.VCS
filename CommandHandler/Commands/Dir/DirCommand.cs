using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using CommandHandler.Commands.Common;

namespace CommandHandler.Commands.Dir 
{
    public class DirCommand : BaseCommand, IDirCommand
    {
        public void Execute(ICollection<string> args)
        {
            var antilPath = "ANTIL.xml";
            var doc = XDocument.Load(antilPath);
            var path = doc.Element("Cd").Attribute("path").Value;

            if (path != string.Empty)
            {
                var dir = new DirectoryInfo(path);

                if (dir.Exists)
                {
                    int f = 0, d = 0;
                    Console.WriteLine(" ");
                    ch.WriteLine("Directories: ");
                    foreach (var subDir in dir.GetDirectories())
                    {
                       ch.WriteLine(" + " + subDir.Name);
                        ++d;
                    }

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

            ch.WriteLine("Some error",ConsoleColor.Red);
        }
    }
}

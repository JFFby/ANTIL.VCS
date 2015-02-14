using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;
using System.Xml.Linq;
using System.IO;

namespace CommandHandler.Commands.Remove
{
    public class RemoveCommand : BaseCommand, IRemoveCommand
    {
        private XDocument doc;
        private CommitXmlHelper commitHelper;

        public RemoveCommand(CommitXmlHelper commitHelper)
        {
            this.commitHelper = commitHelper;
        }

        public void Execute(ICollection<string> args)
        {
            if (commitHelper.Project == null)
            {
                ch.WriteLine("You need to initialize a repository first.", ConsoleColor.Red);
                return;
            }

            doc = commitHelper.Document;

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
                FileInfo file = new FileInfo(commitHelper.Project.Path + args.ToArray()[0]);
                if (file.Exists)
                    RemoveFile(file);
                else
                {
                    ch.WriteLine("There's no such file in commit!", ConsoleColor.Red);
                    return;
                }
            }

            doc.Save(commitHelper.Project.Path + ".ANTIL\\commit.xml");
        }

        private void RemoveFile(FileInfo file)
        {
            foreach (var element in doc.Element("commit").Element("files").Elements("file")
                ?? new XElement[] { })
            {
                if (element.Element("fullName").Value == file.FullName.ToString())
                {
                    element.Remove();
                    ch.WriteLine(string.Format("\t{0} was  removed from commit.", file.Name));
                }
            }
        }

        private void RemoveAllFiles()
        {
            IEnumerable<FileInfo> files = commitHelper.GetFilesFromXml();
            foreach (var file in files)
            {
                RemoveFile(file);
            }
        }
    }
}

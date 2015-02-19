using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;
using CommandHandler.Entites;
using System.Xml.Linq;
using System.IO;

namespace CommandHandler.Commands.Add
{
    public class AddCommand : BaseCommand, IAddCommand
    {
        private CommitXmlHelper commitHelper;
        private XDocument doc;

        public AddCommand(CommitXmlHelper commitHelper)
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
                AddAllFiles();
            }
            else
            {
                FileInfo file = new FileInfo(commitHelper.Project.Path + args.ToArray()[0]);
                if (file.Exists)
                    AddFile(file);
                else
                {
                    ch.WriteLine("File does not exist!", ConsoleColor.Red);
                    return;
                }
            }

            doc.Save(commitHelper.Project.Path + ".ANTIL\\commit.xml");
        }

        private void AddFile(FileInfo file)
        {
            foreach (var element in doc.Element("commit").Element("files").Elements("file"))
            {
                if (element.Element("fullName").Value == file.FullName.ToString())
                    if (element.Element("date").Value == file.LastWriteTime.ToString())
                        return;
                    else element.Remove();
            }
            doc.Element("commit").Element("files").Add(
                new XElement("file",
                    new XElement("fullName", file.FullName),
                    new XElement("name", file.Name),
                    new XElement("extention", file.Extension),
                    new XElement("date", file.LastWriteTime.ToString())
                ));
            ch.WriteLine(string.Format("\t {0} was added", file.Name));
        }

        private void AddAllFiles()
        {
            IEnumerable<FileInfo> files = commitHelper.GetFiles();
            foreach (var file in files)
            {
                AddFile(file);
            }
        }
    }
}

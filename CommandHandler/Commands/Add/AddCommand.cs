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
        private AntilStorageHelper storageHelper;
        private RepositoryXMLHelper repositoryHelper;
        private XDocument doc;
        private AntilProject project { get { return storageHelper.GetProject(); } }

        public AddCommand(AntilStorageHelper storageHelper, RepositoryXMLHelper repositoryHelper)
        {
            this.storageHelper = storageHelper;
            this.repositoryHelper = repositoryHelper;
            string docPath = "commit.xml";
            try
            {
                doc = XDocument.Load(project.Path + ".ANTIL\\" + docPath);
            }
            catch (FileNotFoundException)
            {
                doc = new XDocument(new XElement("commit",
                    new XElement("parentCommit"),
                    new XElement("projectName", project.Name),
                    new XElement("files")
                    ));
            }
        }

        public void Execute(ICollection<string> args)
        {
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
                FileInfo file = new FileInfo(project.Path + args.ToArray()[0]);
                if (file.Exists)
                    AddFile(file);
                else
                {
                    ch.WriteLine("File does not exist!", ConsoleColor.Red);
                    return;
                }
            }

            doc.Save(project.Path + ".ANTIL\\commit.xml");
        }

        private void AddFile(FileInfo file)
        {
            foreach (var element in doc.Element("commit").Element("files").Elements("file"))
            {
                if (element.Element("fullName").Value == file.FullName.ToString()
                    && element.Element("date").Value == file.LastWriteTime.ToString())
                    return;
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
            IEnumerable<FileInfo> files = repositoryHelper.GetFiles(project.Path);
            foreach (var file in files)
            {
                AddFile(file);
            }
        }
    }
}

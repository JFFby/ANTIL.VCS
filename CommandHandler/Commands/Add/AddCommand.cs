using System;
using System.Collections.Generic;
using System.Linq;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;
using System.Xml.Linq;
using System.IO;

namespace CommandHandler.Commands.Add
{
    public class AddCommand : BaseCommand, IAddCommand
    {
        private readonly RepositoryXMLHelper repositoryhHelper;

        public AddCommand(RepositoryXMLHelper repositoryhHelper)
        {
            this.repositoryhHelper = repositoryhHelper;
        }

        public void Execute(ICollection<string> args)
        {
            if (string.IsNullOrEmpty(repositoryhHelper.Project.Name))
            {
                ch.WriteLine("You need to initialize a repository first.", ConsoleColor.Red);
                return;
            }

            if (args.Count != 1)
            {
                ch.WriteLine("Bad arguments. Type \"help\" to see the reference");
                return;
            }
            else if (args.ToArray()[0].ToLower() == "-a")
            {
                AddAllFiles();
            }
            else
            {
                FileInfo file = new FileInfo(repositoryhHelper.Project.Path + args.ToArray()[0]);
                if (file.Exists)
                {
                    int missValue = 1;
                    AddFile(file, ref missValue, true);
                }
                else
                {
                    ch.WriteLine("File does not exist!", ConsoleColor.Red);
                    return;
                }
            }
        }

        private void AddFile(FileInfo file, ref int fileCounter, bool single = false)
        {
            if (IsAlredyInIndex(file, single))
            {
                return;
            }

            var version = 1;
            var status = "added";
            var commits = repositoryhHelper.RemoveNewCommitSection(GetCommitsWithFile(file.FullName));
            if (commits.Any())
            {
                var maxId = commits.Max(c => Int32.Parse(c.Attribute("id").Value));
                var fileMeta = commits.First(x => x.Attribute("id").Value == maxId.ToString())
                    .Elements("File").First(e => e.Element("fullName").Value == file.FullName);
                var date = DateTime.Parse(fileMeta.Element("lwt").Value);
                if (date == file.LastWriteTime)
                {
                    if (single)
                        ch.WriteLine("File wasn't change", ConsoleColor.Red);
                    return;
                }

                version = Int32.Parse(fileMeta.Element("version").Value) + 1;
                status = "modified";
            }

            var doc = repositoryhHelper.CheckForNewCommitSection();
            doc.Descendants("Commit").First(c => c.Attribute("id").Value == "new").Add(
                new XElement("File",
              new XElement("name", file.Name),
              new XElement("fullName", file.FullName),
              new XElement("path", file.DirectoryName),
              new XElement("lwt", file.LastWriteTime),
              new XElement("directory", file.Directory.Name),
              new XElement("lenght", file.Length),
              new XElement("status", status),
              new XElement("version", version)));
            ++fileCounter;

            doc.Save(repositoryhHelper.PathToSave);
            ch.WriteLine(string.Format("\t {0} was {1}", file.FullName, status), ConsoleColor.Green);
        }

        private void AddAllFiles()
        {
            IEnumerable<FileInfo> files = repositoryhHelper.GetFiles(repositoryhHelper.Project.Path);
            var counter = 0;
            foreach (var file in files)
            {
                AddFile(file,ref counter);
            }
            //CheckForRemoval(files, ref counter);
            if(counter < 1)
                ch.WriteLine("Nothing to update");
        }

        private IEnumerable<XElement> GetCommitsWithFile(string fullName)
        {
            return repositoryhHelper.Document.Descendants("File")
                .Where(x => x.Element("fullName").Value == fullName)
                .Select(x => x.Parent).ToList();
        }

        /// <summary>
        /// Изначально писала в комманде add -a список удалённых файлов, но что-то я подумал и решил,
        /// что наверно надо эту функцию в status надо перенисти, так что не киляй, И если будешь делать статус,
        /// знай что она туту есть :) 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="counter"> для того, чтобы написть, что репозиторий не менялся с последнего коммита</param>
        private void CheckForRemoval(IEnumerable<FileInfo> files, ref int counter)
        {
            var repoFiles = repositoryhHelper.GetRepositoryFilesFromXML();
            foreach (var repoFile in repoFiles)
            {
                if (files.All(f => f.FullName != repoFile.FullName))
                {
                    ++counter;
                    ch.WriteLine(string.Format("\t {0} was removed", repoFile.FullName),ConsoleColor.Red);
                }
            }
        }

        private bool IsAlredyInIndex(FileInfo file, bool single = false)
        {
            var result = false;
            var doc = repositoryhHelper.Document;
            var newCommit = doc.Descendants("Commit")
                .FirstOrDefault(c => c.Attribute("id").Value == "new");

            if (newCommit == null)
                return result;

            var commitFiles = newCommit.Descendants("File").ToList();
            XElement xmlMeta = commitFiles.FirstOrDefault(e => e.Element("fullName").Value == file.FullName);

            if (xmlMeta != null)
            {
                var lwt = DateTime.Parse(xmlMeta.Element("lwt").Value);
                if (lwt == file.LastWriteTime)
                {
                    if (single)
                        ch.WriteLine(string.Format("/t {0} is alreay in index", file.FullName),ConsoleColor.Red);
                    result = true;
                }
                else
                {
                    xmlMeta.Element("lwt").Value = file.FullName;
                    ch.WriteLine(string.Format("/t {0}.Index was updated", file.FullName),ConsoleColor.Green);
                    result = true;
                }
            }

            return result;
        }
    }
}

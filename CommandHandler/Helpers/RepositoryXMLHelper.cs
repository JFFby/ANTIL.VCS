using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CommandHandler.Entites;

namespace CommandHandler.Helpers
{
    public class RepositoryXMLHelper
    {
        private readonly string storageName = "AntilProject.xml";
        private readonly AntilStorageHelper storageHelper;

        public RepositoryXMLHelper(AntilStorageHelper storageHelper)
        {
            this.storageHelper = storageHelper;
        }

        public string PathToSave { get { return Project.Path + ".ANTIL\\" + storageName; } }

        public XDocument Document
        {
            get
            {
                return XDocument.Load(Project.Path + ".ANTIL\\" + storageName);
            }
        }

        public AntilProject Project
        {
            get { return storageHelper.GetProject(); }
        }

        public void CreateRepoStorage(string path, string projName)
        {
            var files = GetFiles(path);

            var doc = new XDocument(new XElement("AntilProject", 
                new XAttribute("name", projName), new XAttribute("currentCommitId","1"),
                new XElement("Commit", new XAttribute("name", "init"), new XAttribute("id", "1"),
              files.Select(f => new XElement("File",
                  new XElement("name", f.Name),
                  new XElement("fullName", f.ShotFileName(Project.Path)),
                  new XElement("lwt", f.LastWriteTime),
                  new XElement("lenght", f.Length),
                  new XElement("status", "added"),
                  new XElement("version", 1))))
            ));
            doc.Save(PathToSave);
        }

        public string ProcessProjectName(string path, ICollection<string> args)
        {
            if (args.Count > 0)
            {
                return args.ToList()[0];
            }

            var argsArray = path.Split('\\');
            return argsArray[argsArray.Length - 2];
        }

        public IEnumerable<FileInfo> GetFiles(string path)
        {
            var directory = new DirectoryInfo(path.Replace(".ANTIL", ""));
            var dirs = GetDirs(directory);
            var files = new List<FileInfo>(directory.GetFiles());
            foreach (var dir in dirs)
            {
                files.AddRange(dir.GetFiles());
            }

            return files;
        }

        public XDocument CheckForNewCommitSection()
        {
            var doc = Document;
            var newCommitSection = doc.Descendants("Commit")
                .FirstOrDefault(c => c.Attribute("id").Value == "new");
            if (newCommitSection == null)
            {
                doc.Element("AntilProject").Add(
                    new XElement("Commit",
                        new XAttribute("id", "new"),
                        new XAttribute("name", "new")));
                doc.Save(PathToSave);
            }

            return doc;
        }

        private IEnumerable<DirectoryInfo> GetDirs(DirectoryInfo dir)
        {

            return CollectDirs(new List<DirectoryInfo>(), dir.GetDirectories().Where(x => x.Name != ".ANTIL"));
        }

        private IEnumerable<DirectoryInfo> CollectDirs(List<DirectoryInfo> collectedDirs,
            IEnumerable<DirectoryInfo> dirs)
        {
            foreach (var directoryInfo in dirs)
            {
                collectedDirs.Add(directoryInfo);
                if (directoryInfo.GetDirectories().Any())
                    CollectDirs(collectedDirs, directoryInfo.GetDirectories());
            }
            return collectedDirs;
        }

        public IList<FileViewModel> GetNewCommitFiles()
        {
            var newCommitSection = Document.Descendants("Commit").First(e => e.Attribute("id").Value == "new");
            return GetFilesFromXmlNode(newCommitSection);

        }

        private IList<FileViewModel> GetFilesFromXmlNode(XElement element)
        {
            var newCommitFiles = new List<FileViewModel>();
            foreach (var file in element.Elements("File"))
            {
                newCommitFiles.Add(MapXmlFileToViewModel(file, true));
            }

            return newCommitFiles;
        }

        public List<FileViewModel> GetRepositoryFilesFromXML()
        {
            var doc = Document;
            var commits = doc.Descendants("Commit");
            commits = RemoveNewCommitSection(commits)
                .OrderByDescending(c => Int32.Parse(c.Attribute("id").Value));
            var repoFiles = new List<FileViewModel>();
            foreach (var commit in commits)
            {
                foreach (var file in commit.Elements("File"))
                {
                    var fileModel = MapXmlFileToViewModel(file, false);

                    if (repoFiles.All(f => f.Name != fileModel.Name))
                    {
                        repoFiles.Add(fileModel);
                    }
                    else if (fileModel.Status == "removed")
                    {
                        repoFiles.Remove(fileModel);
                    }
                    else
                    {
                        repoFiles.First(f => f.Name == fileModel.Name).Update(fileModel);
                    }
                }
            }

            return repoFiles;
        }

        public IEnumerable<XElement> RemoveNewCommitSection(IEnumerable<XElement> commits)
        {
            if (commits.Any(e => e.Attribute("id").Value == "new"))
            {
                commits = commits.Where(e => e.Attribute("id").Value != "new").ToList();
            }
            return commits;
        }

        public XElement GetNewCommitSection(XDocument doc)
        {
            return doc.Descendants("Commit")
                 .FirstOrDefault(c => c.Attribute("id").Value == "new");
        }

        public FileViewModel MapXmlFileToViewModel(XElement file, bool isNew)
        {
            return new FileViewModel(Project.Path)
            {
                Name = file.Element("fullName").Value,
                Version = Int32.Parse(file.Element("version").Value),
                Status = file.Element("status").Value,
                LAstWriteTime = DateTime.Parse(file.Element("lwt").Value),
                CommitId = isNew ? -1 : Int32.Parse(file.Parent.Attribute("id").Value)
            };
        }

        public bool AddRemovedFileInIndex(FileViewModel file)
        {
            if (CheckForRemovedAlreadyInIdex(file))
                return false;

            var doc = Document;
            var commit = doc.Descendants("Commit").First(e => e.Attribute("id").Value == file.CommitId.ToString());
            var fileMeta = commit.Elements("File").First(e => e.Element("fullName").Value == file.Name);
            fileMeta.Element("status").Value = "removed";
            fileMeta.Element("lwt").Value = DateTime.Now.ToString("O");
            var newCommit = doc.Descendants("Commit").First(e => e.Attribute("id").Value == "new");
            newCommit.Add(fileMeta);
            doc.Save(PathToSave);
            return true;
        }

        private bool CheckForRemovedAlreadyInIdex(FileViewModel file)
        {
            return GetNewCommitFiles().Any(model => file.Name == model.Name && file.Status == model.Status);
        }

        public void RemoveRepitionFromNewCommit()
        {
            var files = GetNewCommitFiles();
            if (files.Count != files.Distinct(new FileViewModelNameComparer()).Count())
            {
                var copies = files.GroupBy(f => f.Name).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
                var doc = Document;
                var newCommit = doc.Descendants("Commit").First(c => c.Attribute("id").Value == "new");
                foreach (var copy in copies)
                {
                    var sameFiles = newCommit.Elements("File").Where(e => e.Element("fullName").Value == copy);
                    sameFiles.OrderBy(f => DateTime.Parse(f.Element("lwt").Value))
                          .Take(sameFiles.Count() - 1).ToList().ForEach(e => e.Remove());
                }

                doc.Save(PathToSave);
            }
        }

        /// <summary>
        /// ! добавляем файл в репозиторий -> add -a -> удаляем файл -> add -a -> в новый коммит не заноситься
        /// инфа о том что удалили файл, файл остаётся висеть как только что добавленный
        /// fix
        /// </summary>
        public void ClearIndexFromRemovedFiles(IEnumerable<string> fileNames, IEnumerable<FileViewModel> ncFiles)
        {
            RemoveFromNewCommit(ncFiles.Where(f => !fileNames.Contains(f.Name) && f.Status != "removed")
                .Select(f => f.Name));
        }

        private void RemoveFromNewCommit(IEnumerable<string> fileNames)
        {
            var doc = Document;
            var ncs = GetNewCommitSection(doc);
            ncs.Elements("File").Where(e => fileNames.Contains(e.Element("fullName").Value)).ToList().ForEach(e => e.Remove());
            doc.Save(PathToSave);
        }

        public IList<FileViewModel> GetiInitFiles()
        {
            var initSection = Document.Descendants("Commit").First(e => e.Attribute("name").Value == "init");
            return GetFilesFromXmlNode(initSection);
        }

        public string GetParentCommitId()
        {
            return Document.Root.Attribute("currentCommitId").Value;
        }

        public void UpdateNewCommitSection(string name, string id)
        {
            var doc = Document;
            var newCommit = GetNewCommitSection(doc);
            newCommit.Attribute("name").Value = name;
            newCommit.Attribute("id").Value = id;
            doc.Save(PathToSave);
        }
    }
}

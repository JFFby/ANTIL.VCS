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
                return  XDocument.Load(Project.Path + ".ANTIL\\" + storageName);
            }
        }

        public AntilProject Project
        {
            get { return storageHelper.GetProject(); }
        }

        public string CreateRepoStorage(string path, ICollection<string> args)
        {
            var projName = ProcessProjectName(path, args);
            var files = GetFiles(path);

            var doc = new XDocument(new XElement("AntilProject",new XAttribute("name",projName),
                new XElement("Commit", new XAttribute("name", "init"), new XAttribute("id", "1"),
              files.Select(f => new XElement("File",
                  new XElement("name", f.Name),
                  new XElement("fullName",f.FullName),
                  new XElement("path", f.DirectoryName),
                  new XElement("lwt", f.LastWriteTime),
                  new XElement("directory", f.Directory.Name),
                  new XElement("lenght", f.Length),
                  new XElement("status", "added"),
                  new XElement("version", 1))))
            ));
            doc.Save(PathToSave);

            return projName;
        }

        private string ProcessProjectName(string path,ICollection<string> args)
        {
            if (args.Count>0)
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
                        new XAttribute("id","new"),
                        new XAttribute("name","new")));
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

        public List<FileViewModel> GetNewCommitFiles()
        {
            var doc = Document;
            var newCommitSection = doc.Descendants("Commit").First(e => e.Attribute("id").Value == "new");
            var newCommitFiles = new List<FileViewModel>();
            foreach ( var file in newCommitSection.Elements("File"))
            {
                newCommitFiles.Add(MapXmlFoleToViewModel(file));
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
                    var fileModel = MapXmlFoleToViewModel(file);

                    if (repoFiles.All(f => f.FullName != fileModel.FullName))
                    {
                        repoFiles.Add(fileModel);
                    }
                    else if (fileModel.Status == "removed")
                    {
                        repoFiles.Remove(fileModel);
                    }
                    else
                    {
                        repoFiles.First(f => f.FullName == fileModel.FullName).Update(fileModel);
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
        
        public XElement GetNewCommitSection()
        {
           return Document.Descendants("Commit")
                .FirstOrDefault(c => c.Attribute("id").Value == "new");
        }

        public FileViewModel MapXmlFoleToViewModel(XElement file)
        {
            return new FileViewModel
            {
                FullName = file.Element("fullName").Value,
                Version = Int32.Parse(file.Element("version").Value),
                Status = file.Element("status").Value,
                LAstWriteTime = DateTime.Parse(file.Element("lwt").Value)
            };
        }
    }
}

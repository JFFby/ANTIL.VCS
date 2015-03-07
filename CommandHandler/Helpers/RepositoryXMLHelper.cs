﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CommandHandler.Entites;
using NHibernate.Hql.Ast.ANTLR.Tree;

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

        public string CreateRepoStorage(string path, ICollection<string> args)
        {
            var projName = ProcessProjectName(path, args);
            var files = GetFiles(path);

            var doc = new XDocument(new XElement("AntilProject", new XAttribute("name", projName),
                new XElement("Commit", new XAttribute("name", "init"), new XAttribute("id", "1"),
              files.Select(f => new XElement("File",
                  new XElement("name", f.Name),
                  new XElement("fullName", f.FullName),
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

        private string ProcessProjectName(string path, ICollection<string> args)
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

        public List<FileViewModel> GetNewCommitFiles()
        {
            var doc = Document;
            var newCommitSection = doc.Descendants("Commit").First(e => e.Attribute("id").Value == "new");
            var newCommitFiles = new List<FileViewModel>();
            foreach (var file in newCommitSection.Elements("File"))
            {
                newCommitFiles.Add(MapXmlFoleToViewModel(file, true));
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
                    var fileModel = MapXmlFoleToViewModel(file, false);

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

        public FileViewModel MapXmlFoleToViewModel(XElement file, bool isNew)
        {
            return new FileViewModel
            {
                FullName = file.Element("fullName").Value,
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
            var fileMeta = commit.Elements("File").First(e => e.Element("fullName").Value == file.FullName);
            fileMeta.Element("status").Value = "removed";
            fileMeta.Element("lwt").Value = DateTime.Now.ToString("O");
            var newCommit = doc.Descendants("Commit").First(e => e.Attribute("id").Value == "new");
            newCommit.Add(fileMeta);
            doc.Save(PathToSave);
            return true;
        }

        private bool CheckForRemovedAlreadyInIdex(FileViewModel file)
        {
            return GetNewCommitFiles().Any(model => file.FullName == model.FullName && file.Status == model.Status);
        }

        public void RemoveRepitionFromNewCommit()
        {
            var files = GetNewCommitFiles();
            if (files.Count != files.Distinct(new FileViewModelNameComparer()).Count())
            {
                var copies = files.GroupBy(f => f.FullName).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
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
    }
}

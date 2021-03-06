﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using CommandHandler.Entites;

namespace CommandHandler.Helpers
{
    public class AntilStorageHelper
    {
        private readonly string storagePath = "ANTIL.xml";

        public AntilStoreItem InitAntilStore()
        {
            var file = new FileInfo(storagePath);
            if (!file.Exists)
            {
                var document = new XDocument(new XElement("Antil",
                    new XElement("Cd", new XAttribute("path", string.Empty)),
                    new XElement("Projects"),
                    new XElement("UserName"))
                   );

                document.Save(storagePath);

                return new AntilStoreItem
                {
                    Store = new FileInfo(storagePath),
                    Cd = string.Empty,
                };
            }

            return new AntilStoreItem
            {
                Store = file,
                Cd = GetCdPath()
            };
        }

        public AntilStoreItem GetCdPath(AntilStoreItem item)
        {
            var cd = string.Empty;
            var newFileRevision = new FileInfo(storagePath);

            if (newFileRevision.LastWriteTime == item.Store.LastWriteTime)
                cd = item.Cd;
            else
            {
                cd = GetCdPath();
                item.Store = newFileRevision;

            }

            item.ProjectName = GetProjectName();
            item.Cd = cd;
            return item;
        }

        public void AddProject(string path, string name)
        {
            var doc = XDocument.Load(storagePath);
            var xElement = doc.Root.Element("Projects");
            if (xElement != null)
            {
                xElement.Add(new XElement("Project",
                    new XElement("name", name),
                    new XElement("path", path)));
                doc.Save(storagePath);
            }
        }

        public void SetCd(string value)
        {
            var splitter = String.Empty;
            if (value != string.Empty)
                splitter = value.ToCharArray()[value.Length - 1].ToString() == "\\" ? string.Empty : "\\";
            var doc = XDocument.Load(storagePath);
            doc.Root.Element("Cd").Attribute("path").SetValue(value + splitter);
            doc.Save(storagePath);
        }

        public string GetCdPath()
        {
            var doc = XDocument.Load(storagePath);
            return doc.Root.Element("Cd").Attribute("path").Value;
        }

        public string UserName
        {
            get
            {
                return XDocument.Load(storagePath).Root.Element("UserName").Value;
            }
            set
            {
                var doc = XDocument.Load(storagePath);
                doc.Root.Element("UserName").Value = value;
                doc.Save(storagePath);
            }
        }

        public string GetProjectName()
        {
            var cd = GetCdPath();
            var projects = GetProjects();

            foreach (var project in projects)
            {
                if (cd.IndexOf(project.Path, System.StringComparison.OrdinalIgnoreCase) > -1)
                    return " [" + project.Name + "]";
            }

            return string.Empty;
        }

        public AntilProject GetProject()
        {
            var cd = GetCdPath();
            var projects = GetProjects();

            foreach (var project in projects)
            {
                if (cd.IndexOf(project.Path, System.StringComparison.OrdinalIgnoreCase) > -1)
                    return project;
            }

            return null;
        }

        public IEnumerable<AntilProject> GetProjects()
        {
            var xElement = XDocument.Load(storagePath).Root.Element("Projects");
            if (xElement != null)
            {
                var projects = new List<AntilProject>();
                foreach (var el in xElement.Elements("Project"))
                {
                    projects.Add(new AntilProject
                    {
                        Name = el.Element("name").Value,
                        Path = el.Element("path").Value
                    });
                }

                return projects;
            }

            return new List<AntilProject>();
        }
    }


}
using System.Collections.Generic;
using CommandHandler.Entites;
using System.Xml.Linq;
using System.IO;

namespace CommandHandler.Helpers
{
    public class CommitXmlHelper
    {
        private AntilStorageHelper storageHelper;
        private RepositoryXMLHelper repositoryHelper;
        public AntilProject Project { get { return storageHelper.GetProject(); } }

        public CommitXmlHelper(AntilStorageHelper storageHelper,RepositoryXMLHelper repositoryHelper)
        {
            this.storageHelper = storageHelper;
            this.repositoryHelper = repositoryHelper;
        }

        public XDocument Document
        {
            get
            {
                XDocument doc;
                string docPath = "commit.xml";
                try
                {
                    doc = XDocument.Load(Project.Path + ".ANTIL\\" + docPath);
                }
                catch (FileNotFoundException)
                {
                    doc = new XDocument(new XElement("commit",
                        new XElement("parentCommit"),
                        new XElement("projectName", Project.Name),
                        new XElement("files")
                        ));
                }
                return doc;
            }
        }

        public IEnumerable<FileInfo> GetFiles()
        {
            return repositoryHelper.GetFiles(Project.Path);
        }

        public IEnumerable<FileInfo> GetFilesFromXml()
        {
            List<FileInfo> files = new List<FileInfo>();
            foreach(var element in Document.Element("commit").Element("files").Elements("file"))
            {
                files.Add(new FileInfo(element.Element("fullName").Value));
            }
            return files;
        }

    }
}

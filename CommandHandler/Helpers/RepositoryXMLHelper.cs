using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CommandHandler.Helpers
{
    public class RepositoryXMLHelper
    {
        private readonly string storageName = "AntilProject.xml";
        public void CreateRepoStorage(string path, ICollection<string> args)
        {
            var projName = ProcessProjectName(path, args);
            var subDirs = GetDirs(path);

            var doc = new XDocument(new XElement("AntilProject", new XAttribute("name",projName)
                ));
            doc.Save(path + "//" + storageName);
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

        private IEnumerable<DirectoryInfo> GetDirs(string path)
        {
            var dir = new DirectoryInfo(path.Replace(".ANTIL", ""));
            return CollectDirs(new List<DirectoryInfo>(), dir.GetDirectories().Where(x => x.Name != ".ANTIL")).ToList();
        }

        private IEnumerable<DirectoryInfo> CollectDirs(List<DirectoryInfo> collectedDirs ,IEnumerable<DirectoryInfo> dirs)
        {
            foreach (var directoryInfo in dirs)
            {
                collectedDirs.Add(directoryInfo);
                if (directoryInfo.GetDirectories().Any())
                    CollectDirs(collectedDirs, directoryInfo.GetDirectories()); 
            }
            return collectedDirs;
        } 
    }
}

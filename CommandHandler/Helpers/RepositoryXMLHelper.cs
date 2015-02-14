using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CommandHandler.Helpers
{
    public class RepositoryXMLHelper
    {
        private readonly string storageName = "AntilProject.xml";
        public string CreateRepoStorage(string path, ICollection<string> args)
        {
            var projName = ProcessProjectName(path, args);
            var files = GetFiles(path);

            var doc = new XDocument(new XElement("AntilProject",new XAttribute("name",projName),
              files.Select(f => new XElement("File",
                  new XElement("name", f.Name), 
                  new XElement("path",f.DirectoryName),
                  new XElement("lwt", f.LastWriteTime),
                  new XElement("directory",f.Directory.Name),
                  new XElement("lenght",f.Length)))
            ));
            doc.Save(path + "//" + storageName);

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

        private IEnumerable<DirectoryInfo> GetDirs(DirectoryInfo dir)
        {
           
            return CollectDirs(new List<DirectoryInfo>(), dir.GetDirectories().Where(x => x.Name != ".ANTIL"));
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

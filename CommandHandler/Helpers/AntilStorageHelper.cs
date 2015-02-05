using System.IO;
using System.Xml.Linq;

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
                var document = new XDocument(
                    new XElement("Cd", new XAttribute("path", string.Empty))
                    );

                document.Save(storagePath);

                return new AntilStoreItem
                {
                    Store = new FileInfo(storagePath),
                    Cd = string.Empty
                };
            }
            
            return new AntilStoreItem
            {
                Store = file,
                Cd = GetCdPath()
            };
        }

        public string GetCdPath(AntilStoreItem item)
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

            item.Cd = cd;
            return cd;
        }

        public void SetCd(string value)
        {
            var doc = XDocument.Load(storagePath);
            doc.Element("Cd").Attribute("path").SetValue(value);
            doc.Save(storagePath);
        }

        public string GetCdPath()
        {
            var doc = XDocument.Load(storagePath);
            return doc.Element("Cd").Attribute("path").Value;
        }
    }
}

using System;
using System.IO;
using System.Xml.Linq;

namespace CommandHandler
{
    /// <summary>
    /// Думаю не имеет смысла реализовавывать у CommandHandler никаких интерфейсов ибо
    /// мложно себе представить себе другую реализацию этого класса
    /// З.Ы. как прочтёшь, удаляй
    /// </summary>
    public class CommandHandler
    {
        private readonly Controller controller;
        private readonly ConsoleHelper ch;
        private readonly CommandHandlerHelper cmdHelper;

        public CommandHandler(Controller controller, CommandHandlerHelper cmdHelper)
        {
            this.controller = controller;
            this.cmdHelper = cmdHelper;
            this.ch = new ConsoleHelper();
        }

        public void Run()
        {
            var antilStore = InitAntilStore();

            ch.WriteLine("Hello, I'm ANTIL and I'm Version Control System");
            ch.WriteLine("Use 'help' command to see all my features :)");
            do
            {
                var cd = GetCd(antilStore);
                Console.Write(cd + " > ");
                var command = Console.ReadLine();

                cmdHelper.ExecuteMethod(controller, command);

            } while (true);

        }

        private AntilStoreItem InitAntilStore()
        {
            string path = "ANTIL.xml";

            var file = new FileInfo(path);
            if (!file.Exists)
            {
                var document = new XDocument(
                    new XElement("Cd", new XAttribute("path", string.Empty))
                    );

                document.Save(path);

                return new AntilStoreItem
                {
                    Store = new FileInfo(path),
                    Cd = string.Empty
                };
            }

            string cd = XDocument.Load(path).Element("Cd").Attribute("path").Value;

                return new AntilStoreItem
            {
                Store = file,
                Cd = cd 
            };
        }

        private string GetCd(AntilStoreItem item)
        {
            var path = "ANTIL.xml";
            var cd = string.Empty;
            var newFileRevision = new FileInfo(path);

            if (newFileRevision.LastWriteTime == item.Store.LastWriteTime)
                cd = item.Cd;
            else
            {
                cd = XDocument.Load(path).Element("Cd").Attribute("path").Value;
                item.Store = newFileRevision;
            }

            item.Cd = cd;
            return cd;

        }
    }
}

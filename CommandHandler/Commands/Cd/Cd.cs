using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CommandHandler.Commands.Common;

namespace CommandHandler.Commands.Cd
{
    public class Cd : BaseCommand, ICd
    {
        private readonly CommandHandlerHelper cmdHelper;

        public Cd(CommandHandlerHelper cmdHelper)
        {
            this.cmdHelper = cmdHelper;
        }

        public void Execute(ICollection<string> args)
        {
            if (args.Count > 0 && cmdHelper.IsMethodExist(this, args.ToList()[0]))
            {
                cmdHelper.ExecuteMethod(this, args);
                return;
            }

            ExrcuteCdCommandWithOutArgs(args);
        }

        private void ExrcuteCdCommandWithOutArgs(ICollection<string> args)
        {
            var path = string.Empty;
            if (args.Count > 0 && args.ToList()[0].Length > 0)
                path = args.ToList()[0];
            else
            {
                ch.WriteLine("Bad arguments", ConsoleColor.Red);
                return;
            }

            var dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                var antilPath = "ANTIL.xml";
                ch.WriteLine("You are in " + dir.FullName + " now", ConsoleColor.Green);
                var doc = XDocument.Load(antilPath);
                doc.Element("Cd").Attribute("path").SetValue(dir.FullName);
                doc.Save(antilPath);
            }
            else
            {
                ch.WriteLine("Folder does not exist", ConsoleColor.Red);
            }
        }

        public void Clear(ICollection<string> args)
        {
            var antilPath = "ANTIL.xml";
            var doc = XDocument.Load(antilPath);
            doc.Element("Cd").Attribute("path").SetValue(string.Empty);
            doc.Save(antilPath);

            ch.WriteLine("Current location is clear", ConsoleColor.Green);
        }


    }
}

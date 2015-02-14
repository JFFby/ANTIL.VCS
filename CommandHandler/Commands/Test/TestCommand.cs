using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandHandler.Helpers;
using CommandHandler.Commands.Common;
using System.Net;
using System.IO;

namespace CommandHandler.Commands.Test
{
    public class TestCommand: BaseCommand, ITestCommand
    {
        public void Execute(ICollection<string> args)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:3300/");
            request.Headers.Add("cmd", "Init");
            request.Method = "POST";
            Stream fstream = File.Open(@"E:\Downloads\sendtext.txt", FileMode.Open);
            request.ContentLength = (int)fstream.Length;
            fstream.CopyTo(request.GetRequestStream());
            //byte[] file = new byte[fstream.Length];
            //fstream.Read(file, 0, (int)fstream.Length);
            //request.ContentLength = file.Length;
            //request.ContentType = "file";
            //request.GetRequestStream().Write(file, 0, file.Length);
        }
    }
}

using System;
using System.Net;
using System.Threading.Tasks;
using HttpCommandHandler.Windsdor;
using System.IO;

namespace ANTIL.Server
{
    public class AntilServer
    {
        private readonly HttpListener listner;

        public AntilServer(string url)
        {
            listner = new HttpListener();
            listner.Prefixes.Add(url);
        }

        public void Start()
        {
            listner.Start();
            Console.WriteLine("Welcome to Antil server v1.0 :)");

            while (true)
            {
                var context = listner.GetContext();
                Task.Factory.StartNew(() => Test(context));
                Console.WriteLine("в главном");
            }
        }

        public void Test(HttpListenerContext context)
        {
            var request = context.Request;
            var cmdHendler = IOC.Resolve<HttpCommandHandler.HttpCommandHandler>();
            string command = request.Headers.Get("cmd");
            byte[] file = new byte[request.ContentLength64];
            request.InputStream.Read(file, 0, file.Length);
            File.WriteAllBytes(@"E:\file.txt", file);
            cmdHendler.ExecuteMethod(command);
            //cmdHendler.ExecuteMethod(context.Request.QueryString["cmd"]);
        }

    }
}

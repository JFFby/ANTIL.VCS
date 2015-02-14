using System;
using System.Net;
using System.Threading.Tasks;
using HttpCommandHandler;
using HttpCommandHandler.Windsdor;

namespace ANTIL.Server
{
    public class AntilServer : IDisposable
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
                Task.Factory.StartNew(() => ProcessRequest(context));
            }
        }

        public void ProcessRequest(HttpListenerContext context)
        {
            var cmdHendler = new HttpCommandHandler.HttpCommandHandler(IOC.Resolve<HttpController>());
            cmdHendler.ExecuteMethod(context, context.Request.Headers.Get("cmd"));
        }

        public void Dispose()
        {
            listner.Stop();
        }
    }
}

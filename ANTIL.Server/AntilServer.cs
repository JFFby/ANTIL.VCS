using System;
using System.Net;
using System.Threading.Tasks;
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
                Task.Factory.StartNew(() => Test(context));
                Console.WriteLine("в главном");
            }
        }

        public void Test(HttpListenerContext context)
        {
            var cmdHendler = IOC.Resolve<HttpCommandHandler.HttpCommandHandler>();
            cmdHendler.ExecuteMethod(context);
        }

        public void Dispose()
        {
            listner.Stop();
        }
    }
}

using System;
using System.Net;
using System.Reflection;

namespace HttpCommandHandler
{
    public class HttpCommandHandler
    {
        private readonly HttpController controller;

        public HttpCommandHandler(HttpController controller)
        {
            this.controller = controller;
        }

        public void ExecuteMethod(HttpListenerContext context)
        {
            //MethodInfo mi = controller.GetType().GetMethod(context.Request.Headers.Get("cmd"));
            MethodInfo mi = controller.GetType().GetMethod(context.Request.QueryString["cmd"]);
            if (mi != null)
            {
                mi.Invoke(controller, new object[] { context });
            }
            else
            {
                Console.WriteLine("Fail execute");
            }
        }
    }
}

using System;
using System.Net;
using System.Reflection;

namespace HttpCommandHandler
{
    public class HttpCommandHandler
    {
        private readonly object controller;

        public HttpCommandHandler(object controller)
        {
            this.controller = controller;
        }

        public void ExecuteMethod(HttpListenerContext context, string cmd)
        {
            MethodInfo mi = controller.GetType().GetMethod(cmd);
            if (mi != null)
            {
                mi.Invoke(controller, new object[] { context });
            }
            else
            {
                Console.WriteLine("Execute fail");
            }
        }
    }
}

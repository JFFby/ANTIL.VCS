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

        public void ExecuteMethod(HttpListenerContext context, string cmd, object obj = null)
        {
            obj = obj ?? controller;

            MethodInfo mi = obj.GetType().GetMethod(cmd);
            if (mi != null)
            {
                mi.Invoke(obj, new object[] { context });
            }
            else
            {
                context.Response.StatusCode = 204;
                context.Response.StatusDescription = "server response: Execute fail";
                context.Response.Close();
                Console.WriteLine("Execute fail");
            }
        }
    }
}

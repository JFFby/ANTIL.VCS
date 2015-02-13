using System;
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

        public void ExecuteMethod(string cmd)
        {
            MethodInfo mi = controller.GetType().GetMethod(cmd);
            if (mi != null)
            {
                mi.Invoke(controller, null);
            }
            else
            {
                Console.WriteLine("Fail execute");
            }
        }
    }
}

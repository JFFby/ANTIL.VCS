using System;
using System.Net;
using HttpCommandHandler.Commands.Registration;

namespace HttpCommandHandler
{
    public class HttpController
    {
        private readonly IRegistration registration;

        public HttpController(IRegistration registration)
        {
            this.registration = registration;
        }

        public void Registration(HttpListenerContext context)
        {
            registration.Execute(context);
        } 
    }
}

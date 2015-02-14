using System.Net;
using HttpCommandHandler.Commands.Authorization;
using HttpCommandHandler.Commands.Push;
using HttpCommandHandler.Commands.Registration;

namespace HttpCommandHandler
{
    public class HttpController
    {
        private readonly IRegistrationCommand registration;
        private readonly IAuthorizationCommand authorization;
        private readonly IPushCommand push;

        public HttpController(IRegistrationCommand registration,
            IAuthorizationCommand authorization,
            IPushCommand push)
        {
            this.registration = registration;
            this.authorization = authorization;
            this.push = push;
        }

        public void Registration(HttpListenerContext context)
        {
            registration.Execute(context);
        }

        public void Authorization(HttpListenerContext context)
        {
            authorization.Execute(context);
        }

        public void Push(HttpListenerContext context)
        {
            push.Execute(context);
        }
    }
}

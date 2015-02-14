using System.Net;
using HttpCommandHandler.Commands.Authorization;
using HttpCommandHandler.Commands.Registration;

namespace HttpCommandHandler
{
    public class HttpController
    {
        private readonly IRegistrationCommand registration;
        private readonly IAuthorizationCommand authorization;

        public HttpController(IRegistrationCommand registration,
            IAuthorizationCommand authorization)
        {
            this.registration = registration;
            this.authorization = authorization;
        }

        public void Registration(HttpListenerContext context)
        {
            registration.Execute(context);
        }

        public void Authorization(HttpListenerContext context)
        {
            authorization.Execute(context);
        }
    }
}

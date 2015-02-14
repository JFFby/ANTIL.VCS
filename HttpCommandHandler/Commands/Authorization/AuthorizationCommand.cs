using System;
using System.Net;
using ANTIL.Domain.Dao.Interfaces;

namespace HttpCommandHandler.Commands.Authorization
{
    public class AuthorizationCommand : IAuthorizationCommand
    {
        private readonly IUserDao userDao;

        public AuthorizationCommand(IUserDao userDao)
        {
            this.userDao = userDao;
        }

        public void Execute(HttpListenerContext context)
        {
            try
            {
                var login = context.Request.Headers.Get("userName");
                var pass = context.Request.Headers.Get("password");
                if (userDao.IsExistUser(login, pass))
                {
                    var response = context.Response;
                    response.StatusCode = 200;
                    response.StatusDescription = "Ok";
                    Console.WriteLine("User was autirize");
                }
                else
                {
                    context.Response.StatusCode = 500;
                    context.Response.StatusDescription = "Fail";
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.StatusDescription = ex.Message;
            }
            finally
            {
                context.Response.Close();
            }
        }
    }
}

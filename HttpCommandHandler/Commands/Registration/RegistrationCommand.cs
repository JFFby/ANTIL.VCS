using System;
using System.Net;
using ANTIL.Domain.Core.Entities;
using ANTIL.Domain.Dao.Interfaces;

namespace HttpCommandHandler.Commands.Registration
{
    public class RegistrationCommand : IRegistrationCommand
    {
        private readonly IUserDao userDao;
        public RegistrationCommand(IUserDao userDao)
        {
            this.userDao = userDao;
        }

        public void Execute(HttpListenerContext context)
        {
            try
            {
                var user = new User
                {
                    UserName = context.Request.Headers.Get("userName"),
                    Password = context.Request.Headers.Get("password")
                };

                userDao.Save(user);

                Console.WriteLine("User was added");

                var response = context.Response;
                response.StatusCode = 200;
                response.StatusDescription = "Ok";
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.StatusCode = 500;
                response.StatusDescription = ex.Message;
            }
            finally
            {
                context.Response.Close();
            }
        }
    }
}

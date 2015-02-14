using System;
using System.Net;
using ANTIL.Domain.Core.Entities;
using ANTIL.Domain.Dao.Interfaces;

namespace HttpCommandHandler.Commands.Registration
{
    public class Registration : IRegistration
    {
        private readonly IUserDao userDao;
        public Registration(IUserDao userDao)
        {
            this.userDao = userDao;
        }

        public void Execute(HttpListenerContext context)
        {
            //var user = new User
            //{
            //    UserName = context.Request.Headers.Get("userName"),
            //    Password = context.Request.Headers.Get("password")
            //};
            var user = new User
            {
                Password = "dima",
                UserName = "dron"
            };

            userDao.Save(user);

            Console.WriteLine("User added");
        }
    }
}

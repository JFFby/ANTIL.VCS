using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ANTIL.Domain.Core.Entities;
using ANTIL.Domain.Dao.Interfaces;

namespace HttpCommandHandler.Commands.Push
{
    class PushCommand : IPushCommand
    {
        private readonly IAntilFileDao antilFileDao;
        private readonly HttpCommandHandler cmdHandler;
        private List<AntilFile> Repository;
        private readonly IProjectDao projectDao;
        private readonly IUserDao userDao;
        private readonly ICommitDao commitDao;

        public PushCommand(IAntilFileDao antilFileDao,
            IProjectDao projectDao,
            IUserDao userDao,
            ICommitDao commitDao)
        {
            this.antilFileDao = antilFileDao;
            this.projectDao = projectDao;
            this.userDao = userDao;
            this.commitDao = commitDao;
            cmdHandler = new HttpCommandHandler(this);
            Repository = new List<AntilFile>();
        }

        public void Execute(HttpListenerContext contecxt)
        {
            cmdHandler.ExecuteMethod(contecxt, contecxt.Request.Headers.Get("action"), this);
        }

        public void Info(HttpListenerContext context)
        {
            try
            {
                string userName = context.Request.Headers.Get("owner");
                var user = userDao.CreateQuery().FirstOrDefault(u => u.Id != 0);
                if (user != null)
                {
                    
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 204;
                context.Response.StatusDescription = ex.Message;
            }
            finally
            {
                context.Response.Close();
            }

        }

        public void File(HttpListenerContext context)
        {
            try
            {
                var file = new AntilFile
                {
                    Name = context.Request.Headers.Get("fileName"),
                    Updated = DateTime.Parse(context.Request.Headers.Get("dateTime")),
                    Path = context.Request.Headers.Get("fullName"),
                    Extension = context.Request.Headers.Get("extension"),
                    Data = Encoding.ASCII.GetBytes("JFF"),
                    Id = default(int)
                };

                Repository.Add(file);
                antilFileDao.Save(file);
                context.Response.StatusCode = 200;
                context.Response.StatusDescription = "Ok";
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 204;
                context.Response.StatusDescription = ex.Message;
            }
            finally
            {
                context.Response.Close();
            }
        }

        private void Update(HttpListenerContext context)
        {
            try
            {
                antilFileDao.BulkSave(Repository);
                Console.WriteLine(string.Format("{0} files was save",Repository.Count));
                context.Response.StatusCode = 200;
                context.Response.StatusDescription = "Ok";
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 204;
                context.Response.StatusDescription = ex.Message;
            }
            finally
            {
                Repository.Clear();
                context.Response.Close();
            }
        }
    }
}

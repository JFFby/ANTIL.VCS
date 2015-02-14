using System;
using System.Collections.Generic;
using System.Net;
using ANTIL.Domain.Core.Entities;
using ANTIL.Domain.Dao.Interfaces;

namespace HttpCommandHandler.Commands.Push
{
    class PushCommand : IPushCommand
    {
        private readonly IAntilFileDao antilFileDao;
        private readonly HttpCommandHandler cmdHandler;
        private List<AntilFile> Repository;

        public PushCommand(IAntilFileDao antilFileDao)
        {
            this.antilFileDao = antilFileDao;
            this.cmdHandler = new HttpCommandHandler(this);
            Repository = new List<AntilFile>();
        }

        public void Execute(HttpListenerContext contect)
        {
            cmdHandler.ExecuteMethod(contect,contect.Request.Headers.Get("param"));
        }

        private void Add(HttpListenerContext context)
        {
            try
            {
                var file = new AntilFile
                {
                    Name = context.Request.Headers.Get("name"),
                    Updated = DateTime.Parse(context.Request.Headers.Get("date")),
                    Path = context.Request.Headers.Get("fullName"),
                    CommitName = context.Request.Headers.Get("commitName"),
                    Extension = context.Request.Headers.Get("extension"),
                    Owner = context.Request.Headers.Get("owner"),
                    ParentCommit = context.Request.Headers.Get("parent"),
                    Project = context.Request.Headers.Get("project")
                };

                Repository.Add(file);
                context.Response.StatusCode = 200;
                context.Response.StatusDescription = "Ok";
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

        private void Done(HttpListenerContext context)
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
                context.Response.StatusCode = 500;
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

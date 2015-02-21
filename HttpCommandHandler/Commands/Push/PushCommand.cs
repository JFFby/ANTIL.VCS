using System;
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
                var user = userDao.Get(userName);
                if (user != null)
                {
                    var proj = projectDao.GetOrCreateProject(context.Request.Headers.Get("project"), user);
                    string commitName = context.Request.Headers.Get("commitName");

                    if (!commitDao.IsUniqueCommit(commitName, proj))
                    {
                        throw new Exception("Error. Commit name must be unique");
                    }

                    var commit = new Commit
                    {
                        Name = commitName,
                        Project = proj,
                        ParentCommit = commitDao.Get(context.Request.Headers.Get("parent"), proj)
                    };

                    commitDao.Save(commit);

                    context.Response.StatusCode = 200;
                    context.Response.StatusDescription = "Commit was added";
                    context.Response.Headers.Add("commitId",commit.Id.ToString());
                }
                else
                {
                    throw new Exception("Error. Log in please");
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
                var commit = commitDao.Get(Int32.Parse(context.Request.Headers.Get("commitId")));

                if (commit == null)
                {
                    throw new Exception("Error. Commit not found");
                }

                var file = new AntilFile
                {
                    Name = context.Request.Headers.Get("fileName"),
                    Updated = DateTime.Parse(context.Request.Headers.Get("dateTime")),
                    Path = context.Request.Headers.Get("fullName"),
                    Extension = context.Request.Headers.Get("extension"),
                    Data = Encoding.ASCII.GetBytes("JFF"),
                    Commit = commit,
                    Status = context.Request.Headers.Get("fileName"),
                    Version = Int32.Parse(context.Request.Headers.Get("fileName"))
                };

              

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
    }
}

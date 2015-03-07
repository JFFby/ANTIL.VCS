using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.TestHttp
{
    public class TestHttp : BaseCommand, ITestHttp
    {
        private readonly CommandHandlerHelper cmdHelper;

        public TestHttp(CommandHandlerHelper cmdHelper)
        {
            this.cmdHelper = cmdHelper;
        }

        public void Execute(ICollection<string> args)
        {
            if (args.Count > 0 && cmdHelper.IsMethodExist(this, args.ToList()[0]))
            {
                cmdHelper.ExecuteMethod(this, args);
                return;
            }

            ch.WriteLine("Need some args",ConsoleColor.Red);
        }

        [AllowUnauthorized]
        public void File(ICollection<string> args)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:3300/");

            request.Headers.Add("cmd", "Push");
            request.Headers.Add("action", "File");
            request.Headers.Add("fileName", "Valera2.txt");
            request.Headers.Add("dateTime",DateTime.Now.ToString("o"));
            request.Headers.Add("fullName", @"d:\valera.txt");
            request.Headers.Add("extension", ".txt");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            ch.WriteLine(response.StatusDescription);
            response.Close();
        }

        public void Proj(ICollection<string> args)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:3300/");

            request.Headers.Add("cmd", "Push");
            request.Headers.Add("action", "Info");
            var response = (HttpWebResponse)request.GetResponse();
            ch.WriteLine(response.StatusDescription);
            response.Close();
        }

        [AllowUnauthorized]
        public void Valera(ICollection<string> args)
        {
            var request = (HttpWebRequest) WebRequest.Create("http://localhost:3300");
            request.Headers.Add("cmd","Push");
            request.Headers.Add("action","Valera");
            request.Headers.Add("valera","valera");
        }
    }
}

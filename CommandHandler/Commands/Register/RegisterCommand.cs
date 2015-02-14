using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;
using System.Net;

namespace CommandHandler.Commands.Register
{
    public class RegisterCommand: BaseCommand, IRegisterCommand
    {
        private string userName { get; set; }
        private string password { get; set; }
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:3300/");

        public void Execute(ICollection<string> args)
        {
            if (args.Count != 1 || args.ToArray()[0].Length == 0)
            {
                ch.WriteLine("Bad arguments. Type \"help\" to see the reference.");
                return;
            }

            userName = args.ToArray()[0];
            password = GetPassword();

            request.Headers.Add("cmd", "Registration");
            request.Headers.Add("Username", userName);
            request.Headers.Add("Password", password);

            WebResponse response = request.GetResponse();

            Console.WriteLine(response.Headers.Get("result"));
        }

        private string GetPassword()
        {
            return Console.ReadLine();
        }
    }
}

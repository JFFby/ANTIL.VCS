using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandHandler.Commands.Common;
using CommandHandler.Helpers;
using System.Net;

namespace CommandHandler.Commands.LogIn
{
    public class LoginCommand: BaseCommand, ILoginCommand
    {
        private string userName { get; set; }
        private string password { get; set; }
        private AntilStorageHelper sh;

        public LoginCommand(AntilStorageHelper storageHelper)
        {
            sh = storageHelper;
        }

        public void Execute(ICollection<string> args)
        {
            ch.WriteLine("Enter your username: ");

            userName = Console.ReadLine();

            while (userName.Length < 4)
            {
                ch.WriteLine("Username must be at least 4 characters long.");
                userName = Console.ReadLine();
            }

            password = GetPassword();

            if (password == null)
                return;

            SendLogInRequest();
        }

        private void SendLogInRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:3300/");

            request.Headers.Add("cmd", "Authorization");
            request.Headers.Add("UserName", userName);
            request.Headers.Add("Password", password);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                sh.UserName = userName;
            }
            else ch.WriteLine("Invalid user name or password.", ConsoleColor.Red);
        }

        private string GetPassword()
        {
            ch.WriteLine("Enter your password: ");

            string password = ch.GetMaskedString();

            return password;
        }
    }
}

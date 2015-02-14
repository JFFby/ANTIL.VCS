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
    public class RegisterCommand : BaseCommand, IRegisterCommand
    {
        private string userName { get; set; }
        private string password { get; set; }
        private AntilStorageHelper sh;

        public RegisterCommand(AntilStorageHelper storageHelper)
        {
            sh = storageHelper;
        }

        public void Execute(ICollection<string> args)
        {
            if (sh.UserName != string.Empty)
            {
                ch.WriteLine("You're already logged in.", ConsoleColor.White);
                return;
            }

            ch.WriteLine("Enter a username: ");

            userName = Console.ReadLine();

            while (userName.Length < 4)
            {
                ch.WriteLine("Username must be at least 4 characters long.");
                userName = Console.ReadLine();
            }

            password = GetPassword();

            if (password == null)
                return;

            
            SendRegisterRequest();
        }

        private string GetPassword()
        {
            ch.WriteLine("Enter your password: ");

            string password = ch.GetMaskedString();
            if (password.Length < 4)
            {
                ch.WriteLine("Password must be at least 4 characters long.", ConsoleColor.Red);
                return null;
            }

            ch.WriteLine("Confirm password:");
            
            if (password != ch.GetMaskedString())
            {
                ch.WriteLine("Passwords did not match. Try again.", ConsoleColor.Red);
                return null;
            }

            return password;
        }

        private void SendRegisterRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:3300/");

            request.Headers.Add("cmd", "Registration");
            request.Headers.Add("Username", userName);
            request.Headers.Add("Password", password);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                sh.UserName = userName;
                ch.WriteLine(string.Format("You've successfully registered {0}'s acount.", userName),
                    ConsoleColor.Green);

            }
            else ch.WriteLine("Sorry, this user name is taken.", ConsoleColor.Red);
        }
    }
}

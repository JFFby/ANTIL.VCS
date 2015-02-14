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

        public void Execute(ICollection<string> args)
        {
            if (args.Count != 1 || args.ToArray()[0].Length == 0)
            {
                ch.WriteLine("Bad arguments. Type \"help\" to see the reference.", ConsoleColor.Red);
                return;
            }

            userName = args.ToArray()[0];
            password = GetPassword();

            if (password == null)
                return;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:3300/");
            request.Headers.Add("cmd", "Registration");
            request.Headers.Add("Username", userName);
            request.Headers.Add("Password", password);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
                ch.WriteLine(string.Format("Welocme to ANTILvcs, {0}", userName));
            else ch.WriteLine("Error! This username is taken.", ConsoleColor.Red);
            response.Close();

        }

        private string GetPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            ch.WriteLine("Enter your password: ");

            do
            {
                key = Console.ReadKey(true);
                if (char.IsLetterOrDigit(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            if (password.Length < 4)
            {
                ch.WriteLine("Password must be at least 4 characters long.", ConsoleColor.Red);
                return null;
            }

            return password;
        }
    }
}

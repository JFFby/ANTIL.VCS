using System;

namespace CommandHandler.Helpers
{
    public class ConsoleHelper
    {
        public void WriteLine(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("ANTIL: " + msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Write(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.Write("ANTIL: " + msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public string GetMaskedString()
        {
            string str = "";
            ConsoleKeyInfo key;
            
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" > ");

            do
            {
                key = Console.ReadKey(true);
                if (char.IsLetterOrDigit(key.KeyChar))
                {
                    str += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && str.Length > 0)
                {
                    str = str.Substring(0, str.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            return str;
        }
    }
}

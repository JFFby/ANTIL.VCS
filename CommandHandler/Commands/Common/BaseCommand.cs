using System;

namespace CommandHandler.Commands.Common
{
    public class BaseCommand
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
    }
}

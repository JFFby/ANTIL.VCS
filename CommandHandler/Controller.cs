using System.Collections.Generic;
using CommandHandler.Commands.Exit;

namespace CommandHandler
{
    /// <summary>
    /// Аналогично с CommandHandler. Здесь только вызываем Execute методы, не думаю что здесь что-то 
    /// можно поменять
    /// </summary>

    public class Controller
    {
        private readonly IExitCommand exitCommand;

        public Controller(IExitCommand exitCommand)
        {
            this.exitCommand = exitCommand;
        }

        public void exit(ICollection<string> args)
        {
            exitCommand.Execute(args);
        }
    }
}

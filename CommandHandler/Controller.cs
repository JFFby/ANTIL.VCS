﻿using System.Collections.Generic;
using CommandHandler.Commands.Exit;
using CommandHandler.Commands.Help;

namespace CommandHandler
{
    /// <summary>
    /// Аналогично с CommandHandler. Здесь только вызываем Execute методы, не думаю что здесь что-то 
    /// можно поменять
    /// З.Ы. как прочтёшь, удаляй
    /// </summary>

    public class Controller
    {
        private readonly IExitCommand exitCommand;
        private readonly IHelpCommand helpCommand;

        public Controller(IExitCommand exitCommand, IHelpCommand helpCommand)
        {
            this.exitCommand = exitCommand;
            this.helpCommand = helpCommand;
        }

        public void Exit(ICollection<string> args)
        {
            exitCommand.Execute(args);
        }

        public void Help(ICollection<string> args)
        {
            helpCommand.Execute(args);
        }
    }
}

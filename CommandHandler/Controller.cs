﻿using System;
using System.Collections.Generic;
using System.Linq;
using CommandHandler.Commands.Cd;
using CommandHandler.Commands.Dir;
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
        private readonly ICd cd;
        private readonly IDirCommand dirCommand;

        public Controller(IExitCommand exitCommand, IHelpCommand helpCommand, ICd cd, IDirCommand dirCommand)
        {
            this.exitCommand = exitCommand;
            this.helpCommand = helpCommand;
            this.cd = cd;
            this.dirCommand = dirCommand;
        }

        public void Exit(ICollection<string> args)
        {
            exitCommand.Execute(args);
        }

        public void Help(ICollection<string> args)
        {
            helpCommand.Execute(args);
        }

        public void Cd(ICollection<string> args)
        {
            cd.Execute(args);
        }

        public void Dir(ICollection<string> args)
        {
            dirCommand.Execute(args);
        }
    }
}

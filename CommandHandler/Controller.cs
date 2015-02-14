﻿using System.Collections.Generic;
using CommandHandler.Commands.Cd;
using CommandHandler.Commands.Dir;
using CommandHandler.Commands.Exit;
using CommandHandler.Commands.Help;
using CommandHandler.Commands.Init;
using CommandHandler.Commands.List;
using CommandHandler.Commands.Status;
using CommandHandler.Commands.Register;

namespace CommandHandler
{
    public class Controller
    {
        private readonly IExitCommand exitCommand;
        private readonly IHelpCommand helpCommand;
        private readonly ICd cdCommand;
        private readonly IDirCommand dirCommand;
        private readonly IInitCommand initComman;
        private readonly IStatusCommand statusCommand;
        private readonly IListCommand listCommand;
        private readonly IRegisterCommand registerCommand;

        public Controller(IExitCommand exitCommand,
            IHelpCommand helpCommand,
            ICd cdCommand,
            IDirCommand dirCommand,
            IInitCommand initComman,
            IStatusCommand statusCommand,
            IListCommand listCommand,
            IRegisterCommand registerCommand)
        {
            this.exitCommand = exitCommand;
            this.helpCommand = helpCommand;
            this.cdCommand = cdCommand;
            this.dirCommand = dirCommand;
            this.initComman = initComman;
            this.statusCommand = statusCommand;
            this.listCommand = listCommand;
            this.registerCommand = registerCommand;
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
            cdCommand.Execute(args);
        }

        public void Dir(ICollection<string> args)
        {
            dirCommand.Execute(args);
        }

        public void Init(ICollection<string> args)
        {
            initComman.Execute(args);
        }

        public void Status(ICollection<string> args)
        {
            statusCommand.Execute(args);
        }

        public void List(ICollection<string> args)
        {
            listCommand.Execute(args);
        }

        public void Register(ICollection<string> args)
        {
            registerCommand.Execute(args);
        }
    }
}

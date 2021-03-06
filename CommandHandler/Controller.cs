﻿using System.Collections.Generic;
using CommandHandler.Commands.Cd;
using CommandHandler.Commands.Commit;
using CommandHandler.Commands.Dir;
using CommandHandler.Commands.Exit;
using CommandHandler.Commands.Help;
using CommandHandler.Commands.Init;
using CommandHandler.Commands.List;
using CommandHandler.Commands.Status;
using CommandHandler.Commands.Register;
using CommandHandler.Commands.LogIn;
using CommandHandler.Commands.Add;
using CommandHandler.Commands.Remove;
using CommandHandler.Commands.TestHttp;
using CommandHandler.Helpers;

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
        private readonly ILoginCommand loginCommand;
        private readonly IAddCommand addCommand;
        private readonly IRemoveCommand removeCommand;
        private readonly ITestHttp testHttp;
        private readonly ICommitCommand commitCommand;

        public Controller(IExitCommand exitCommand,
            IHelpCommand helpCommand,
            ICd cdCommand,
            IDirCommand dirCommand,
            IInitCommand initComman,
            IStatusCommand statusCommand,
            IListCommand listCommand,
            IRegisterCommand registerCommand,
            ILoginCommand logInCommand,
            IAddCommand addCommand,
            IRemoveCommand removeCommand,
            ITestHttp testHttp, ICommitCommand commitCommand)
        {
            this.exitCommand = exitCommand;
            this.helpCommand = helpCommand;
            this.cdCommand = cdCommand;
            this.dirCommand = dirCommand;
            this.initComman = initComman;
            this.statusCommand = statusCommand;
            this.listCommand = listCommand;
            this.registerCommand = registerCommand;
            this.loginCommand = logInCommand;
            this.addCommand = addCommand;
            this.removeCommand = removeCommand;
            this.testHttp = testHttp;
            this.commitCommand = commitCommand;
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

        public void Login(ICollection<string> args)
        {
            loginCommand.Execute(args);
        }

        public void Add(ICollection<string> args)
        {
            addCommand.Execute(args);
        }

        public void Remove(ICollection<string> args)
        {
            removeCommand.Execute(args);
        }

        [AllowUnauthorized]
        public void Testhttp(ICollection<string> args)
        {
            testHttp.Execute(args);
        }

        public void Commit(ICollection<string> args)
        {
            commitCommand.Execute(args);
        }
    }
}

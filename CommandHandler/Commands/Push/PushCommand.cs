using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandHandler.Commands.Common;
using CommandHandler.Entites;
using CommandHandler.Helpers;

namespace CommandHandler.Commands.Push
{
    public class PushCommand: BaseCommand, IPushCommand
    {
        public void Execute(ICollection<string> args)
        {
            throw new NotImplementedException();
        }
    }
}

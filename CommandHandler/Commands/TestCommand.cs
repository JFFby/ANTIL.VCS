using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandHandler.Helpers;
using CommandHandler.Commands.Common;
using 

namespace CommandHandler.Commands.Test
{
    public class TestCommand: BaseCommand, ITestCommand
    {
        public void Execute(IEnumerable<string> args)
        {
            
        }
    }
}

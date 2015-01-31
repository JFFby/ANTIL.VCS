using System.Collections.Generic;

namespace CommandHandler.Commands.Common
{
    public interface IANTILCommand
    {
        void Execute(ICollection<string> args);
    }
}

using System.Net;

namespace HttpCommandHandler.Commands
{
    public interface IAntilHttpCommand
    {
        void Execute(HttpListenerContext context);
    }
}

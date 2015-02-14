namespace ANTIL.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new AntilServer("http://localhost:3300/");
            server.Start();
        }
    }
}

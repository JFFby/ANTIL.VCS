using System.IO;

namespace CommandHandler.Helpers
{
    public static class FileNameHelper
    {
        public static string ShotFileName(this FileInfo file, string projpPath)
        {
            return file.FullName.Replace(projpPath, "");
        }
    }
}

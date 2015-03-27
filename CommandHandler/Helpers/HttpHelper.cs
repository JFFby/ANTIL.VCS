using System.IO;
using System.Net;
using CommandHandler.Entites;

namespace CommandHandler.Helpers
{
    public class HttpHelper
    {
        private string Path = "http://localhost:3300/";

        public AntilResponse SendMeta(string commitName, string parent, string owner, string project, string comment)
        {
            var request = (HttpWebRequest) WebRequest.Create(Path);
            request.Method = "GET";
            request.Headers.Add("cmd", "Push");
            request.Headers.Add("action", "Info");
            request.Headers.Add("commitName", commitName);
            request.Headers.Add("parent", parent);
            request.Headers.Add("owner", owner);
            request.Headers.Add("project", project);
            request.Headers.Add("comment", comment);

            var response = (HttpWebResponse)request.GetResponse();
            var ri = new ResponseInfo
            {
                Description = response.StatusDescription,
                StatusCode = response.StatusCode,
                CommitId = response.Headers.Get("commitId")
                
            };
            response.Close();
            return ri;
        }

        public AntilResponse SendFile(FileViewModel fileView, string commitId)
        {
            var file = new FileInfo(fileView.FullPath);

            var request = (HttpWebRequest)WebRequest.Create(Path);
            request.Method = "POST";
            request.Headers.Add("cmd", "Push");
            request.Headers.Add("action", "File");

            request.Headers.Add("fileName", fileView.Name);
            request.Headers.Add("extension", file.Extension);
            request.Headers.Add("status", fileView.Status);
            request.Headers.Add("version", fileView.Version.ToString());
            request.Headers.Add("commitId", commitId);
            request.Headers.Add("dateTime", file.LastWriteTime.ToString("o"));

            Stream fstream = File.Open(fileView.FullPath, FileMode.Open);
            request.ContentLength = fstream.Length;
            fstream.CopyTo(request.GetRequestStream());

            var response = (HttpWebResponse)request.GetResponse();
            var ar = new AntilResponse()
            {
                Description = response.StatusDescription,
                StatusCode = response.StatusCode

            };
            response.Close();
            return ar;
        }

    }
}

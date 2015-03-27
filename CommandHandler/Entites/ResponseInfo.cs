using System.Net;

namespace CommandHandler.Entites
{

    public class AntilResponse
    {
        public string Description { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }

    public class ResponseInfo : AntilResponse
    {
        public string CommitId { get; set; }
    }
}

using System.Net.Mime;

namespace WebApi.Gates.Interfaces.Default
{
    public class PostParameters
    {
        public string Uri;
        public string Data;
        public string ContentType;
        public string Method = "POST";

        public PostParameters(string uri)
        {
            Uri = uri;
        }

        public PostParameters(string uri, string data, string contentType) : this(uri)
        {
            Data = data;
            ContentType = contentType;
        }
    }
}
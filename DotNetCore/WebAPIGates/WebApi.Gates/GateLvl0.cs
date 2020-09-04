using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.Gates.Interfaces;
using WebApi.Gates.Interfaces.Default;

namespace WebApi.Gates
{
    public class GateLvl0 : IGate<PostParameters>, IGateAsync<PostParameters>
    {
        public GateLvl0(string mainUri)
        {
            MainUri = mainUri;
        }

        public string MainUri { get; }

        public string Get(string uri)
        {
            return _readAnswer(_formGetRequest(uri));
        }

        public string Post(PostParameters postParameters)
        {
            return _readAnswer(_formPostRequest(postParameters));
        }

        public Task<string> GetAsync(string uri)
        {
            return _readAnswerAsync(_formGetRequest(uri));
        }

        public Task<string> PostAsync(PostParameters postParameters)
        {
            return _readAnswerAsync(_formPostRequest(postParameters));
        }

        private HttpWebRequest _formGetRequest(string uri)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            return request;
        }

        private HttpWebRequest _formPostRequest(PostParameters postParameters)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(postParameters.Data);

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(postParameters.Uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = postParameters.ContentType;
            request.Method = postParameters.Method;

            using (Stream requestBody = request.GetRequestStream())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            return request;
        }

        private string _readAnswer(HttpWebRequest request)
        {
            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader =
                new StreamReader(
                    stream ?? throw new Exception(
                        $"Http response error in {this.GetType().FullName}.{nameof(_readAnswer)}")))
            {
                return reader.ReadToEnd();
            }
        }

        private async Task<string> _readAnswerAsync(HttpWebRequest request)
        {
            using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader =
                new StreamReader(
                    stream ?? throw new Exception(
                        $"Http response error in {this.GetType().FullName}.{nameof(_readAnswerAsync)}")))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
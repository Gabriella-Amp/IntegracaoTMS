using System.Net.Http;

namespace Services.Shared.Configurations.Http.Models
{
    public class HttpRequestMessageWrapper
    {
        public HttpRequestMessageWrapper(HttpRequestMessage message, object content)
        {
            Message = message;
            Content = content;
        }

        public string Name => "HTTP REQUEST";

        public object Content { get; }

        public HttpRequestMessage Message { get; }
    }
}
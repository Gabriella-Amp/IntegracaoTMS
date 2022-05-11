using System;
using System.Net.Http;

namespace Services.Shared.Configurations.Http.Models
{
    public class HttpResponseMessageWrapper
    {
        public HttpResponseMessageWrapper(HttpResponseMessage message, object content, TimeSpan elapsedTime)
        {
            Message = message;
            Content = content;
            ElapsedTime = elapsedTime;
        }

        public string Name => "HTTP RESPONSE";

        public object Content { get; }

        public TimeSpan ElapsedTime { get; }

        public HttpResponseMessage Message { get; }
    }
}
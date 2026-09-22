using System.Net;

namespace CrosswordHelper.Tests
{
    public class StubMessageHandler : DelegatingHandler
    {
        private readonly Dictionary<string, string> _content;

        public StubMessageHandler(string content)
        {
            _content = new Dictionary<string, string> { { "*", content } };
        }

        public StubMessageHandler(Dictionary<string, string> content)
        {
            _content = content;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string requestUri = request.RequestUri.Segments[1];
            string? content = null;

            if (_content.ContainsKey(requestUri))
            {
                content = _content[requestUri];
            }
            else if (_content.ContainsKey("*"))
            {
                content = _content["*"];
            }
            else
            {
                return Task.FromResult(new HttpResponseMessage() { StatusCode = HttpStatusCode.NotFound });
            }

            return Task.FromResult(new HttpResponseMessage() { Content = new StringContent(content) });
        }
    }
}
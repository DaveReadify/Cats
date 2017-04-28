using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cats.Tests.Fakes
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;
        public HttpRequestMessage LastRequest { get; private set; }
        public string Etag { get; private set; }
        public string LastContent { get; private set; }

        public FakeHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(_response);

            LastRequest = request;
            LastContent = request.Content?.ReadAsStringAsync().Result;

            return responseTask.Task;
        }
    }
}

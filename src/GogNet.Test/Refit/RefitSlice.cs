/*
 * Runtime glue for the Refit vertical slice (test project only).
 *
 * The public GOG endpoints need no auth, so this is just the library's settings factory
 * plus a CapturingHandler that records the outgoing request and returns canned JSON —
 * the suite never touches the network.
 */

using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GogNet.Client;
using Refit;

namespace GogNet.Test.RefitSlice
{
    /// <summary>Builds a Refit client for one API using the library's settings factory.</summary>
    public static class GogRefit
    {
        public static readonly RefitSettings Settings = GogRefitSettings.Create();

        public static T Create<T>(HttpClient http) => RestService.For<T>(http, Settings);

        public static HttpClient Client(CapturingHandler handler) =>
            new HttpClient(handler) { BaseAddress = GogRefitSettings.ApiBaseAddress };
    }

    /// <summary>Test double: records the outgoing request and returns a canned JSON response.</summary>
    public sealed class CapturingHandler : HttpMessageHandler
    {
        private readonly string _json;
        public HttpRequestMessage LastRequest { get; private set; }
        public string LastRequestBody { get; private set; }
        public CapturingHandler(string json) { _json = json; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            LastRequest = request;
            // read the body eagerly: Refit disposes the content after sending
#if NET5_0_OR_GREATER
            LastRequestBody = request.Content == null ? null : await request.Content.ReadAsStringAsync(ct);
#else
            LastRequestBody = request.Content == null ? null : await request.Content.ReadAsStringAsync();
#endif
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_json, Encoding.UTF8, "application/json"),
            };
        }
    }
}

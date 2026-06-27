using System.Diagnostics;

namespace BangumiNet.Api.Helpers;

internal class TextErrorHandler(HttpMessageHandler innerHandler) : DelegatingHandler(innerHandler)
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.Content.Headers.ContentType?.MediaType?.StartsWith("text") ?? false)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            Trace.TraceWarning($"{request.RequestUri} [{response.StatusCode}] {body.Trim()}");
        }

        return response;
    }
}

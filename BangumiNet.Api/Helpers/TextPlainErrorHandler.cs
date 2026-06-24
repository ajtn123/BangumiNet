using System.Diagnostics;

namespace BangumiNet.Api.Helpers;

/// <summary>
/// 拦截 text/plain 类型的 HTTP 错误响应，防止 Kiota 在反序列化时抛出异常。
/// 将 text/plain 错误响应体包装为 <see cref="HttpRequestException"/> 向上抛出。
/// </summary>
public class TextErrorHandler(HttpMessageHandler innerHandler) : DelegatingHandler(innerHandler)
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

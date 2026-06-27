using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Timeline.Events;
using BangumiNet.Common.Attributes;
using Microsoft.Kiota.Serialization.Json;
using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BangumiNet.Api.Misc;

/// <summary>
/// Server-Sent Events from /p1/timeline/-/events
/// </summary>
public class TimelineEventStream(HttpClient httpClient, IHttpAuthenticationProvider? authProvider)
{
    private const string Endpoint = $"{P1.ApiClient.BaseUrl}/p1/timeline/-/events";
    private static readonly JsonParseNodeFactory factory = new();

    private static async Task<Timeline?> DeserializeTimeline(byte[] bytes)
    {
        await using var stream = new MemoryStream(bytes, writable: false);
        var node = await factory.GetRootParseNodeAsync("application/json", stream);
        return node.GetObjectValue(EventsGetResponse.CreateFromDiscriminatorValue).Timeline;
    }

    public async IAsyncEnumerable<Timeline> StartAsync(FilterMode mode = FilterMode.All, TimelineCategory? category = null, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var url = $"{Endpoint}?mode={AttributeHelpers.GetAttribute<EnumMemberAttribute>(mode)!.Value}";
        if (category != null) url += $"&cat={(int)category}";
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Accept.ParseAdd("text/event-stream");
        authProvider?.AuthenticateRequestAsync(request);
        using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct);
        response.EnsureSuccessStatusCode();
        await using var stream = await response.Content.ReadAsStreamAsync(ct);

        await foreach (var item in SseParser.Create(stream, (type, bytes) => DeserializeTimeline([.. bytes]).GetAwaiter().GetResult()).EnumerateAsync(ct))
            if (item.Data is not null) yield return item.Data;
    }
}

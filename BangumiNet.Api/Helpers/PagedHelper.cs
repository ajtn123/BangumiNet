using BangumiNet.Api.Interfaces;
using Microsoft.Kiota.Abstractions;

namespace BangumiNet.Api.Helpers;

public static class PagedHelper
{
    public static Action<RequestConfiguration<T>> Paging<T>(int? limit, int? offset) where T : class, IPagedRequest, new() => config => config.Paging(limit, offset);
    public static void Paging<T>(this RequestConfiguration<T> config, int? limit, int? offset) where T : class, IPagedRequest, new()
    {
        config.QueryParameters.Limit = limit;
        config.QueryParameters.Offset = offset;
    }
}

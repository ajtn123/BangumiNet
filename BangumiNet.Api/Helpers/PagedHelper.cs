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

    /// <summary>
    /// page starts from 1
    /// </summary>
    public static void SetPage<T>(this RequestConfiguration<T> config, int page, int size) where T : class, IPagedRequest, new()
    {
        config.QueryParameters.Offset = (page - 1) * size;
        config.QueryParameters.Limit = size;
    }

    public static void Limit<T>(this RequestConfiguration<T> config, int? limit) where T : class, IPagedRequest, new()
        => config.QueryParameters.Limit = limit;
    public static void Offset<T>(this RequestConfiguration<T> config, int? offset) where T : class, IPagedRequest, new()
        => config.QueryParameters.Offset = offset;
}

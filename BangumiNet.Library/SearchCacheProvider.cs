using BangumiNet.Api.Interfaces;
using BangumiNet.Api.P1.Models;
using BangumiNet.Common;
using BangumiNet.Shared;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;

namespace BangumiNet.Library;

public static class SearchCacheProvider
{
    public record class SearchResult(int? Id, string? Name, string? NameCn, SubjectType? Type, ImageSet? Images);

    static SearchCacheProvider()
    {
        SearchResults = Load();
    }

    public static ConcurrentDictionary<string, SearchResult?> SearchResults { get; private set; }

    public static void Add(string keyword, SlimSubject? result)
        => Add(keyword, result is { } r ? new SearchResult(r.Id, r.Name, r.NameCN, (SubjectType?)r.Type, r.Images is { } images ? new ImageSet
        {
            Grid = images.Grid,
            Small = images.Small,
            Medium = images.Medium,
            Large = images.Large,
        } : null) : null);
    public static void Add(string keyword, SearchResult? result)
        => SearchResults[keyword] = result;
    public static SearchResult? Get(string keyword)
        => SearchResults.GetValueOrDefault(keyword);

    private static readonly JsonSerializerOptions options = new() { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
    private static readonly FileInfo local = new(PathProvider.GetAbsolutePathForLocalData(Constants.LibraryCacheJsonName));
    private static ConcurrentDictionary<string, SearchResult?> Load()
    {
        if (!local.Exists) return [];
        try
        {
            using var fs = local.OpenRead();
            return JsonSerializer.Deserialize<ConcurrentDictionary<string, SearchResult?>>(fs, options) ?? [];
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Message);
            return [];
        }
    }
    public static void Save()
    {
        var json = JsonSerializer.SerializeToUtf8Bytes(SearchResults, options);
        File.WriteAllBytes(local.FullName, json);
    }
}

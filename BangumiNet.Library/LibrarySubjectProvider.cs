using BangumiNet.Api.Interfaces;
using BangumiNet.Common;
using BangumiNet.Shared;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;

namespace BangumiNet.Library;

public static class LibrarySubjectProvider
{
    public record class SubjectEntry(int? Id, string? Name, string? NameCn, SubjectType? Type, ImageSet? Images);

    static LibrarySubjectProvider()
    {
        Subjects = Load();
    }

    public static ConcurrentDictionary<string, SubjectEntry?> Subjects { get; private set; }

    private static bool isChanged = false;
    public static void Set(string keyword, SubjectEntry? result)
    {
        Interlocked.Exchange(ref isChanged, true);
        Subjects[keyword] = result;
    }
    public static SubjectEntry? Get(string keyword)
        => Subjects.GetValueOrDefault(keyword);

    private static readonly JsonSerializerOptions options = new() { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
    private static readonly FileInfo local = new(Path.Combine(Constants.AppData, Constants.LibraryCacheJsonName));
    private static ConcurrentDictionary<string, SubjectEntry?> Load()
    {
        if (!local.Exists) return [];
        try
        {
            using var fs = local.OpenRead();
            return JsonSerializer.Deserialize<ConcurrentDictionary<string, SubjectEntry?>>(fs, options) ?? [];
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Message);
            return [];
        }
    }
    public static void Save()
    {
        if (Interlocked.Exchange(ref isChanged, false) == false)
            return;
        if (Subjects.IsEmpty)
        {
            File.Delete(local.FullName);
            return;
        }

        var json = JsonSerializer.SerializeToUtf8Bytes(Subjects, options);
        Utils.WriteAppData(local, json);
    }
}

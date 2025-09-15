using System.Text.Json;

namespace BangumiNet.Shared;

public static class CacheProvider
{
    public static List<CacheFileInfo>? CacheFiles { get; set; }
    private static readonly JsonSerializerOptions options = new() { IgnoreReadOnlyProperties = true };
    public static void LoadCacheList()
    {
        if (!SettingProvider.CurrentSettings.IsDiskCacheEnabled) return;

        var path = GetAbsolutePath(Constants.CacheJsonName);
        if (!File.Exists(path))
        {
            CacheFiles = [];
            return;
        }

        var json = File.ReadAllText(path);
        var list = JsonSerializer.Deserialize<List<CacheFileInfo>>(json, options);
        if (list is null)
        {
            CacheFiles = [];
            return;
        }
        else
        {
            CacheFiles = [.. list.Where(c => c.Validate())];
            return;
        }
    }

    public static void SaveCacheList()
    {
        if (!SettingProvider.CurrentSettings.IsDiskCacheEnabled) return;

        var json = JsonSerializer.Serialize(CacheFiles?.Where(c => c.Validate()).ToList(), options);
        var path = GetAbsolutePath(Constants.CacheJsonName);
        Directory.CreateDirectory(SettingProvider.CurrentSettings.DiskCacheDirectory);
        File.WriteAllText(path, json);
    }

    private static uint writes = 0;
    public static void WriteCache(Stream content, CacheFileInfo info)
    {
        if (!SettingProvider.CurrentSettings.IsDiskCacheEnabled || CacheFiles is null) return;

        CacheFiles.Add(info);
        info.RelativePath = Path.Combine(info.Type.ToString(), Utils.GetHash(info.Id));
        var dir = Path.GetDirectoryName(info.AbsolutePath);
        if (string.IsNullOrWhiteSpace(dir)) return;
        Directory.CreateDirectory(dir);
        using var file = File.OpenWrite(info.AbsolutePath);
        content.CopyTo(file);
        content.Position = 0;

        writes++;
        if (writes > SettingProvider.CurrentSettings.IndexSaveFrequency)
        {
            writes = 0;
            SaveCacheList();
        }
    }

    public static FileStream? ReadCache(string id)
    {
        if (!SettingProvider.CurrentSettings.IsDiskCacheEnabled || CacheFiles is null) return null;

        var existing = CacheFiles.Where(c => c.Id.Equals(id)).FirstOrDefault(defaultValue: null);
        if (existing?.Validate() ?? false)
            return existing!.GetFileStream();
        else return null;
    }

    public static string GetAbsolutePath(string relativePath)
        => Path.Combine(SettingProvider.CurrentSettings.DiskCacheDirectory, relativePath);
}

namespace BangumiNet.Shared;

public static class CacheProvider
{
    public static string CacheDirPath => Path.Combine(SettingProvider.CurrentSettings.LocalDataDirectory, Constants.DiskCacheDirectory);
    public static string GetAbsolutePath(string relativePath) => Path.Combine(CacheDirPath, relativePath);

    public static long CacheSize { get; set; } = 0;
    public static void CalculateCacheSize()
    {
        if (!SettingProvider.CurrentSettings.IsDiskCacheEnabled) CleanUpCache();
        else CacheSize = CacheDirInfo.EnumerateFiles().Sum(f => f.Length);
    }

    public static void WriteCache(string id, Stream content)
    {
        if (!SettingProvider.CurrentSettings.IsDiskCacheEnabled) return;

        var l = content.Length;
        if (l > SettingProvider.CurrentSettings.DiskCacheSizeLimit) return;
        if (CacheSize + l > SettingProvider.CurrentSettings.DiskCacheSizeLimit) CleanUpCache();

        var idHash = Utils.GetHash(id);
        var path = GetAbsolutePath(idHash);
        var dir = Path.GetDirectoryName(path);
        if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(idHash) || string.IsNullOrWhiteSpace(dir)) return;

        Directory.CreateDirectory(dir);
        using var file = File.OpenWrite(path);
        content.CopyTo(file);
        content.Position = 0;

        CacheSize += l;
    }

    public static FileStream? ReadCache(string id)
    {
        if (!SettingProvider.CurrentSettings.IsDiskCacheEnabled) return null;

        var idHash = Utils.GetHash(id);
        var path = GetAbsolutePath(idHash);

        if (File.Exists(path))
            return File.OpenRead(path);
        else return null;
    }

    public static void DeleteCache(string id)
    {
        var idHash = Utils.GetHash(id);
        var path = GetAbsolutePath(idHash);

        if (File.Exists(path)) File.Delete(path);
    }


    private readonly static DirectoryInfo CacheDirInfo = new(CacheDirPath);
    public static void CleanUpCache()
    {
        var files = CacheDirInfo.EnumerateFiles();

        if (!SettingProvider.CurrentSettings.IsDiskCacheEnabled)
        {
            foreach (var file in files)
                file.Delete();
            return;
        }

        foreach (var file in files.OrderBy(c => c.LastAccessTimeUtc).Take(files.Count() / 4))
            file.Delete();

        CacheSize = files.Sum(f => f.Length);
        if (CacheSize > SettingProvider.CurrentSettings.DiskCacheSizeLimit)
            CleanUpCache();
    }
}

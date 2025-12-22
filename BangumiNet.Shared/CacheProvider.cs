namespace BangumiNet.Shared;

public static class CacheProvider
{
    public static string CacheDirPath => PathProvider.GetAbsolutePathForLocalData(Constants.DiskCacheDirectory);
    public static DirectoryInfo CacheDirInfo => new(CacheDirPath);
    public static string GetAbsolutePath(string relativePath) => Path.Combine(CacheDirPath, relativePath);

    public static long CacheSize { get; set; } = 0;
    static CacheProvider()
    {
        if (!SettingProvider.Current.IsDiskCacheEnabled) DumpCache();
        else if (!CacheDirInfo.Exists) CacheDirInfo.Create();
        else CacheSize = CacheDirInfo.EnumerateFiles().Sum(f => f.Length);
    }

    public static async Task WriteCache(string id, Stream content)
    {
        if (!SettingProvider.Current.IsDiskCacheEnabled) return;

        var l = content.Length;
        if (l > SettingProvider.Current.DiskCacheSizeLimit) return;
        if (CacheSize + l > SettingProvider.Current.DiskCacheSizeLimit) CleanUpCache();

        var idHash = Utils.GetHash(id);
        var path = GetAbsolutePath(idHash);
        var dir = Path.GetDirectoryName(path);
        if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(idHash) || string.IsNullOrWhiteSpace(dir)) return;

        Directory.CreateDirectory(dir);
        await using var file = File.OpenWrite(path);
        await content.CopyToAsync(file, CancellationToken.None);
        content.Position = 0;

        CacheSize += l;
    }

    public static FileStream? ReadCache(string id)
    {
        if (!SettingProvider.Current.IsDiskCacheEnabled) return null;

        var idHash = Utils.GetHash(id);
        var path = GetAbsolutePath(idHash);

        if (File.Exists(path))
            return File.OpenRead(path);
        else return null;
    }

    public static string? GetCacheFile(string id)
    {
        if (!SettingProvider.Current.IsDiskCacheEnabled) return null;

        var idHash = Utils.GetHash(id);
        var path = GetAbsolutePath(idHash);

        if (File.Exists(path))
            return path;
        else return null;
    }

    public static void DeleteCache(string id)
    {
        var idHash = Utils.GetHash(id);
        var path = GetAbsolutePath(idHash);

        if (File.Exists(path)) File.Delete(path);
    }

    public static void CleanUpCache()
    {
        if (!SettingProvider.Current.IsDiskCacheEnabled)
        {
            DumpCache();
            return;
        }

        var files = CacheDirInfo.EnumerateFiles();
        foreach (var file in files.OrderBy(c => c.LastAccessTimeUtc).Take(files.Count() / 4))
            file.Delete();

        CacheSize = files.Sum(f => f.Length);
        if (CacheSize > SettingProvider.Current.DiskCacheSizeLimit)
            CleanUpCache();
    }

    public static void DumpCache()
    {
        var files = CacheDirInfo.EnumerateFiles();
        foreach (var file in files)
            file.Delete();
        CacheSize = files.Sum(f => f.Length);
    }
}

namespace BangumiNet.Shared;

public class CacheFileInfo
{
    public required string Id { get; set; }
    public required CacheType Type { get; set; }
    public required DateTime InitiateTime { get; set; }

    public TimeSpan ValidTime { get; set; } = TimeSpan.FromDays(7);
    public string RelativePath { get; set; } = DateTime.Now.ToFileTimeUtc().ToString();

    public string AbsolutePath => CacheProvider.GetAbsolutePath(RelativePath);
    public bool IsPresent => File.Exists(AbsolutePath);
    public FileStream? GetFileStream()
    {
        if (IsPresent)
            return File.OpenRead(AbsolutePath);
        else return null;
    }
    public MemoryStream? GetMemoryStream() => GetFileStream()?.Clone();
    public void DeleteCache()
    {
        if (IsPresent)
            File.Delete(AbsolutePath);
    }
    public bool Validate()
    {
        if (!IsPresent) return false;
        else if (InitiateTime + ValidTime < DateTime.Now)
        {
            DeleteCache();
            return false;
        }
        else return true;
    }
}

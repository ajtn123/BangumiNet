using System.Reflection;

namespace BangumiNet.Shared;

public class Settings : IApiSettings
{
    public string UserAgent { get; set; } = $"ajtn123/{Constants.ApplicationName}/{Assembly.GetExecutingAssembly().GetName().Version} ({Environment.OSVersion.Platform}) ({Constants.SourceRepository})";
    public string? AuthToken { get; set; } = null;

    public bool IsDiskCacheEnabled { get; set; } = true;
    public string DiskCacheDirectory { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ApplicationName, "Cache");
    public uint IndexSaveFrequency { get; set; } = 100;

    public bool PreferChineseNames { get; set; } = false;
}

using System.Reflection;

namespace BangumiNet.Shared;

public class Settings : IApiSettings
{
    public string UserAgent { get; set; } = $"ajtn123/{Constants.ApplicationName}/{Assembly.GetExecutingAssembly().GetName().Version} ({Environment.OSVersion.Platform}) ({Constants.SourceRepository})";
    public string? AuthToken { get; set; } = null;

    public bool IsCacheEnabled { get; set; } = true;
    public string CachePath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ApplicationName, "Cache");

    public bool PreferChineseNames { get; set; } = false;
}

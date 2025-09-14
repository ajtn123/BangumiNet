using System.Reflection;

namespace BangumiNet.Shared;

public class Settings : IApiSettings
{
    public string UserAgent { get; set; } = $"ajtn123/{Constants.ApplicationName}/{Assembly.GetExecutingAssembly().GetName().Version} ({Environment.OSVersion.Platform}) ({Constants.SourceRepository})";
    public string? AuthToken { get; set; } = null;

    public bool PreferChineseNames { get; set; } = false;
}

using BangumiNet.Shared.Interfaces;
using System.Reflection;

namespace BangumiNet.Shared;

public class Settings : IApiSettings
{
    public string UserAgent { get; set; } = $"ajtn123/{Constants.ApplicationName}/{Assembly.GetExecutingAssembly().GetName().Version} ({Environment.OSVersion.Platform}) ({Constants.SourceRepository})";
    public string? AuthToken { get; set; } = null;

    public string BangumiTvUrlBase { get; set; } = "https://bgm.tv";
    public string GoogleQueryUrlBase { get; set; } = "https://www.google.com/search?q=";

    public string LocalDataDirectory { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ApplicationName);

    public bool IsDiskCacheEnabled { get; set; } = true;
    public long DiskCacheSizeLimit { get; set; } = 1 << 27;

    public bool PreferChineseNames { get; set; } = false;

    public string EpMainBg { get; set; } = "#7f7fff7f";
    public string EpSpBg { get; set; } = "#7fffff7f";
    public string EpOpBg { get; set; } = "#7f7fffff";
    public string EpEdBg { get; set; } = "#7f7fffff";
    public string EpCmBg { get; set; } = "#7f7f7f7f";
    public string EpMadBg { get; set; } = "#7f7f7f7f";
    public string EpOtherBg { get; set; } = "#7f7f7f7f";
    public string EpNullBg { get; set; } = "#ffff0000";

    public string ConfirmBg { get; set; } = "";
}

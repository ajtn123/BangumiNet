using BangumiNet.Shared.Interfaces;
using System.Reflection;

namespace BangumiNet.Shared;

public class Settings : IApiSettings
{
    public string UserAgent { get; set; } = $"ajtn123/{Constants.ApplicationName}/{Assembly.GetExecutingAssembly().GetName().Version} ({Environment.OSVersion.Platform}) ({Constants.SourceRepository})";
    public string? AuthToken { get; set; } = null;

    public string BangumiTvUrlBase { get; set; } = "https://bgm.tv";
    public Dictionary<string, string> SearchQueryUrlBases { get; set; } = new()
    {
        ["Google"] = "https://www.google.com/search?q=",
        ["Bing"] = "https://www.bing.com/search?q=",
        ["百度"] = "https://www.baidu.com/s?wd=",
        ["DuckDuckGo"] = "https://duckduckgo.com/?q=",
        ["Bangumi"] = "https://bgm.tv/subject_search/",
        ["维基百科"] = "https://zh.wikipedia.org/w/index.php?search=",
        ["Wikipedia"] = "https://en.wikipedia.org/w/index.php?search=",
        ["ウィキペディア"] = "https://ja.wikipedia.org/w/index.php?search="
    };
    public string DefaultSearchEngine { get; set; } = "Google";

    public string LocalDataDirectory { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ApplicationName);

    public bool IsDiskCacheEnabled { get; set; } = true;
    public long DiskCacheSizeLimit { get; set; } = 1 << 27;

    public bool PreferChineseNames { get; set; } = false;
    public bool ShowSplashScreenOnAppStartup { get; set; } = false;
    public bool ShowSplashScreenOnWindowStartup { get; set; } = false;

    public int CollectionPageSize { get; set; } = 30;
    public int SearchPageSize { get; set; } = 10;
    public int EpisodePageSize { get; set; } = 100;
    public int SubjectBrowserPageSize { get; set; } = 30;

    public string EpMainBg { get; set; } = "#7f7fff7f";
    public string EpSpBg { get; set; } = "#7fffff7f";
    public string EpOpBg { get; set; } = "#7f7fffff";
    public string EpEdBg { get; set; } = "#7f7fffff";
    public string EpCmBg { get; set; } = "#7f7f7f7f";
    public string EpMadBg { get; set; } = "#7f7f7f7f";
    public string EpOtherBg { get; set; } = "#7f7f7f7f";

    public string ErrorBg { get; set; } = "#7fff7f7f";
    public string OkBg { get; set; } = "#7f7fff7f";
}

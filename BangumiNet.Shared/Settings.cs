using BangumiNet.Api;
using System.Reflection;

namespace BangumiNet.Shared;

public record class Settings : IApiSettings
{
    public string UserAgent { get; init; } = $"ajtn123/{Constants.ApplicationName}/{Assembly.GetExecutingAssembly().GetName().Version} ({Environment.OSVersion.Platform}) ({Constants.SourceRepository})";
    public string? AuthToken { get; init; } = null;

    public string BangumiTvUrlBase { get; init; } = "https://bgm.tv";
    public Dictionary<string, string> SearchQueryUrlBases { get; init; } = new()
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
    public string DefaultSearchEngine { get; init; } = "Google";

    public string LocalDataDirectory { get; init; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ApplicationName);

    public bool IsDiskCacheEnabled { get; init; } = true;
    public long DiskCacheSizeLimit { get; init; } = 1 << 27;

    public bool SaveLogFile { get; init; } = false;
    public bool CheckUpdateOnStartup { get; init; } = true;

    public bool PreferChineseNames { get; init; } = false;
    public bool ShowSplashScreenOnAppStartup { get; init; } = false;
    public bool ShowSplashScreenOnWindowStartup { get; init; } = false;

    public int CollectionPageSize { get; init; } = 30;
    public int RevisionPageSize { get; init; } = 30;
    public int CommentPageSize { get; init; } = 20;
    public int SearchPageSize { get; init; } = 20;
    public int EpisodePageSize { get; init; } = 100;
    public int SubjectBrowserPageSize { get; init; } = 30;

    public ApplicationTheme ApplicationTheme { get; init; } = ApplicationTheme.System;
    public bool UseSystemAccentColor { get; init; } = true;

    // 所有颜色选项以 Color 开头，反之则不可以 Color 开头

    public string ColorCustomAccent { get; init; } = "#fff09199";

    public string ColorEpMainBg { get; init; } = "#7f7fff7f";
    public string ColorEpSpBg { get; init; } = "#7fffff7f";
    public string ColorEpOpBg { get; init; } = "#7f7fffff";
    public string ColorEpEdBg { get; init; } = "#7f7fffff";
    public string ColorEpCmBg { get; init; } = "#7f7f7f7f";
    public string ColorEpMadBg { get; init; } = "#7f7f7f7f";
    public string ColorEpOtherBg { get; init; } = "#7f7f7f7f";

    public string ColorErrorBg { get; init; } = "#7fff7f7f";
    public string ColorOkBg { get; init; } = "#7f7fff7f";
}

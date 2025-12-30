using System.Text.RegularExpressions;

namespace BangumiNet.Library;

public static partial class Patterns
{
    [GeneratedRegex("(^(Extra|Bonuse?)s?$|特典)", RegexOptions.IgnoreCase)]
    public static partial Regex ExtraDirectory();

    [GeneratedRegex("^CDs?$", RegexOptions.IgnoreCase)]
    public static partial Regex CDDirectory();

    [GeneratedRegex("CDs?$", RegexOptions.IgnoreCase)]
    public static partial Regex CDDirectoryLoose();

    [GeneratedRegex("^Scans?$", RegexOptions.IgnoreCase)]
    public static partial Regex ScanDirectory();

    [GeneratedRegex("Scans?$", RegexOptions.IgnoreCase)]
    public static partial Regex ScanDirectoryLoose();

    [GeneratedRegex("^Sp(ecial)?s?$", RegexOptions.IgnoreCase)]
    public static partial Regex SPDirectory();

    [GeneratedRegex("Sp(ecial)?s?$", RegexOptions.IgnoreCase)]
    public static partial Regex SPDirectoryLoose();

    [GeneratedRegex("^Sub(title)?s?$", RegexOptions.IgnoreCase)]
    public static partial Regex SubtitlesDirectory();

    [GeneratedRegex("Sub(title)?s?$", RegexOptions.IgnoreCase)]
    public static partial Regex SubtitlesDirectoryLoose();
}

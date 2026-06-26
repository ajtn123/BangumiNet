using System.Text.RegularExpressions;

namespace BangumiNet.Common;

public static partial class StickerService
{
    /// <summary>Get remote URL from internal sticker ID.</summary>
    public static string? GetUrlById(int id) => id switch
    {
        >= 1 and <= 16 => $"https://bgm.tv/img/smiles/{id}.gif",
        27 => "https://bgm.tv/img/smiles/bgm/11.gif",
        39 => "https://bgm.tv/img/smiles/bgm/23.gif",
        >= 17 and <= 38 => $"https://bgm.tv/img/smiles/bgm/{id - 16:D2}.png",
        >= 40 and <= 141 => $"https://bgm.tv/img/smiles/tv/{id - 39:D2}.gif",
        _ => null,
    };

    private static readonly HashSet<int> gif500 = [500, 501, 505, 515, 516, 517, 518, 519, 521, 522, 523];

    /// <summary>Get remote URL from N in (bgmN).</summary>
    public static string? GetUrlByCode(int n) => n switch
    {
        >= 1 and <= 125 => GetUrlById(n + 16),
        >= 200 and <= 238 => $"https://bgm.tv/img/smiles/tv_vs/bgm_{n}.png",
        >= 500 and <= 529 => $"https://bgm.tv/img/smiles/tv_500/bgm_{n}.{(gif500.Contains(n) ? "gif" : "png")}",
        _ => null,
    };

    [GeneratedRegex(@"^\((bgm|musume_|blake_)(\d+)\)$")]
    private static partial Regex StickerPattern();

    public static readonly string[] StickerEmojis = [
        "(=A=)", "(=w=)", "(-w=)", "(S_S)", "(=v=)", "(@_@)", "(=W=)", "(TAT)",
        "(T_T)", "(='=)", "(=3=)", "(= =')", "(=///=)", "(=.,=)", "(:P)", "(LOL)",
    ];

    /// <summary>Get remote URL from <c>(xxx)</c>.</summary>
    public static string? GetUrlByCode(string code)
    {
        if (StickerPattern().Match(code) is { Success: true } match &&
            int.TryParse(match.Groups[2].ValueSpan, out int n))
            return match.Groups[1].Value switch
            {
                "bgm" => GetUrlByCode(n),
                "musume_" => $"https://lain.bgm.tv/img/smiles/musume/musume_{n:D2}.gif",
                "blake_" => $"https://lain.bgm.tv/img/smiles/blake/blake_{n:D2}.gif",
                _ => null,
            };

        if (StickerEmojis.IndexOf(code) is int i && i >= 0)
            return GetUrlById(i + 1);

        return null;
    }
}

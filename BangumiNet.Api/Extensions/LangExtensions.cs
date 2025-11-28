using BangumiNet.Api.Interfaces;

namespace BangumiNet.Api.Extensions;

public enum Lang { En, Jp, Cn }

public static class LangExtensions
{
    public static Lang[] DefaultFallbackOrder { get; set; } = [Lang.Cn, Lang.Jp, Lang.En];
    public static string? ToLocalString(this IMultiLang type) => type.ToLocalString(DefaultFallbackOrder);
    public static string? ToLocalString(this IMultiLang type, params Lang[] fallback)
    {
        string? str;
        foreach (var lang in fallback)
        {
            str = lang switch
            {
                Lang.En => type.En,
                Lang.Jp => type.Jp,
                Lang.Cn => type.Cn,
                _ => throw new NotImplementedException(),
            };
            if (!string.IsNullOrWhiteSpace(str)) return str;
        }
        return null;
    }
}

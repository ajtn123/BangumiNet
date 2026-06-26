using System.Collections.Concurrent;
using Avalonia.Media.Imaging;
using BangumiNet.Common;

namespace BangumiNet.Utils;

// https://github.com/bangumi/frontend/blob/8a7627e259b66028ee69d3aa1bcdf34cc6d9f220/packages/utils/bbcode/convert.ts
// TODO: BMO:bmoji (https://bgm.tv/group/topic/438228)
public static class StickerProvider
{
    private static readonly CacheProvider cache = new("Stickers", 1 << 24);
    private static readonly ConcurrentDictionary<string, Bitmap> bitmaps = [];

    public static async Task<Bitmap?> GetStickerById(int? id, CancellationToken ct = default)
    {
        if (id is not int n)
            return null;

        var url = StickerService.GetUrlById(n);
        return await GetStickerByUrl(url, ct);
    }

    public static async Task<Bitmap?> GetStickerByCode(string code, CancellationToken ct = default)
    {
        var url = StickerService.GetUrlByCode(code);
        return await GetStickerByUrl(url, ct);
    }

    public static async Task<Bitmap?> GetStickerByUrl(string? url, CancellationToken ct = default)
    {
        if (url is null)
        {
            return null;
        }
        else if (bitmaps.TryGetValue(url, out var bitmap))
        {
            return bitmap;
        }
        else
        {
            bitmap = await ApiC.GetImageAsync(url, cache: cache, ct: ct);
            if (bitmap is null) return null;
            return bitmaps.AddOrUpdate(url, bitmap, (s, b) => b);
        }
    }

    public static ReactionViewModel[] SubjectCommentReactions => [
        new(0), new(104), new(54), new(140), new(122), new(90), new(88), new(80)
    ];
    public static ReactionViewModel[] CommonReactions => [
        new(0), new(79), new(54), new(140), new(62), new(122), new(104), new(80), new(141), new(88), new(85), new(90)
    ];
}

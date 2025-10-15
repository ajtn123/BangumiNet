﻿using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace BangumiNet.Utils;

// https://github.com/bangumi/frontend/blob/8a7627e259b66028ee69d3aa1bcdf34cc6d9f220/packages/utils/bbcode/convert.ts
public static class StickerProvider
{
    private static readonly Dictionary<Uri, Bitmap> cachedStickerBitmaps = [];
    public static Uri GetStickerUri(int? id)
    {
        var defaultSticker = Common.GetAssetUri("bgm.tv/img/smiles/tv/44.gif");
        if (id is not int sid)
            return defaultSticker;
        else if (sid == 0)
        {
            return Common.GetAssetUri($"bgm.tv/img/smiles/tv/44.gif");
        }
        else if (sid >= 1 && sid < 17)
        {
            return Common.GetAssetUri($"bgm.tv/img/smiles/{sid}.gif");
        }
        else if (sid >= 17 && sid < 40)
        {
            string bgmId = (sid - 16).ToString().PadLeft(2, '0');

            if (bgmId == "11")
                return Common.GetAssetUri($"bgm.tv/img/smiles/bgm/{bgmId}.gif");
            if (bgmId == "23")
                return Common.GetAssetUri($"bgm.tv/img/smiles/bgm/{bgmId}.gif");
            else
                return Common.GetAssetUri($"bgm.tv/img/smiles/bgm/{bgmId}.png");

        }
        else if (sid >= 40 && sid < 141)
        {
            string tvId = (sid - 39).ToString().PadLeft(2, '0');
            return Common.GetAssetUri($"bgm.tv/img/smiles/tv/{tvId}.gif");
        }
        else return defaultSticker;
    }
    public static Bitmap GetStickerBitmap(int? id)
    {
        var uri = GetStickerUri(id);
        if (cachedStickerBitmaps.TryGetValue(uri, out Bitmap? value))
            return value;

        var bitmap = new Bitmap(AssetLoader.Open(uri));
        cachedStickerBitmaps[uri] = bitmap;
        return bitmap;
    }

    public static ReactionViewModel[] SubjectCommentReactions => [
        new(0), new(104), new(54), new(140), new(122), new(90), new(88), new(80)
    ];
    public static ReactionViewModel[] EpisodeCommentReactions => [
        new(0), new(79), new(54), new(140), new(62), new(122), new(104), new(80), new(141), new(88), new(85), new(90)
    ];
}

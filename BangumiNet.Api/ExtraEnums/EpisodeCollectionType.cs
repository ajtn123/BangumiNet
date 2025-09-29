namespace BangumiNet.Api.ExtraEnums;

public enum EpisodeCollectionType
{
    Uncollected = 0,
    Wish = 1,
    Done = 2,
    Dropped = 3,
}
public static partial class EnumExtensions
{
    public static string ToStringSC(this EpisodeCollectionType type)
        => type switch
        {
            EpisodeCollectionType.Uncollected => "未收藏",
            EpisodeCollectionType.Wish => "想看",
            EpisodeCollectionType.Done => "看过",
            EpisodeCollectionType.Dropped => "抛弃",
            _ => throw new NotImplementedException(),
        };
}

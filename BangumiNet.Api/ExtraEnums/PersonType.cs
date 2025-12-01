using BangumiNet.Api.V0.Models;

namespace BangumiNet.Api.ExtraEnums;

public static partial class EnumExtensions
{
    public static string ToStringSC(this PersonCareer career)
        => career switch
        {
            PersonCareer.Producer => "制作人",
            PersonCareer.Mangaka => "漫画家",
            PersonCareer.Artist => "艺术家",
            PersonCareer.Seiyu => "声优",
            PersonCareer.Writer => "作家",
            PersonCareer.Illustrator => "插画师",
            PersonCareer.Actor => "演员",
            _ => throw new NotImplementedException(),
        };
    public static PersonCareer? ParsePersonCareer(string career)
        => career switch
        {
            "producer" => PersonCareer.Producer,
            "mangaka" => PersonCareer.Mangaka,
            "artist" => PersonCareer.Artist,
            "seiyu" => PersonCareer.Seiyu,
            "writer" => PersonCareer.Writer,
            "illustrator" => PersonCareer.Illustrator,
            "actor" => PersonCareer.Actor,
            _ => null,
        };
}

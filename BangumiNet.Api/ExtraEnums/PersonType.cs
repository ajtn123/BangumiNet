using BangumiNet.Api.V0.Models;

namespace BangumiNet.Api.ExtraEnums;

public enum PersonType
{
    /// <summary>个人</summary>
    Individual = 1,
    /// <summary>公司</summary>
    Company = 2,
    /// <summary>组合</summary>
    Group = 3
}

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
    public static string ToStringSC(this PersonType career)
        => career switch
        {
            PersonType.Individual => "个人",
            PersonType.Company => "公司",
            PersonType.Group => "组合",
            _ => throw new NotImplementedException(),
        };
}

namespace BangumiNet.Api.ExtraEnums;

/// <summary>性别</summary>
public enum Gender
{
    /// <summary>男</summary>
    Male = 0,
    /// <summary>女</summary>
    Female = 1,
    /// <summary>其他</summary>
    Other = 2,
}

public static partial class EnumExtensions
{
    public static Gender? TryParseGender(string? str)
        => str switch
        {
            "男" => Gender.Male,
            "女" => Gender.Female,
            null => null,
            _ => Gender.Other,
        };
}

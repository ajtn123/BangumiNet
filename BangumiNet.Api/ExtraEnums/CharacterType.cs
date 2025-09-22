namespace BangumiNet.Api.ExtraEnums;

public enum CharacterType
{
    /// <summary>角色</summary>
    Individual = 1,
    /// <summary>机体</summary>
    Bot = 2,
    /// <summary>舰船</summary>
    Ship = 3,
    /// <summary>组织</summary>
    Organization = 4,
}
public static partial class EnumExtensions
{
    public static string ToStringSC(this CharacterType type)
        => type switch
        {
            CharacterType.Individual => "角色",
            CharacterType.Bot => "机体",
            CharacterType.Ship => "舰船",
            CharacterType.Organization => "组织",
            _ => throw new NotImplementedException(),
        };
}


namespace BangumiNet.Api.ExtraEnums;

public enum CharacterType
{
    Individual = 1,
    Bot = 2,
    Ship = 3,
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


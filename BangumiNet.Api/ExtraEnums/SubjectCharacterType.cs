namespace BangumiNet.Api.ExtraEnums;

// https://github.com/bangumi/server/blob/master/web/res/character.go
public enum SubjectCharacterType
{
    Main = 1,
    Supporting = 2,
    Cameo = 3,
    Minor = 4,
    Narrator = 5,
    VoiceBank = 6,
}
public static partial class EnumExtensions
{
    public static string ToStringSC(this SubjectCharacterType type)
        => type switch
        {
            SubjectCharacterType.Main => "主角",
            SubjectCharacterType.Supporting => "配角",
            SubjectCharacterType.Cameo => "客串",
            SubjectCharacterType.Minor => "闲角",
            SubjectCharacterType.Narrator => "旁白",
            SubjectCharacterType.VoiceBank => "声库",
            _ => throw new NotImplementedException(),
        };
}

namespace BangumiNet.Shared
{
    public enum ItemType
    {
        Subject,
        Episode,
        Character,
        Person,
        User,
    }
}
namespace BangumiNet.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static string ToStringSC(this ItemType type) => type switch
        {
            ItemType.Subject => "项目",
            ItemType.Episode => "话",
            ItemType.Character => "角色",
            ItemType.Person => "人物",
            ItemType.User => "用户",
            _ => throw new NotImplementedException(),
        };
    }
}
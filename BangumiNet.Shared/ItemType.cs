namespace BangumiNet.Shared
{
    public enum ItemType
    {
        Subject,
        Episode,
        Character,
        Person,
        User,
        Topic,
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
            ItemType.Topic => "话题",
            _ => throw new NotImplementedException(),
        };
    }
}
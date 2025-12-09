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
        Group,
        Timeline,
        Revision,
        Blog,
        Photo,
        Index,
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
            ItemType.Group => "小组",
            ItemType.Blog => "日志",
            ItemType.Photo => "图片",
            ItemType.Index => "目录",
            _ => throw new NotImplementedException(),
        };
    }
}
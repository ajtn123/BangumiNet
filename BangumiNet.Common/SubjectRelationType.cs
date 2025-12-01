using BangumiNet.Common.Attributes;
using System.ComponentModel;

namespace BangumiNet.Common;

public enum SubjectRelationType
{
    [NameEn("Adaptation")]
    [NameCn("改编")]
    [Description("同系列不同平台作品（如柯南漫画与动画版、柯南漫画与动画版、CLANNAD 游戏与动画版）")]
    Adaptation = 1,

    // Anime & Real

    [NameEn("Prequel")]
    [NameCn("前传")]
    [Description("发生在故事之前")]
    Prequel = 2,

    [NameEn("Sequel")]
    [NameCn("续集")]
    [Description("发生在故事之后")]
    Sequel = 3,

    [NameEn("Summary")]
    [NameCn("总集篇")]
    [Description("对故事的概括版本")]
    Summary = 4,

    [NameEn("Full Story")]
    [NameCn("全集")]
    [Description("相对于剧场版/总集篇的完整故事")]
    FullStory = 5,

    [NameEn("Side Story")]
    [NameCn("番外篇")]
    SideStory = 6,

    [NameEn("Same Setting")]
    [NameCn("相同世界观")]
    [Description("发生在同一个世界观/时间线下，不同的出演角色")]
    SameSetting = 8,

    [NameEn("Alternative Setting")]
    [NameCn("不同世界观")]
    [Description("相同的主演角色，不同的世界观/时间线设定")]
    AlternativeSetting = 9,

    [NameEn("Alternative Version")]
    [NameCn("不同演绎")]
    [Description("相同设定、角色，不同的演绎方式（如EVA原作与新剧场版)")]
    AlternativeVersion = 10,

    [NameEn("Spin-off")]
    [NameCn("衍生")]
    [Description("世界观相同，角色主线与有关联或来自主线，但又非主线的主角们")]
    SpinOff = 11,

    [NameEn("Parent Story")]
    [NameCn("主线故事")]
    ParentStory = 12,

    [NameEn("Character")]
    [NameCn("角色出演")]
    [Description("相同角色，没有关联的故事")]
    Character = 7,

    [NameEn("Collaboration")]
    [NameCn("联动")]
    [Description("出现了被关联作品中的角色")]
    Collaboration = 14,

    [NameEn("Other")]
    [NameCn("其他")]
    Other = 99,

    // Book

    [NameEn("Offprint")]
    [NameCn("单行本")]
    Offprint = 1003,

    [NameEn("Series")]
    [NameCn("系列")]
    BookSeries = 1002,

    [NameEn("Album")]
    [NameCn("画集")]
    IllustrationAlbum = 1004,

    [NameEn("Version")]
    [NameCn("不同版本")]
    BookVersion = 1010,

    [NameEn("Prequel")]
    [NameCn("前传")]
    [Description("发生在故事之前")]
    BookPrequel = 1005,

    [NameEn("Sequel")]
    [NameCn("续集")]
    [Description("发生在故事之后")]
    BookSequel = 1006,

    [NameEn("Side Story")]
    [NameCn("番外篇")]
    BookSideStory = 1007,

    [NameEn("Parent Story")]
    [NameCn("主线故事")]
    BookParentStory = 1008,

    [NameEn("Alternative Version")]
    [NameCn("不同演绎")]
    [Description("相同设定、角色，不同的演绎方式")]
    BookAlternativeVersion = 1015,

    [NameEn("Character")]
    [NameCn("角色出演")]
    [Description("相同角色，没有关联的故事")]
    BookCharacter = 1011,

    [NameEn("Same setting")]
    [NameCn("相同世界观")]
    [Description("发生在同一个世界观/时间线下，不同的出演角色")]
    BookSameSetting = 1012,

    [NameEn("Alternative setting")]
    [NameCn("不同世界观")]
    [Description("相同的出演角色，不同的世界观/时间线设定")]
    BookAlternativeSetting = 1013,

    [NameEn("Collaboration")]
    [NameCn("联动")]
    [Description("出现了被关联作品中的角色")]
    BookCollaboration = 1014,

    [NameEn("Other")]
    [NameCn("其他")]
    BookOther = 1099,

    // Music

    [NameEn("OST")]
    [NameCn("原声集")]
    OST = 3001,

    [NameEn("Character Song")]
    [NameCn("角色歌")]
    CharacterSong = 3002,

    [NameEn("Opening Song")]
    [NameCn("片头曲")]
    OpeningSong = 3003,

    [NameEn("Ending Song")]
    [NameCn("片尾曲")]
    EndingSong = 3004,

    [NameEn("Insert Song")]
    [NameCn("插入歌")]
    InsertSong = 3005,

    [NameEn("Image Song")]
    [NameCn("印象曲")]
    ImageSong = 3006,

    [NameEn("Drama")]
    [NameCn("广播剧")]
    Drama = 3007,

    [NameEn("Other")]
    [NameCn("其他")]
    MusicOther = 3099,

    // Game

    [NameEn("Prequel")]
    [NameCn("前传")]
    [Description("发生在故事之前/或作品发售之前")]
    GamePrequel = 4002,

    [NameEn("Sequel")]
    [NameCn("续集")]
    [Description("发生在故事之后/或作品发售之后")]
    GameSequel = 4003,

    [NameEn("Side Story")]
    [NameCn("外传")]
    GameSideStory = 4006,

    [NameEn("Parent Story")]
    [NameCn("主线故事")]
    GameParentStory = 4012,

    [NameEn("Same Setting")]
    [NameCn("相同世界观")]
    [Description("发生在同一个世界观/时间线下，不同的出演角色")]
    GameSameSetting = 4008,

    [NameEn("Alternative setting")]
    [NameCn("不同世界观")]
    [Description("相同的出演角色，不同的世界观/时间线设定")]
    GameAlternativeSetting = 4009,

    [NameEn("Alternative Version")]
    [NameCn("不同演绎")]
    [Description("相同设定、角色，不同的演绎方式")]
    GameAlternativeVersion = 4010,

    [NameEn("Character")]
    [NameCn("角色出演")]
    [Description("相同角色，没有关联的故事")]
    GameCharacter = 4007,

    [NameEn("Collaboration")]
    [NameCn("联动")]
    [Description("出现了被关联作品中的角色")]
    [SkipViceVersa]
    GameCollaboration = 4014,

    //[NameEn("Version")]
    //[NameCn("不同版本")]
    //[Description("相同故事、角色，画面、音乐或系统改进")]
    //GameVersion = 4011,

    //[NameEn("Main Version")]
    //[NameCn("主版本")]
    //[Description("游戏最初发售时的版本")]
    //GameMainVersion = 4013,

    [NameEn("Version")]
    [NameCn("不同版本")]
    [Description("相同故事、角色，画面、音乐或系统改进")]
    GameVersion = 4016,

    [NameEn("Main Version")]
    [NameCn("主版本")]
    [Description("游戏最初发售时的版本")]
    GameMainVersion = 4017,

    [NameEn("DLC")]
    [NameCn("扩展包")]
    GameDLC = 4015,

    [NameEn("Collection")]
    [NameCn("扩展包")]
    [Description("收录本作品的合集条目")]
    GameCollection = 4018,

    [NameEn("In Collection")]
    [NameCn("收录作品")]
    [Description("合集条目中收录的作品")]
    GameInCollection = 4019,

    [NameEn("Other")]
    [NameCn("其他")]
    [SkipViceVersa]
    GameOther = 4099,
}

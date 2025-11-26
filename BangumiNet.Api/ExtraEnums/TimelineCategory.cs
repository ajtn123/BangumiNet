namespace BangumiNet.Api.ExtraEnums;

//https://github.com/bangumi/server-private/blob/master/lib/timeline/type.ts
public enum TimelineCategory
{
    Daily = 1,
    Wiki = 2,
    Subject = 3,
    Progress = 4,
    Status = 5,
    Blog = 6,
    Index = 7,
    Mono = 8,
    Doujin = 9,
}

public static class TimelineTypes
{
    public enum Daily
    {
        Mystery = 0,
        Register = 1,
        AddFriend = 2,
        JoinGroup = 3,
        CreateGroup = 4,
        JoinEden = 5,
    }
    public enum Wiki
    {
        AddBook = 1,
        AddAnime = 2,
        AddDisc = 3,
        AddGame = 4,
        AddBookSeries = 5,
        AddReal = 6,
    }
    public enum Subject
    {
        WishBook = 1,
        WishAnime = 2,
        WishMusic = 3,
        WishGame = 4,
        DoneBook = 5,
        DoneAnime = 6,
        DoneMusic = 7,
        DoneGame = 8,
        DoingBook = 9,
        DoingAnime = 10,
        DoingMusic = 11,
        DoingGame = 12,
        OnHold = 13,
        Dropped = 14,
    }
    public enum Progress
    {
        DoneBatch = 0,
        Wish = 1,
        Done = 2,
        Dropped = 3,
    }
    public enum Status
    {
        Sign = 0,
        Tsukkomi = 1,
        Nickname = 2,
    }
    public enum Mono
    {
        Created = 0,
        Collected = 1,
    }
    public enum Doujin
    {
        AddWork = 0,
        CollectWork = 1,
        CreateClub = 2,
        FollowClub = 3,
        FollowEvent = 4,
        AttendEvent = 5,
    }

    public static Enum? Parse(this TimelineCategory category, int? type)
        => category switch
        {
            TimelineCategory.Daily => (Daily?)type,
            TimelineCategory.Wiki => (Wiki?)type,
            TimelineCategory.Subject => (Subject?)type,
            TimelineCategory.Progress => (Progress?)type,
            TimelineCategory.Status => (Status?)type,
            TimelineCategory.Blog => null,
            TimelineCategory.Index => null,
            TimelineCategory.Mono => (Mono?)type,
            TimelineCategory.Doujin => (Doujin?)type,
            _ => throw new NotImplementedException(),
        };
}

public enum MemoMonoCategory
{
    Character = 1,
    Person = 2,
}

public static partial class EnumExtensions
{
    public static string ToStringSC(this TimelineCategory category) => category switch
    {
        TimelineCategory.Daily => "日常行为",
        TimelineCategory.Wiki => "维基操作",
        TimelineCategory.Subject => "收藏条目",
        TimelineCategory.Progress => "收视进度",
        TimelineCategory.Status => "状态",
        TimelineCategory.Blog => "日志",
        TimelineCategory.Index => "目录",
        TimelineCategory.Mono => "人物",
        TimelineCategory.Doujin => "天窗",
        _ => throw new NotImplementedException(),
    };
    public static string ToStringSC(this TimelineTypes.Daily type) => type switch
    {
        TimelineTypes.Daily.Mystery => "神秘的行动",
        TimelineTypes.Daily.Register => "注册",
        TimelineTypes.Daily.AddFriend => "添加好友",
        TimelineTypes.Daily.JoinGroup => "加入小组",
        TimelineTypes.Daily.CreateGroup => "创建小组",
        TimelineTypes.Daily.JoinEden => "加入乐园",
        _ => throw new NotImplementedException(),
    };
    public static string ToStringSC(this TimelineTypes.Wiki type) => type switch
    {
        TimelineTypes.Wiki.AddBook => "添加了新书",
        TimelineTypes.Wiki.AddAnime => "添加了新动画",
        TimelineTypes.Wiki.AddDisc => "添加了新唱片",
        TimelineTypes.Wiki.AddGame => "添加了新游戏",
        TimelineTypes.Wiki.AddBookSeries => "添加了新图书系列",
        TimelineTypes.Wiki.AddReal => "添加了新影视",
        _ => throw new NotImplementedException(),
    };
    public static string ToStringSC(this TimelineTypes.Subject type) => type switch
    {
        TimelineTypes.Subject.WishBook => "想读",
        TimelineTypes.Subject.WishAnime => "想看",
        TimelineTypes.Subject.WishMusic => "想听",
        TimelineTypes.Subject.WishGame => "想玩",
        TimelineTypes.Subject.DoneBook => "读过",
        TimelineTypes.Subject.DoneAnime => "看过",
        TimelineTypes.Subject.DoneMusic => "听过",
        TimelineTypes.Subject.DoneGame => "玩过",
        TimelineTypes.Subject.DoingBook => "在读",
        TimelineTypes.Subject.DoingAnime => "在看",
        TimelineTypes.Subject.DoingMusic => "在听",
        TimelineTypes.Subject.DoingGame => "在玩",
        TimelineTypes.Subject.OnHold => "搁置",
        TimelineTypes.Subject.Dropped => "抛弃",
        _ => throw new NotImplementedException(),
    };
    public static string ToStringSC(this TimelineTypes.Progress type) => type switch
    {
        TimelineTypes.Progress.DoneBatch => "批量完成",
        TimelineTypes.Progress.Wish => "想看",
        TimelineTypes.Progress.Done => "看过",
        TimelineTypes.Progress.Dropped => "抛弃",
        _ => throw new NotImplementedException(),
    };
    public static string ToStringSC(this TimelineTypes.Status type) => type switch
    {
        TimelineTypes.Status.Sign => "更新签名",
        TimelineTypes.Status.Tsukkomi => "吐槽",
        TimelineTypes.Status.Nickname => "修改昵称",
        _ => throw new NotImplementedException(),
    };
    public static string ToStringSC(this TimelineTypes.Mono type) => type switch
    {
        TimelineTypes.Mono.Created => "创建",
        TimelineTypes.Mono.Collected => "收藏",
        _ => throw new NotImplementedException(),
    };
    public static string ToStringSC(this TimelineTypes.Doujin type) => type switch
    {
        TimelineTypes.Doujin.AddWork => "添加作品",
        TimelineTypes.Doujin.CollectWork => "收藏作品",
        TimelineTypes.Doujin.CreateClub => "创建社团",
        TimelineTypes.Doujin.FollowClub => "关注社团",
        TimelineTypes.Doujin.FollowEvent => "关注活动",
        TimelineTypes.Doujin.AttendEvent => "参加活动",
        _ => throw new NotImplementedException(),
    };
}
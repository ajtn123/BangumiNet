namespace BangumiNet.Api.ExtraEnums;

public enum ReportType
{
    User = 6,
    GroupTopic = 7,
    GroupReply = 8,
    SubjectTopic = 9,
    SubjectReply = 10,
    EpisodeReply = 11,
    CharacterReply = 12,
    PersonReply = 13,
    Blog = 14,
    BlogReply = 15,
    Timeline = 16,
    TimelineReply = 17,
    Index = 18,
    IndexReply = 19,
}

public enum ReportReason
{
    Abuse = 1,
    Spam = 2,
    Political = 3,
    Illegal = 4,
    Privacy = 5,
    CheatScore = 6,
    Flame = 7,
    Advertisement = 8,
    Spoiler = 9,
    Other = 99,
}

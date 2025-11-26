namespace BangumiNet.Api.ExtraEnums;
// https://github.com/bangumi/server-private/blob/master/lib/types/common.ts
public enum RevisionType
{
    SubjectEdit = 1,
    SubjectLock = 103,
    SubjectUnlock = 104,
    SubjectMerge = 11,
    SubjectErase = 12,
    SubjectRelation = 17,
    SubjectCharacterRelation = 5,
    SubjectCastRelation = 6,
    SubjectPersonRelation = 10,
    CharacterEdit = 2,
    CharacterMerge = 13,
    CharacterErase = 14,
    CharacterSubjectRelation = 4,
    CharacterCastRelation = 7,
    PersonEdit = 3,
    PersonMerge = 15,
    PersonErase = 16,
    PersonCastRelation = 8,
    PersonSubjectRelation = 9,
    EpisodeEdit = 18,
    EpisodeMerge = 181,
    EpisodeMove = 182,
    EpisodeLock = 183,
    EpisodeUnlock = 184,
    EpisodeErase = 185,
}

public static partial class EnumExtensions
{
    public static string ToStringSC(this RevisionType type) => type switch
    {
        RevisionType.SubjectEdit => "条目编辑",
        RevisionType.SubjectLock => "条目锁定",
        RevisionType.SubjectUnlock => "条目解锁",
        RevisionType.SubjectMerge => "条目合体",
        RevisionType.SubjectErase => "条目删除",
        RevisionType.SubjectRelation => "条目关联",
        RevisionType.SubjectCharacterRelation => "条目->角色关联",
        RevisionType.SubjectCastRelation => "条目->声优关联",
        RevisionType.SubjectPersonRelation => "条目->人物关联",
        RevisionType.CharacterEdit => "角色编辑",
        RevisionType.CharacterMerge => "角色合体",
        RevisionType.CharacterErase => "角色删除",
        RevisionType.CharacterSubjectRelation => "角色->条目关联",
        RevisionType.CharacterCastRelation => "角色->声优关联",
        RevisionType.PersonEdit => "人物编辑",
        RevisionType.PersonMerge => "人物合体",
        RevisionType.PersonErase => "人物删除",
        RevisionType.PersonCastRelation => "人物->声优关联",
        RevisionType.PersonSubjectRelation => "人物->条目关联",
        RevisionType.EpisodeEdit => "章节编辑",
        RevisionType.EpisodeMerge => "章节合体",
        RevisionType.EpisodeMove => "章节移动",
        RevisionType.EpisodeLock => "章节锁定",
        RevisionType.EpisodeUnlock => "章节解锁",
        RevisionType.EpisodeErase => "章节删除",
        _ => throw new NotImplementedException(),
    };
}

using BangumiNet.Api.ExtraEnums;
using BangumiNet.Common;

namespace BangumiNet.Models;

public record class IndexRelation
{
    public IndexRelatedCategory? Category { get; init; }
    public SubjectType? Type { get; init; }
    public int? Id { get; init; }
    public int? IndexId { get; init; }
    public int? ItemId { get; init; }
    public int? Order { get; init; }
    public string? Comment { get; init; }
    public string? Award { get; init; }
    public DateTimeOffset? CreationTime { get; init; }
}

using BangumiNet.Api.ExtraEnums;

namespace BangumiNet.Api.Html.Models;

public class Comment
{
    public required string? Username { get; set; }
    public required string AvatarUrl { get; set; }
    public required string? Content { get; set; }
    public required string? Nickname { get; set; }
    public required int? Score { get; set; }
    public required CollectionType? CollectionType { get; set; }
    public required DateTimeOffset? CollectionTime { get; set; }
    public required string? CollectionTimeString { get; set; }
}

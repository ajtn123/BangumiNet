using BangumiNet.Shared;

namespace BangumiNet.Api.Html.Models;

public class CommentPage
{
    public ItemType Type { get; set; }
    public int Id { get; set; }
    public int Page { get; set; }
    public int? TotalPage { get; set; }
    public required List<Comment> Comments { get; set; }
}

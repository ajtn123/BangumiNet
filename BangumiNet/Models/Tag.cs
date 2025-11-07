using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;

namespace BangumiNet.Models;

public class Tag(Subjects sub) : ITag
{
    /// <summary><c>name</c>，标签名称。</summary>
    public string? Name { get; set; } = sub.AdditionalData.TryGet("name")?.ToString();
    /// <summary><c>count</c>，标签被添加次数。</summary>
    public int? Count { get; set; } = Convert.ToInt32(sub.AdditionalData.TryGet("count"));
    /// <summary><c>total_cont</c>，好像总是为零。</summary>
    public int? TotalCount { get; set; } = Convert.ToInt32(sub.AdditionalData.TryGet("total_cont"));
}

using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Utils;

namespace BangumiNet.Models;

public class Tag(Subjects sub) : ITag
{
    public string? Name { get; set; } = sub.AdditionalData["name"].ToString();
    public int? Count { get; set; } = Common.NumberToInt(sub.AdditionalData["count"]);
}

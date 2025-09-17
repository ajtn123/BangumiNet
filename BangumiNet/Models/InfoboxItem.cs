using BangumiNet.Api.V0.Models;

namespace BangumiNet.Models;

public class InfoboxItem(Subjects sub)
{
    public string? Key { get; set; } = sub.Key;
    public string? Value { get; set; } = sub.Value?.String;
}

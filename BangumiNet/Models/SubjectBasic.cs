using BangumiNet.Api.ExtraEnums;

namespace BangumiNet.Models;

public class SubjectBasic
{
    public string? Name { get; set; }
    public string? NameCn { get; set; }
    public int? Id { get; set; }
    public SubjectType? Type { get; set; }
}

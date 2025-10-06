using BangumiNet.Api.V0.Models;

namespace BangumiNet.Api.Interfaces;

public interface IRevision
{
    int? Id { get; set; }
    int? Type { get; set; }
    Creator? Creator { get; set; }
    string? Summary { get; set; }
    DateTimeOffset? CreatedAt { get; set; }
}

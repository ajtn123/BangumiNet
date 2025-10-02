namespace BangumiNet.Api.Interfaces;

public interface IPagedResponse
{
    int? Total { get; set; }
    int? Limit { get; set; }
    int? Offset { get; set; }
}

namespace BangumiNet.Api.Interfaces;

public interface IPagedRequest
{
    int? Limit { get; set; }
    int? Offset { get; set; }
}

public interface IPagedResponse
{
    int? Total { get; }
    int? Limit { get; }
    int? Offset { get; }
}

public interface IPagedDataResponse<out TData> where TData : IEnumerable<object>
{
    int? Total { get; }
    TData? Data { get; }
}

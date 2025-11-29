namespace BangumiNet.Api.Interfaces;

public interface IPagedRequest
{
    int? Limit { get; set; }
    int? Offset { get; set; }
}

public interface IPagedResponse
{
    int? Total { get; }
}

public interface IPagedResponse<out TData> : IPagedResponse where TData : IEnumerable<object>
{
    TData? Data { get; }
}

public interface IPagedResponseFull : IPagedResponse, IPagedRequest;
public interface IPagedResponseFull<out TData> : IPagedResponse<TData>, IPagedResponseFull where TData : IEnumerable<object>;

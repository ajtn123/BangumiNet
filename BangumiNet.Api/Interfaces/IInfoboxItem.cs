namespace BangumiNet.Api.Interfaces;

public interface IInfoboxItem<out TValue> where TValue : IEnumerable<IInfoboxKeyValuePair>
{
    string? Key { get; }
    TValue? Values { get; }
}

public interface IInfoboxKeyValuePair
{
    string? K { get; }
    string? V { get; }
}

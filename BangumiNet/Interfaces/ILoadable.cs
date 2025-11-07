namespace BangumiNet.Interfaces;

public interface ILoadable
{
    Task Load(CancellationToken cancellationToken = default);
}

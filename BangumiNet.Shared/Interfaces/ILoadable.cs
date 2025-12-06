namespace BangumiNet.Shared.Interfaces;

public interface ILoadable
{
    Task Load(CancellationToken cancellationToken = default);
}

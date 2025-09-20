namespace BangumiNet.Shared.Interfaces;

public interface INeighboring
{
    public INeighboring? Prev { get; set; }
    public INeighboring? Next { get; set; }
}

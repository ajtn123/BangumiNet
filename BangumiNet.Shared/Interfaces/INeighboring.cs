namespace BangumiNet.Shared.Interfaces;

public interface INeighboring
{
    INeighboring? Prev { get; set; }
    INeighboring? Next { get; set; }
}

namespace BangumiNet.Api.Interfaces;

public interface IImages
{
    string? Large { get; }
    string? Medium { get; }
    string? Small { get; }
}
public interface IImagesGrid : IImages
{
    string? Grid { get; }
}
public interface IImagesCommon : IImages
{
    string? Common { get; }
}
public interface IImagesFull : IImagesCommon, IImagesGrid;

public record class ImageSet : IImagesFull
{
    public string? Common { get; init; }
    public string? Large { get; init; }
    public string? Medium { get; init; }
    public string? Small { get; init; }
    public string? Grid { get; init; }
}

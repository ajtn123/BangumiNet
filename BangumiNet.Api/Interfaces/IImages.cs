using BangumiNet.Api.Misc;

namespace BangumiNet.Api.Interfaces;

public interface IImages
{
    string? Large { get; }
    string? Medium { get; }
    string? Small { get; }

    /// <inheritdoc cref="BangumiImage.GetResizedImage(string, int, int)"/>
    string Resize(int width = 0, int height = 0) => BangumiImage.GetResizedImage(Large ?? Medium ?? Small!, width, height);
    string Original() => BangumiImage.GetOriginalImage(Large ?? Medium ?? Small!);
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

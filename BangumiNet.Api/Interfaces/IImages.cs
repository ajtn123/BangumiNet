namespace BangumiNet.Api.Interfaces;

public interface IImages
{
    string? Large { get; set; }
    string? Medium { get; set; }
    string? Small { get; set; }
}
public interface IImagesGrid : IImages
{
    string? Grid { get; set; }
}
public interface IImagesCommon : IImages
{
    string? Common { get; set; }
}
public interface IImagesFull : IImagesCommon, IImagesGrid { }

using BangumiNet.Api.Interfaces;

namespace BangumiNet.Models;

public class ImageSet : IImagesFull
{
    public string? Common { get; set; }
    public string? Large { get; set; }
    public string? Medium { get; set; }
    public string? Small { get; set; }
    public string? Grid { get; set; }
}

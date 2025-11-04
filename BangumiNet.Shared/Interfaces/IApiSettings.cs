namespace BangumiNet.Shared.Interfaces;

public interface IApiSettings
{
    string UserAgent { get; set; }
    string? AuthToken { get; set; }
}

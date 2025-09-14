namespace BangumiNet.Shared;

public interface IApiSettings
{
    string UserAgent { get; set; }
    string? AuthToken { get; set; }
}

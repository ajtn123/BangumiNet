namespace BangumiNet.Api;

public interface IApiSettings
{
    string UserAgent { get; }
    string? AuthToken { get; }
}

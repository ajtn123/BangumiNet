namespace BangumiNet.Api;

public interface IApiSettings
{
    string UserAgent { get; set; }
    string? AuthToken { get; set; }
}

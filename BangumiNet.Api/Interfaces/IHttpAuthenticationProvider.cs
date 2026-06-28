namespace BangumiNet.Api.Interfaces;

public interface IHttpAuthenticationProvider
{
    Task AuthenticateRequestAsync(HttpRequestMessage request);
}

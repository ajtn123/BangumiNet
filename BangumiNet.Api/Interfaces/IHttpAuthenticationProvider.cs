namespace BangumiNet.Api.Interfaces;

public interface IHttpAuthenticationProvider
{
    void AuthenticateRequestAsync(HttpRequestMessage request);
}

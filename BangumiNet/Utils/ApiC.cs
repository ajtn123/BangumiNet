using Avalonia.Media.Imaging;
using BangumiNet.Api;
using BangumiNet.Api.V0.Models;
using BangumiNet.Api.V0.V0.Me;
using System.Net.Http;

namespace BangumiNet.Utils;

public class ApiC
{
    public static Clients Clients { get; private set; } = ClientBuilder.Build(SettingProvider.CurrentSettings);
    public static Api.V0.V0.V0RequestBuilder V0 => Clients.V0Client.V0;
    public static HttpClient HttpClient => Clients.HttpClient;

    public static async Task<Bitmap?> GetImageAsync(string? url, bool useCache = true)
    {
        if (string.IsNullOrWhiteSpace(url)) return null;
        useCache = useCache && SettingProvider.CurrentSettings.IsDiskCacheEnabled;
        Bitmap? result = null;

        if (useCache)
            try
            {
                using var cacheStream = CacheProvider.ReadCache(url);
                if (cacheStream is not null)
                    result = new Bitmap(cacheStream);
            }
            catch (Exception e) { Trace.TraceError(e.Message); CacheProvider.DeleteCache(url); }
        if (result != null) return result;

        var stream = (await HttpClient.GetStreamAsync(url)).Clone();
        if (useCache) CacheProvider.WriteCache(url, stream);
        result = new Bitmap(stream);

        return result;
    }

    public static void RebuildClients()
        => Clients = ClientBuilder.Build(SettingProvider.CurrentSettings);

    /// <summary>
    /// 发起请求并转换为相应的 ViewModel 类型
    /// </summary>
    /// <typeparam name="T">ViewModel 的类型</typeparam>
    /// <param name="id">ID (如果需求)</param>
    /// <returns>ViewModel</returns>
    public static async Task<T?> GetViewModelAsync<T>(int? id = null, string? username = null) where T : ViewModelBase
    {
        if (typeof(T) == typeof(SubjectViewModel) && id is int subjectId)
        {
            Subject? subject = null;
            try { subject = await V0.Subjects[subjectId].GetAsync(); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (subject is null) return null;
            else return new SubjectViewModel(subject) as T;
        }
        else if (typeof(T) == typeof(EpisodeViewModel) && id is int episodeId)
        {
            EpisodeDetail? episode = null;
            try { episode = await V0.Episodes[episodeId].GetAsync(); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (episode is null) return null;
            else return new EpisodeViewModel(episode) as T;
        }
        else if (typeof(T) == typeof(CharacterViewModel) && id is int characterId)
        {
            Character? character = null;
            try { character = await V0.Characters[characterId].GetAsync(); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (character is null) return null;
            else return new CharacterViewModel(character) as T;
        }
        else if (typeof(T) == typeof(PersonViewModel) && id is int personId)
        {
            PersonDetail? person = null;
            try { person = await V0.Persons[personId].GetAsync(); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (person is null) return null;
            else return new PersonViewModel(person) as T;
        }
        else if (typeof(T) == typeof(UserViewModel) && username is string uid)
        {
            User? user = null;
            try { user = await V0.Users[uid].GetAsync(); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (user is null) return null;
            else return new UserViewModel(user) as T;
        }
        else if (typeof(T) == typeof(UserViewModel) && username is null)
        {
            MeGetResponse? me = null;
            try { me = await V0.Me.GetAsMeGetResponseAsync(); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (me is null) return null;
            else return new UserViewModel(me) as T;
        }

        else return null;
    }
}

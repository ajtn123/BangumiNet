using Avalonia.Media.Imaging;
using BangumiNet.Api;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Legacy.Calendar;
using BangumiNet.Api.V0.Models;
using BangumiNet.Api.V0.V0.Me;
using System.Net.Http;

namespace BangumiNet.Utils;

public class ApiC
{
    public static Clients Clients { get; private set; } = ClientBuilder.Build(SettingProvider.CurrentSettings);
    public static Api.V0.V0.V0RequestBuilder V0 => Clients.V0Client.V0;
    public static HttpClient HttpClient => Clients.HttpClient;

    public static string? CurrentUsername { get; private set; }
    public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(CurrentUsername);
    public static Task<UserViewModel?> RefreshAuthState() => GetViewModelAsync<UserViewModel>();

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
    /// <param name="username">用户名 (如果需求)</param>
    /// <returns>ViewModel</returns>
    public static async Task<T?> GetViewModelAsync<T>(object? id = null, string? username = null) where T : ViewModelBase
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
            if (IsAuthenticated)
            {
                UserEpisodeCollection? episode = null;
                try { episode = await V0.Users.Minus.Collections.Minus.Episodes[episodeId].GetAsync(); }
                catch (Exception e) { Trace.TraceError(e.Message); }

                if (episode is null) return null;
                else return EpisodeViewModel.InitFormCollection(episode) as T;
            }
            else
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

            CurrentUsername = me?.Username;

            if (me is null) return null;
            else return new UserViewModel(me) as T;
        }
        else if (typeof(T) == typeof(SubjectCollectionViewModel) && id is int subjectId1)
        {
            UserSubjectCollection? subjectCollection = null;
            try { subjectCollection = await V0.Users[username ?? CurrentUsername].Collections[subjectId1].GetAsync(); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (subjectCollection is null) return null;
            else return new SubjectCollectionViewModel(subjectCollection) as T;
        }
        else if (typeof(T) == typeof(CalendarViewModel))
        {
            List<Calendar>? calendars = null;
            try { calendars = await Clients.LegacyClient.Calendar.GetAsync(); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (calendars is null) return null;
            var day = (id as DayOfWeek?) ?? DateTime.Today.DayOfWeek;
            return new CalendarViewModel(calendars.Where(x => Common.ParseDayOfWeek(x.Weekday?.Id) == day).First()) as T;
        }
        else if (typeof(T) == typeof(AiringViewModel))
        {
            List<Calendar>? calendars = null;
            try { calendars = await Clients.LegacyClient.Calendar.GetAsync(); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (calendars is null) return null;
            else return new AiringViewModel(calendars) as T;
        }

        else return null;
    }

    public static async Task<bool> GetIsCollected(ItemType type, int? id)
    {
        if (id is not int i || string.IsNullOrWhiteSpace(CurrentUsername)) return false;

        var uc = V0.Users[CurrentUsername].Collections;
        try
        {
            object? r = type switch
            {
                ItemType.Subject => await uc[i].GetAsync(),
                ItemType.Character => await uc.Minus.Characters[i].GetAsync(),
                ItemType.Person => await uc.Minus.Persons[i].GetAsync(),
                _ => throw new NotImplementedException(),
            };

            if (r != null) return true;
        }
        catch (Exception e) { Trace.TraceError(e.Message); }

        return false;
    }

    public static async Task<int> UpdateEpisodeCollection(int id, EpisodeCollectionType type)
    {
        try
        {
            await V0.Users.Minus.Collections.Minus.Episodes[id].PutAsync(new() { Type = (int)type });
            return 204;
        }
        catch (ErrorDetail e)
        {
            Trace.TraceError(e.Message);
            return e.ResponseStatusCode;
        }
    }
}

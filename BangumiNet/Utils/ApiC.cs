using Avalonia.Media.Imaging;
using Avalonia.Platform;
using BangumiNet.Api;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace BangumiNet.Utils;

public static partial class ApiC
{
    public static Clients Clients { get; private set; } = ClientBuilder.Build(SettingProvider.CurrentSettings);
    public static Api.P1.P1.P1RequestBuilder P1 => Clients.P1Client.P1;
    public static Api.V0.V0.V0RequestBuilder V0 => Clients.V0Client.V0;
    public static HttpClient HttpClient => Clients.HttpClient;

    public static string? CurrentUsername { get; private set; }
    public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(CurrentUsername);
    public static Task<UserViewModel?> RefreshAuthState() => GetViewModelAsync<UserViewModel>();

    [GeneratedRegex(@"^https?://lain\.bgm\.tv(/r/[0-9]+)?/pic/user/[A-Za-z]/icon\.jpg$")]
    private static partial Regex DefaultUserAvatarUrl();
    [GeneratedRegex(@"^https?://lain\.bgm\.tv/pic/photo/[A-Za-z]/no_photo\.png$")]
    private static partial Regex NoPhotoUrl();
    public static Bitmap DefaultUserAvatar { get; } = new(AssetLoader.Open(CommonUtils.GetAssetUri("DefaultAvatar.png")));
    public static Bitmap FallbackImage { get; } = new(AssetLoader.Open(CommonUtils.GetAssetUri("FallbackImage.png")));
    public static Bitmap InternetErrorFallbackImage { get; } = new(AssetLoader.Open(CommonUtils.GetAssetUri("InternetError.png")));
    public static bool IsShared(this Bitmap bitmap)
        => ReferenceEquals(bitmap, DefaultUserAvatar) ||
           ReferenceEquals(bitmap, FallbackImage) ||
           ReferenceEquals(bitmap, InternetErrorFallbackImage);

    private static readonly SemaphoreSlim semaphore = new(128);
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> urlLocks = new();
    public static async Task<Bitmap?> GetImageAsync(string? url, bool useCache = true, bool fallback = false, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(url)) return null;
        if (DefaultUserAvatarUrl().IsMatch(url))
            return DefaultUserAvatar;
        if (NoPhotoUrl().IsMatch(url))
            return FallbackImage;

        var urlLock = urlLocks.GetOrAdd(url, _ => new SemaphoreSlim(1, 1));
        await urlLock.WaitAsync(cancellationToken);
        await semaphore.WaitAsync(cancellationToken);
        try
        {
            useCache = useCache && SettingProvider.CurrentSettings.IsDiskCacheEnabled;

            if (useCache)
            {
                await using var cacheStream = CacheProvider.ReadCache(url);
                if (cacheStream is not null)
                {
                    var cacheBitmap = new Bitmap(cacheStream);
                    return cacheBitmap;
                }
            }

            await using var response = await HttpClient.GetStreamAsync(url, cancellationToken: cancellationToken);
            await using var responseStream = await response.Clone(cancellationToken: cancellationToken);
            if (useCache) await CacheProvider.WriteCache(url, responseStream);
            var responseBitmap = new Bitmap(responseStream);
            return responseBitmap;
        }
        catch (Exception e)
        {
            Trace.TraceError($"Image Loading Failed: {url}");
            Trace.TraceError(e.Message);
            CacheProvider.DeleteCache(url);
            if (fallback) return InternetErrorFallbackImage;
            else return null;
        }
        finally
        {
            semaphore.Release();
            urlLock.Release();
        }
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
    public static async Task<T?> GetViewModelAsync<T>(object? id = null, string? username = null, CancellationToken cancellationToken = default) where T : ViewModelBase
    {
        if (typeof(T) == typeof(SubjectViewModel) && id is int subjectId)
        {
            Api.P1.Models.Subject? subject = null;
            try { subject = await P1.Subjects[subjectId].GetAsync(cancellationToken: cancellationToken); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (subject is null) return null;
            else return new SubjectViewModel(subject) as T;
        }
        else if (typeof(T) == typeof(EpisodeViewModel) && id is int episodeId)
        {
            Api.P1.Models.Episode? episode = null;
            try { episode = await P1.Episodes[episodeId].GetAsync(cancellationToken: cancellationToken); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (episode is null) return null;
            else return new EpisodeViewModel(episode) as T;
        }
        else if (typeof(T) == typeof(CharacterViewModel) && id is int characterId)
        {
            Api.P1.Models.Character? character = null;
            try { character = await P1.Characters[characterId].GetAsync(cancellationToken: cancellationToken); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (character is null) return null;
            else return new CharacterViewModel(character) as T;
        }
        else if (typeof(T) == typeof(PersonViewModel) && id is int personId)
        {
            Api.P1.Models.Person? person = null;
            try { person = await P1.Persons[personId].GetAsync(cancellationToken: cancellationToken); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (person is null) return null;
            else return new PersonViewModel(person) as T;
        }
        else if (typeof(T) == typeof(BlogViewModel) && id is int bid)
        {
            Api.P1.Models.BlogEntry? blog = null;
            try { blog = await P1.Blogs[bid].GetAsync(cancellationToken: cancellationToken); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (blog is null) return null;
            else return new BlogViewModel(blog) as T;
        }
        else if (typeof(T) == typeof(UserViewModel) && username is string uid)
        {
            Api.P1.Models.User? user = null;
            try { user = await P1.Users[uid].GetAsync(cancellationToken: cancellationToken); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (user is null) return null;
            else return new UserViewModel(user) as T;
        }
        else if (typeof(T) == typeof(MeViewModel))
        {
            Api.P1.Models.Profile? me = null;
            try { me = await P1.Me.GetAsync(cancellationToken: cancellationToken); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            CurrentUsername = me?.Username;

            if (me is null) return null;
            else return new MeViewModel(me) as T;
        }
        else if (typeof(T) == typeof(GroupViewModel) && username is string gid)
        {
            Api.P1.Models.Group? group = null;
            try { group = await P1.Groups[gid].GetAsync(cancellationToken: cancellationToken); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (group is null) return null;
            else return new GroupViewModel(group) as T;
        }
        else if (typeof(T) == typeof(SubjectCollectionViewModel) && id is int subjectId1)
        {
            var user = username ?? CurrentUsername;
            if (user is null) return null;
            UserSubjectCollection? subjectCollection = null;
            try { subjectCollection = await V0.Users[user].Collections[subjectId1].GetAsync(cancellationToken: cancellationToken); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (subjectCollection is null) return null;
            else return new SubjectCollectionViewModel(subjectCollection) { IsMy = username == null } as T;
        }
        else if (typeof(T) == typeof(CalendarViewModel))
        {
            Api.P1.Models.Calendar? calendars = null;
            try { calendars = await P1.Calendar.GetAsync(cancellationToken: cancellationToken); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (calendars is null) return null;
            var day = (id as DayOfWeek?) ?? DateTime.Today.DayOfWeek;
            return new CalendarViewModel(calendars.Days.First(x => x.Key == day)) as T;
        }
        else if (typeof(T) == typeof(AiringViewModel))
        {
            Api.P1.Models.Calendar? calendars = null;
            try { calendars = await P1.Calendar.GetAsync(cancellationToken: cancellationToken); }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (calendars is null) return null;
            else return new AiringViewModel(calendars) as T;
        }
        else if (typeof(T) == typeof(RevisionViewModel) && id is RevisionViewModel rvm)
        {
            IRevision? revision = null;
            try
            {
                if (rvm.Id is not int revisionId)
                    throw new ArgumentException($"非法 RevisionId {rvm.Id}");
                revision = rvm.Parent?.ItemType switch
                {
                    ItemType.Subject => revision = await V0.Revisions.Subjects[revisionId].GetAsync(cancellationToken: cancellationToken),
                    ItemType.Episode => revision = await V0.Revisions.Episodes[revisionId].GetAsync(cancellationToken: cancellationToken),
                    ItemType.Character => revision = await V0.Revisions.Characters[revisionId].GetAsync(cancellationToken: cancellationToken),
                    ItemType.Person => revision = await V0.Revisions.Persons[revisionId].GetAsync(cancellationToken: cancellationToken),
                    _ => throw new ArgumentException($"非法 ItemType {rvm.Parent?.ItemType}"),
                };
            }
            catch (Exception e) { Trace.TraceError(e.Message); }

            if (revision is null) return null;
            else return new RevisionViewModel(revision, rvm.Parent) as T;
        }
        else
        {
            Trace.TraceWarning($"GetViewModelAsync Invalid Request: Type/{typeof(T).FullName} ID/{id} Username/{username}");
            return null;
        }
    }

    /// <summary>
    /// 获取当前用户是否收藏项目
    /// </summary>
    /// <param name="type">Subject/Character/Person</param>
    /// <param name="id">项目 ID</param>
    /// <returns>收藏时间，未收藏或出错时为 null</returns>
    public static async Task<DateTimeOffset?> GetIsCollected(ItemType type, int? id, CancellationToken cancellationToken = default)
    {
        if (id is not int i || string.IsNullOrWhiteSpace(CurrentUsername)) return null;

        var uc = V0.Users[CurrentUsername].Collections;
        try
        {
            return type switch
            {
                ItemType.Subject => (await uc[i].GetAsync(cancellationToken: cancellationToken))?.UpdatedAt,
                ItemType.Character => (await uc.Minus.Characters[i].GetAsync(cancellationToken: cancellationToken))?.CreatedAt,
                ItemType.Person => (await uc.Minus.Persons[i].GetAsync(cancellationToken: cancellationToken))?.CreatedAt,
                _ => throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(ItemType)),
            };
        }
        catch { } // (Exception e) { Trace.TraceError(e.Message); }

        return null;
    }

    public static async Task<int> UpdateCollection(ItemType type, int id, bool target, CancellationToken cancellationToken = default)
    {
        try
        {
            Task task = type switch
            {
                ItemType.Character => target switch
                {
                    true => V0.Characters[id].Collect.PostAsync(cancellationToken: cancellationToken),
                    false => V0.Characters[id].Collect.DeleteAsync(cancellationToken: cancellationToken),
                },
                ItemType.Person => target switch
                {
                    true => V0.Persons[id].Collect.PostAsync(cancellationToken: cancellationToken),
                    false => V0.Persons[id].Collect.DeleteAsync(cancellationToken: cancellationToken),
                },
                _ => throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(ItemType)),
            };
            await task;
            return 204;
        }
        catch (ErrorDetail e)
        {
            Trace.TraceError(e.Message);
            return e.ResponseStatusCode;
        }
    }

    public static async Task<int> UpdateEpisodeCollection(int id, EpisodeCollectionType type, CancellationToken cancellationToken = default)
    {
        try
        {
            await V0.Users.Minus.Collections.Minus.Episodes[id].PutAsync(new() { Type = (int)type }, cancellationToken: cancellationToken);
            return 204;
        }
        catch (ErrorDetail e)
        {
            Trace.TraceError(e.Message);
            return e.ResponseStatusCode;
        }
    }
    public static async Task<int> UpdateEpisodeCollection(int subjectId, IEnumerable<int> eps, EpisodeCollectionType type, CancellationToken cancellationToken = default)
    {
        try
        {
            await V0.Users.Minus.Collections[subjectId].Episodes.PatchAsync(new() { EpisodeId = [.. eps], Type = (int)type }, cancellationToken: cancellationToken);
            return 204;
        }
        catch (ErrorDetail e)
        {
            Trace.TraceError(e.Message);
            return e.ResponseStatusCode;
        }
    }

    public static async Task<TopicViewModel?> GetTopicViewModelAsync(ItemType parentType, int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return parentType switch
            {
                ItemType.Subject => await P1.Subjects.Minus.Topics[id].GetAsync(cancellationToken: cancellationToken) is { } subRes ? new(subRes, true) : null,
                ItemType.Group => await P1.Groups.Minus.Topics[id].GetAsync(cancellationToken: cancellationToken) is { } groRes ? new(groRes, true) : null,
                _ => throw new NotImplementedException(),
            };
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Message);
            return null;
        }
    }
}

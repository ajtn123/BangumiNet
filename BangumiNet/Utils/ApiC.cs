using Avalonia.Media.Imaging;
using BangumiNet.Api;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace BangumiNet.Utils;

public static class ApiC
{
    public static Clients Clients { get; private set; } = ClientBuilder.Build(SettingProvider.Current);
    public static Api.P1.P1.P1RequestBuilder P1 => Clients.P1Client.P1;
    public static Api.V0.V0.V0RequestBuilder V0 => Clients.V0Client.V0;
    public static HttpClient HttpClient => Clients.HttpClient;
    public static CacheProvider ImageCache { get; private set; } = new("Images", SettingProvider.Current.DiskCacheSizeLimit);

    public static string? CurrentUsername { get; private set; }
    public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(CurrentUsername);
    public static Task<UserViewModel?> RefreshAuthState() => GetViewModelAsync<UserViewModel>();

    private static readonly SemaphoreSlim semaphore = new(32);
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> urlLocks = new();
    public static async Task<Bitmap?> GetImageAsync(string url, bool useCache = true, CacheProvider? cache = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(url))
            return null;

        cache ??= ImageCache;
        useCache = useCache && SettingProvider.Current.IsDiskCacheEnabled;

        var urlLock = urlLocks.GetOrAdd(url, _ => new SemaphoreSlim(1, 1));
        await urlLock.WaitAsync(ct);
        await semaphore.WaitAsync(ct);
        try
        {
            if (useCache)
            {
                var cacheFile = cache.Get(url);
                if (cacheFile is not null)
                    return new Bitmap(cacheFile);
            }

            await using var response = await HttpClient.GetStreamAsync(url, cancellationToken: ct);
            await using var responseStream = await response.Clone(cancellationToken: ct);
            if (useCache) await cache.Write(url, responseStream);
            return new Bitmap(responseStream);
        }
        catch (Exception e)
        {
            Trace.TraceError($"Failed to load image: {url}");
            Trace.TraceError(e.ToString());
            cache.Delete(url);
            return null;
        }
        finally
        {
            semaphore.Release();
            urlLock.Release();
        }
    }

    public static void RebuildClients()
        => Clients = ClientBuilder.Build(SettingProvider.Current);

    /// <summary>
    /// 发起请求并转换为相应的 ViewModel 类型
    /// </summary>
    /// <typeparam name="T">ViewModel 的类型</typeparam>
    /// <param name="id">ID (如果需求)</param>
    /// <param name="name">用户名 (如果需求)</param>
    /// <returns>ViewModel</returns>
    public static async Task<T?> GetViewModelAsync<T>(object? id = null, string? name = null, CancellationToken ct = default) where T : ViewModelBase
    {
        try
        {
            static void Throw() => throw new Exception("Invalid API Response");

            if (typeof(T) == typeof(SubjectViewModel) && id is int subjectId)
            {
                if (await P1.Subjects[subjectId].GetAsync(cancellationToken: ct) is not { } response)
                    Throw();

                return new SubjectViewModel(response) as T;
            }
            else if (typeof(T) == typeof(EpisodeViewModel) && id is int episodeId)
            {
                if (await P1.Episodes[episodeId].GetAsync(cancellationToken: ct) is not { } response)
                    Throw();

                return new EpisodeViewModel(response) as T;
            }
            else if (typeof(T) == typeof(CharacterViewModel) && id is int characterId)
            {
                if (await P1.Characters[characterId].GetAsync(cancellationToken: ct) is not { } response)
                    Throw();

                return new CharacterViewModel(response) as T;
            }
            else if (typeof(T) == typeof(PersonViewModel) && id is int personId)
            {
                if (await P1.Persons[personId].GetAsync(cancellationToken: ct) is not { } response)
                    Throw();

                return new PersonViewModel(response) as T;
            }
            else if (typeof(T) == typeof(BlogViewModel) && id is int blogId)
            {
                if (await P1.Blogs[blogId].GetAsync(cancellationToken: ct) is not { } response)
                    Throw();

                return new BlogViewModel(response) as T;
            }
            else if (typeof(T) == typeof(IndexViewModel) && id is int indexId)
            {
                if (await P1.Indexes[indexId].GetAsync(cancellationToken: ct) is not { } response)
                    Throw();

                return new IndexViewModel(response) as T;
            }
            else if (typeof(T) == typeof(UserViewModel) && name is string username)
            {
                if (await P1.Users[username].GetAsync(cancellationToken: ct) is not { } response)
                    Throw();

                return new UserViewModel(response) as T;
            }
            else if (typeof(T) == typeof(MeViewModel))
            {
                if (await P1.Me.GetAsync(cancellationToken: ct) is not { } response)
                    Throw();

                CurrentUsername = response.Username;
                return new MeViewModel(response) as T;
            }
            else if (typeof(T) == typeof(GroupViewModel) && name is string groupname)
            {
                if (await P1.Groups[groupname].GetAsync(cancellationToken: ct) is not { } response)
                    Throw();

                return new GroupViewModel(response) as T;
            }
            else if (typeof(T) == typeof(SubjectCollectionViewModel) && id is int subjectId1 && (name ?? CurrentUsername) is string username1)
            {
                if (await V0.Users[username1].Collections[subjectId1].GetAsync(cancellationToken: ct) is not { } response)
                    Throw();

                return new SubjectCollectionViewModel(response) { IsMy = username1 == CurrentUsername } as T;
            }
            else if (typeof(T) == typeof(CalendarViewModel))
            {
                if (await P1.Calendar.GetAsync(cancellationToken: ct) is not { } response)
                    Throw();

                var day = id as DayOfWeek? ?? DateTime.Today.DayOfWeek;
                return new CalendarViewModel(response.Days.First(x => x.Key == day)) as T;
            }
            else if (typeof(T) == typeof(RevisionViewModel) && id is RevisionViewModel
            {
                Id: { } revisionId,
                Parent:
                {
                    ItemType: ItemType.Subject or ItemType.Episode or ItemType.Character or ItemType.Person,
                    ItemType: { } revisionType,
                } revisionParent,
            })
            {
                if ((IRevision?)(revisionType switch
                {
                    ItemType.Subject => await V0.Revisions.Subjects[revisionId].GetAsync(cancellationToken: ct),
                    ItemType.Episode => await V0.Revisions.Episodes[revisionId].GetAsync(cancellationToken: ct),
                    ItemType.Character => await V0.Revisions.Characters[revisionId].GetAsync(cancellationToken: ct),
                    ItemType.Person => await V0.Revisions.Persons[revisionId].GetAsync(cancellationToken: ct),
                    _ => null,
                }) is not IRevision response) Throw();

                return new RevisionViewModel(response, revisionParent) as T;
            }
            else
            {
                Trace.TraceError($"Invalid Request: Type/{typeof(T).FullName} ID/{id} Name/{name}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Trace.TraceError($"Request Failed: Type/{typeof(T).FullName} ID/{id} Name/{name}");
            Trace.TraceError(ex.ToString());
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
        catch { } // (Exception e) { Trace.TraceError(e.ToString()); }

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
            Trace.TraceError(e.ToString());
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
            Trace.TraceError(e.ToString());
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
            Trace.TraceError(e.ToString());
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
            Trace.TraceError(e.ToString());
            return null;
        }
    }
}

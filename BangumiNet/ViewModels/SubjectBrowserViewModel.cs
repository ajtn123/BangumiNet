using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Helpers;
using BangumiNet.Common;
using System.Reactive;
using QueryParameters = BangumiNet.Api.V0.V0.Subjects.SubjectsRequestBuilder.SubjectsRequestBuilderGetQueryParameters;

namespace BangumiNet.ViewModels;

public partial class SubjectBrowserViewModel : SubjectListPagedViewModel
{
    public SubjectBrowserViewModel()
    {
        Title = $"浏览项目 - {Constants.ApplicationName}";
        Type = SubjectType.Anime;

        this.WhenAnyValue(x => x.Type).Subscribe(x =>
        {
            this.RaisePropertyChanged(nameof(IsBrowsingAnime));
            this.RaisePropertyChanged(nameof(IsBrowsingBook));
            this.RaisePropertyChanged(nameof(IsBrowsingGame));
            this.RaisePropertyChanged(nameof(IsBrowsingReal));
            this.RaisePropertyChanged(nameof(IsBrowsingMusic));
        });

        BrowseCommand = ReactiveCommand.CreateFromTask(ct => LoadPageAsync(-1, ct));
    }

    protected override async Task LoadPageAsync(int? page, CancellationToken cancellationToken = default)
    {
        if (page is not int p) return;
        var offset = (p - 1) * Limit;

        try
        {
            if (p != -1 && QueryParameters != null)
                await BrowsePageAsync(offset, QueryParameters, cancellationToken);
            else
                QueryParameters = await BrowseAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Message);
            QueryParameters = null;
        }
    }

    private async Task<QueryParameters?> BrowseAsync(CancellationToken cancellationToken)
    {
        QueryParameters? queryParameters = null;
        var response = await ApiC.V0.Subjects.GetAsync(config =>
        {
            config.QueryParameters.Type = (int)Type;
            config.QueryParameters.Cat = GetCategory();
            config.QueryParameters.Limit = Limit;
            config.QueryParameters.Offset = 0;
            config.QueryParameters.Year = Year?.Year;
            config.QueryParameters.Month = Month?.Month;
            config.QueryParameters.Series = GetIsSeries();
            config.QueryParameters.Platform = GetPlatform();
            config.QueryParameters.Sort = GetSort();
            queryParameters = config.QueryParameters;
        }, cancellationToken);
        if (response == null) return null;

        UpdateItems(response);
        PageNavigator.UpdatePageInfo(response);
        return queryParameters;
    }
    private async Task BrowsePageAsync(int offset, QueryParameters queryParameters, CancellationToken cancellationToken)
    {
        var response = await ApiC.V0.Subjects.GetAsync(config =>
        {
            config.QueryParameters = queryParameters;
            config.Paging(Limit, offset);
        }, cancellationToken);
        if (response == null) return;

        UpdateItems(response);
        PageNavigator.UpdatePageInfo(response);
    }


    [Reactive] public partial SubjectType Type { get; set; }
    [Reactive] public partial AnimeType? AnimeCategory { get; set; }
    [Reactive] public partial BookType? BookCategory { get; set; }
    [Reactive] public partial GameType? GameCategory { get; set; }
    [Reactive] public partial RealType? RealCategory { get; set; }
    [Reactive] public partial string? Platform { get; set; }
    [Reactive] public partial SubjectBrowserSort? Sort { get; set; }
    [Reactive] public partial bool? IsSeries { get; set; }
    [Reactive] public partial bool IsYearEnabled { get; set; }
    [Reactive] public partial bool IsMonthEnabled { get; set; }
    [Reactive] public partial DateTimeOffset? Year { get; set; }
    [Reactive] public partial DateTimeOffset? Month { get; set; }
    [Reactive] public partial string? ErrorMessage { get; set; }
    [Reactive] public partial QueryParameters? QueryParameters { get; set; }

    [Reactive] public partial ReactiveCommand<Unit, Unit> BrowseCommand { get; set; }

    public bool IsBrowsingAnime => Type == SubjectType.Anime;
    public bool IsBrowsingBook => Type == SubjectType.Book;
    public bool IsBrowsingGame => Type == SubjectType.Game;
    public bool IsBrowsingReal => Type == SubjectType.Real;
    public bool IsBrowsingMusic => Type == SubjectType.Music;

    public override int Limit => SettingProvider.Current.SubjectBrowserPageSize;

    public int? GetCategory()
        => Type switch
        {
            SubjectType.Book => (int?)BookCategory,
            SubjectType.Anime => (int?)AnimeCategory,
            SubjectType.Music => null,
            SubjectType.Game => (int?)GameCategory,
            SubjectType.Real => (int?)RealCategory,
            _ => throw new NotImplementedException(),
        };
    public bool? GetIsSeries()
        => IsBrowsingBook ? IsSeries : null;
    public string? GetPlatform()
        => IsBrowsingGame && !string.IsNullOrWhiteSpace(Platform) ? Platform : null;
    public string? GetSort()
        => Sort?.GetValue();
}

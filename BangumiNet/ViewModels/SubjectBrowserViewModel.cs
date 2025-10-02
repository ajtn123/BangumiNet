using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.V0.V0.Subjects;
using System.Reactive;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class SubjectBrowserViewModel : ViewModelBase
{
    public SubjectBrowserViewModel()
    {
        Type = SubjectType.Anime;
        SubjectListViewModel = new SubjectListViewModel();
        PageNavigatorViewModel = new PageNavigatorViewModel();

        this.WhenAnyValue(x => x.Type).Subscribe(x =>
        {
            this.RaisePropertyChanged(nameof(IsBrowsingAnime));
            this.RaisePropertyChanged(nameof(IsBrowsingBook));
            this.RaisePropertyChanged(nameof(IsBrowsingGame));
            this.RaisePropertyChanged(nameof(IsBrowsingReal));
            this.RaisePropertyChanged(nameof(IsBrowsingMusic));
        });

        this.WhenAnyValue(x => x.ResultOffset, x => x.TotalResults)
            .Subscribe(x => ResultOffsetMessage = x.Item1 == null || x.Item2 == null ? null : $"正在显示第 {x.Item1 + 1}-{Math.Min((int)x.Item1 + Limit, (int)x.Item2)} 条。");

        BrowseCommand = ReactiveCommand.CreateFromTask(BrowseAsync);
        BrowsePageCommand = ReactiveCommand.CreateFromTask<int?>(BrowsePageAsync);

        PageNavigatorViewModel.PrevPage.InvokeCommand(BrowsePageCommand);
        PageNavigatorViewModel.NextPage.InvokeCommand(BrowsePageCommand);
        PageNavigatorViewModel.JumpPage.InvokeCommand(BrowsePageCommand);
    }

    public async Task BrowseAsync()
    {
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
            QueryParameters = config.QueryParameters;
        });
        if (response == null) { QueryParameters = null; return; }
        SubjectListViewModel.UpdateItems(response);
        PageNavigatorViewModel.PageIndex = 1;
        TotalResults = response.Total;
        ResultOffset = response.Offset;
        if (response.Total != null)
            PageNavigatorViewModel.Total = (int)Math.Ceiling((double)response.Total / Limit);
        else PageNavigatorViewModel.Total = null;
    }
    public async Task BrowsePageAsync(int? i)
    {
        if (QueryParameters == null) return;
        if (i is not int pageIndex || !PageNavigatorViewModel.IsInRange(i)) return;
        var response = await ApiC.V0.Subjects.GetAsync(config =>
        {
            config.QueryParameters = QueryParameters;
            config.QueryParameters.Offset = (pageIndex - 1) * Limit;
        });
        if (response == null) { QueryParameters = null; return; }
        SubjectListViewModel.UpdateItems(response);
        PageNavigatorViewModel.PageIndex = pageIndex;
        TotalResults = response.Total;
        ResultOffset = response.Offset;
        if (response.Total != null)
            PageNavigatorViewModel.Total = (int)Math.Ceiling((double)response.Total / Limit);
        else PageNavigatorViewModel.Total = null;
    }


    [Reactive] public partial SubjectType Type { get; set; }
    [Reactive] public partial SubjectCategory.Anime? AnimeCategory { get; set; }
    [Reactive] public partial SubjectCategory.Book? BookCategory { get; set; }
    [Reactive] public partial SubjectCategory.Game? GameCategory { get; set; }
    [Reactive] public partial SubjectCategory.Real? RealCategory { get; set; }
    [Reactive] public partial string? Platform { get; set; }
    [Reactive] public partial SubjectBrowserSort? Sort { get; set; }
    [Reactive] public partial bool? IsSeries { get; set; }
    [Reactive] public partial bool IsYearEnabled { get; set; }
    [Reactive] public partial bool IsMonthEnabled { get; set; }
    [Reactive] public partial DateTimeOffset? Year { get; set; }
    [Reactive] public partial DateTimeOffset? Month { get; set; }
    [Reactive] public partial int? TotalResults { get; set; }
    [Reactive] public partial int? ResultOffset { get; set; }
    [Reactive] public partial PageNavigatorViewModel PageNavigatorViewModel { get; set; }
    [Reactive] public partial string? ErrorMessage { get; set; }
    [Reactive] public partial string? ResultOffsetMessage { get; set; }
    [Reactive] public partial SubjectListViewModel SubjectListViewModel { get; set; }
    [Reactive] public partial SubjectsRequestBuilder.SubjectsRequestBuilderGetQueryParameters? QueryParameters { get; set; }

    public ICommand BrowseCommand { get; set; }
    public ReactiveCommand<int?, Unit> BrowsePageCommand { get; }

    public bool IsBrowsingAnime => Type == SubjectType.Anime;
    public bool IsBrowsingBook => Type == SubjectType.Book;
    public bool IsBrowsingGame => Type == SubjectType.Game;
    public bool IsBrowsingReal => Type == SubjectType.Real;
    public bool IsBrowsingMusic => Type == SubjectType.Music;

    public static int Limit => SettingProvider.CurrentSettings.SubjectBrowserPageSize;

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
        => Sort?.ToString()?.ToLower();
}

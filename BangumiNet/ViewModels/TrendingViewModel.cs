using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Trending.Subjects;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class TrendingViewModel : SubjectListPagedViewModel
{
    public TrendingViewModel(ItemType itemType, SubjectType subjectType)
    {
        ItemType = itemType;
        SubjectType = subjectType;

        LoadCommand = ReactiveCommand.CreateFromTask<int?>(Load);
        PageNavigator.JumpPage.InvokeCommand(LoadCommand);
        PageNavigator.NextPage.InvokeCommand(LoadCommand);
        PageNavigator.PrevPage.InvokeCommand(LoadCommand);

        this.WhenAnyValue(x => x.ItemType).Subscribe(itemType =>
        {
            this.RaisePropertyChanged(nameof(IsSubject));
        });
        this.WhenAnyValue(x => x.SubjectType).Subscribe(itemType =>
        {
            if (ItemType != ItemType.Subject) return;
            _ = Load(1);
        });
    }
    public async Task Load(int? p, CancellationToken ct = default)
    {
        if (p is not int page) return;
        SubjectsGetResponse? response = null;
        var offset = (page - 1) * Limit;
        try
        {
            response = await ApiC.P1.Trending.Subjects.GetAsSubjectsGetResponseAsync(config =>
            {
                config.QueryParameters.Limit = Limit;
                config.QueryParameters.Offset = offset;
                config.QueryParameters.Type = (int)SubjectType;
            }, cancellationToken: ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); return; }
        if (response == null) return;
        SubjectViewModels = response.Data?
            .Where(s => s.Subject != null)
            .Select<TrendingSubject, ViewModelBase>(s => new SubjectViewModel(s.Subject!) { Hype = s.Count })
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }

    [Reactive] public partial ItemType ItemType { get; set; }
    [Reactive] public partial SubjectType SubjectType { get; set; }

    public ICommand LoadCommand { get; set; }

    public bool IsSubject => ItemType == ItemType.Subject;

    public const int Limit = 20;
}

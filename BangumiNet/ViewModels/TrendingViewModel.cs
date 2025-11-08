using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Trending.Subjects;
using BangumiNet.Api.P1.P1.Trending.Subjects.Topics;
using System.Reactive.Linq;
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

        this.WhenAnyValue(x => x.ItemType).Skip(1).Subscribe(itemType =>
        {
            this.RaisePropertyChanged(nameof(IsSubject));
            _ = Load(1);
        });
        this.WhenAnyValue(x => x.SubjectType).Skip(1).Subscribe(itemType =>
        {
            if (!IsSubject) return;
            _ = Load(1);
        });
    }
    public async Task Load(int? p, CancellationToken ct = default)
    {
        if (p is not int page) return;
        var offset = (page - 1) * Limit;
        if (ItemType == ItemType.Subject)
        {
            SubjectsGetResponse? response = null;
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
        else if (ItemType == ItemType.Topic)
        {
            TopicsGetResponse? response = null;
            try
            {
                response = await ApiC.P1.Trending.Subjects.Topics.GetAsTopicsGetResponseAsync(config =>
                {
                    config.QueryParameters.Limit = Limit;
                    config.QueryParameters.Offset = offset;
                }, cancellationToken: ct);
            }
            catch (Exception e) { Trace.TraceError(e.Message); return; }
            if (response == null) return;
            SubjectViewModels = response.Data?
                .Select<SubjectTopic, ViewModelBase>(s => new TopicViewModel(s, false))
                .ToObservableCollection();
            PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
        }
        else throw new NotImplementedException();
    }

    [Reactive] public partial ItemType ItemType { get; set; }
    [Reactive] public partial SubjectType SubjectType { get; set; }

    public ICommand LoadCommand { get; set; }

    public bool IsSubject => ItemType == ItemType.Subject;

    public const int Limit = 20;
}

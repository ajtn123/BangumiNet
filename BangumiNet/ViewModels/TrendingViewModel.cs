using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Trending.Subjects;
using BangumiNet.Api.P1.P1.Trending.Subjects.Topics;
using BangumiNet.Common;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class TrendingViewModel : SubjectListPagedViewModel
{
    public TrendingViewModel(ItemType itemType, SubjectType subjectType)
    {
        ItemType = itemType;
        SubjectType = subjectType;

        this.WhenAnyValue(x => x.ItemType).Skip(1).Subscribe(itemType =>
        {
            this.RaisePropertyChanged(nameof(IsSubject));
            _ = LoadPageCommand.Execute(1).Subscribe();
        });
        this.WhenAnyValue(x => x.SubjectType).Skip(1).Subscribe(itemType =>
        {
            if (!IsSubject) return;
            _ = LoadPageCommand.Execute(1).Subscribe();
        });
    }

    protected override async Task LoadPageAsync(int? page, CancellationToken ct = default)
    {
        if (page is not int p) return;
        var offset = (p - 1) * Limit;

        if (ItemType == ItemType.Subject)
        {
            SubjectsGetResponse? response = null;
            try
            {
                response = await ApiC.P1.Trending.Subjects.GetAsync(config =>
                {
                    config.QueryParameters.Limit = Limit;
                    config.QueryParameters.Offset = offset;
                    config.QueryParameters.Type = (int)SubjectType;
                }, cancellationToken: ct);
            }
            catch (Exception e) { Trace.TraceError(e.ToString()); }
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
                response = await ApiC.P1.Trending.Subjects.Topics.GetAsync(config =>
                {
                    config.QueryParameters.Limit = Limit;
                    config.QueryParameters.Offset = offset;
                }, cancellationToken: ct);
            }
            catch (Exception e) { Trace.TraceError(e.ToString()); }
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

    public bool IsSubject => ItemType == ItemType.Subject;
    public override int Limit => 20;
}

using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Converters;

namespace BangumiNet.ViewModels;

public partial class TimelineItemViewModel : ViewModelBase
{
    public TimelineItemViewModel(Timeline timeline)
    {
        Source = timeline;
        Id = timeline.Id;
        Uid = timeline.Uid;
        Batch = timeline.Batch ?? false;
        Category = (TimelineCategory?)timeline.Cat;
        Replies = timeline.Replies;
        Type = timeline.Type;
        OperationSource = timeline.Source?.Name;
        OperationSourceUrl = timeline.Source?.Url;
        IsMy = ApiC.CurrentUsername != null && timeline.User?.Username == ApiC.CurrentUsername;
        if (OperationSource == "web" && string.IsNullOrWhiteSpace(OperationSourceUrl))
            OperationSourceUrl = CurrentSettings.BangumiTvUrlBase;
        if (timeline.User != null)
            User = new(timeline.User);
        if (timeline.CreatedAt is int ct)
            CreationTime = DateTimeOffset.FromUnixTimeSeconds(ct);
        if (timeline.Memo != null)
        {
            List<ViewModelBase> subjects = [];
            //if (timeline.Memo.Blog != null)
            //    Memo.SubjectViewModels.Add(new BlogViewModel(timeline.Memo.Blog));
            //if (timeline.Memo.Daily != null)
            //    Memo.SubjectViewModels.Add(new DailyViewModel(timeline.Memo.Daily));
            //if (timeline.Memo.Index != null)
            //    Memo.SubjectViewModels.Add(new IndexViewModel(timeline.Memo.Index));
            //if (timeline.Memo.Mono != null)
            //    Memo.SubjectViewModels.Add(new MonoViewModel(timeline.Memo.Mono));
            if (timeline.Memo.Progress?.Single?.Subject != null)
            {
                subjects.Add(new SubjectViewModel(timeline.Memo.Progress.Single.Subject));
                subjects.Add(new TextViewModel($"已完成第 {timeline.Memo.Progress.Single.Episode?.Sort} 集 {NameCnCvt.Convert(timeline.Memo.Progress.Single.Episode)}"));
            }
            if (timeline.Memo.Progress?.Batch?.Subject != null)
            {
                subjects.Add(new SubjectViewModel(timeline.Memo.Progress.Batch.Subject));
                subjects.Add(new TextViewModel($"已完成 {timeline.Memo.Progress.Batch.EpsUpdate} / {timeline.Memo.Progress.Batch.EpsTotal} 集"));
                subjects.Add(new TextViewModel($"已完成 {timeline.Memo.Progress.Batch.VolsUpdate} / {timeline.Memo.Progress.Batch.VolsTotal} 卷"));
            }
            if (timeline.Memo.Status != null)
                subjects.Add(new TextViewModel(timeline.Memo.Status.Tsukkomi));
            if (timeline.Memo.Subject != null)
                subjects.AddRange(timeline.Memo.Subject.Select(sc =>
                {
                    if (sc.Rate != 0 || !string.IsNullOrWhiteSpace(sc.Comment) || (sc.Reactions != null && sc.Reactions.Count != 0))
                        return new SubjectCollectionViewModel(sc) { IsMy = IsMy };
                    else if (sc.Subject != null)
                        return new SubjectViewModel(sc.Subject);
                    else
                        return new ViewModelBase();
                }));
            //if (timeline.Memo.Wiki != null)
            //    Memo.SubjectViewModels.Add(new BlogViewModel(timeline.Memo.Wiki));

            Memo = new() { SubjectViewModels = [.. subjects] };
        }
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial int? Uid { get; set; }
    [Reactive] public partial bool Batch { get; set; }
    [Reactive] public partial bool IsMy { get; set; }
    [Reactive] public partial int? Replies { get; set; }
    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial TimelineCategory? Category { get; set; }
    [Reactive] public partial int? Type { get; set; }
    [Reactive] public partial string? OperationSource { get; set; }
    [Reactive] public partial string? OperationSourceUrl { get; set; }
    [Reactive] public partial SubjectListViewModel? Memo { get; set; }
}

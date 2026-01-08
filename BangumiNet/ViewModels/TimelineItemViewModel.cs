using Avalonia.Controls;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Converters;
using System.Reactive.Disposables;

namespace BangumiNet.ViewModels;

public partial class TimelineItemViewModel : ItemViewModelBase
{
    public TimelineItemViewModel(Timeline timeline)
    {
        Source = timeline;
        Id = timeline.Id;
        Uid = timeline.Uid;
        Batch = timeline.Batch ?? false;
        Category = (TimelineCategory?)timeline.Cat;
        Replies = timeline.Replies;
        Type = Category?.Parse(timeline.Type);
        OperationSource = timeline.Source?.Name;
        OperationSourceUrl = timeline.Source?.Url;
        IsMy = ApiC.CurrentUsername != null && timeline.User?.Username == ApiC.CurrentUsername;
        CreationTime = CommonUtils.ParseBangumiTime(timeline.CreatedAt);

        List<ViewModelBase> subjects = [];
        if (OperationSource == "web" && string.IsNullOrWhiteSpace(OperationSourceUrl))
            OperationSourceUrl = Settings.BangumiTvUrlBase;
        if (timeline.User != null)
            subjects.Add(User = new(timeline.User));
        if (timeline.Memo is { } memo)
        {
            if (memo.Blog is { } blog)
                subjects.Add(new BlogViewModel(blog));
            if (memo.Daily is { } daily)
            {
                if (daily.Groups != null)
                    subjects.AddRange(daily.Groups.Select(g => new GroupViewModel(g)));
                if (daily.Users != null)
                    subjects.AddRange(daily.Users.Select(u => new UserViewModel(u)));
            }
            if (memo.Index is { } index)
                subjects.Add(new IndexViewModel(index));
            if (memo.Mono is { } mono)
            {
                if (mono.Persons != null)
                    subjects.AddRange(mono.Persons.Select(p => new PersonViewModel(p)));
                if (mono.Characters != null)
                    subjects.AddRange(mono.Characters.Select(c => new CharacterViewModel(c)));
            }
            if (memo.Progress?.Single is { } single)
            {
                if (single.Subject != null)
                    subjects.Add(new SubjectViewModel(single.Subject));
                if (single.Episode != null)
                {
                    var evm = new EpisodeViewModel(single.Episode);
                    subjects.Add(
                        new TextViewModel(() => [
                            $"已完成",
                            new HyperlinkButton()
                            {
                                Content = $"第 {evm.Sort} 话 {NameCnCvt.Convert(evm)}",
                                Command = ReactiveCommand.Create(() => SecondaryWindow.Show(evm)),
                            }]
                        )
                    );
                }
            }
            if (memo.Progress?.Batch is { } batch && batch.Subject != null)
            {
                subjects.Add(new SubjectViewModel(batch.Subject));
                subjects.Add(new TextViewModel($"已完成 {batch.EpsUpdate} / {batch.EpsTotal} 话"));
                subjects.Add(new TextViewModel($"已完成 {batch.VolsUpdate} / {batch.VolsTotal} 卷"));
            }
            if (memo.Status != null)
                subjects.Add(new BBCodeViewModel(memo.Status.Tsukkomi));
            if (memo.Subject != null)
                subjects.AddRange(memo.Subject.Select(sc =>
                {
                    if (sc.Rate != 0 || !string.IsNullOrWhiteSpace(sc.Comment))
                        return new SubjectCollectionViewModel(sc) { IsMy = IsMy };
                    else if (sc.Subject != null)
                        return new SubjectViewModel(sc.Subject);
                    else
                        return new ViewModelBase();
                }));
            if (memo.Wiki?.Subject is { } wikiSubject)
                subjects.Add(new SubjectViewModel(wikiSubject));
        }
        if (Category == TimelineCategory.Status || Replies > 0 || (timeline.Reactions != null && timeline.Reactions.Count > 0))
        {
            Reactions = new ReactionListViewModel(timeline.Reactions, timeline.Id, ItemType.Timeline);
            subjects.Add(
                new TextViewModel(() => [
                    new HyperlinkButton
                    {
                        Content = $"共 {Replies} 条回复",
                        Command = ReactiveCommand.Create(() =>
                        {
                            if (RelationItems!.SubjectViewModels!.FirstOrDefault(vm => vm is CommentListViewModel) is CommentListViewModel replies)
                                replies.IsVisible = !replies.IsVisible;
                            else
                            {
                                replies = new(ItemType.Timeline, Id);
                                replies.LoadPageCommand.Execute(1).Subscribe();
                                RelationItems.SubjectViewModels!.Add(replies);
                            }
                        }),
                    },
                    new ReactionButtonView
                    {
                        DataContext = Reactions
                    },
                    new ReactionListView
                    {
                        DataContext = Reactions,
                    }]
                )
            );
        }
        RelationItems = new() { SubjectViewModels = [.. subjects] };
    }

    protected override void Activate(CompositeDisposable disposables) { }

    [Reactive] public partial int? Uid { get; set; }
    [Reactive] public partial bool Batch { get; set; }
    [Reactive] public partial bool IsMy { get; set; }
    [Reactive] public partial int? Replies { get; set; }
    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial TimelineCategory? Category { get; set; }
    [Reactive] public partial Enum? Type { get; set; }
    [Reactive] public partial string? OperationSource { get; set; }
    [Reactive] public partial string? OperationSourceUrl { get; set; }
    [Reactive] public partial ReactionListViewModel? Reactions { get; set; }

    public override ItemType ItemType { get; init; } = ItemType.Timeline;
}

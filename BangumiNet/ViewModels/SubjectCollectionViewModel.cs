using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.V0.Models;
using BangumiNet.Common;
using DynamicData;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class SubjectCollectionViewModel : ViewModelBase
{
    public SubjectCollectionViewModel(SubjectViewModel subject)
    {
        Parent = subject;
        Comment = null;
        Id = subject.Id;
        EpisodeStatus = 0;
        VolumeStatus = 0;
        Tags = [];
        IsPrivate = false;
        Rating = 0;
        UpdateTime = null;
        Type = CollectionType.Wish;
        SubjectType = subject.Type;

        IsMy = true;

        Init();
    }
    public SubjectCollectionViewModel(UserSubjectCollection collection)
    {
        Source = collection;
        Comment = collection.Comment;
        Id = collection.SubjectId;
        EpisodeStatus = collection.EpStatus;
        VolumeStatus = collection.VolStatus;
        Tags = collection.Tags?.ToObservableCollection() ?? [];
        IsPrivate = collection.Private;
        Rating = collection.Rate;
        UpdateTime = collection.UpdatedAt;
        Type = (CollectionType?)collection.Type;
        SubjectType = (SubjectType?)collection.SubjectType;
        if (collection.Subject != null)
            Parent = new(collection.Subject);

        IsMy = true;

        Init();
    }
    public SubjectCollectionViewModel(SubjectCollectionViewModel collection)
    {
        Source = collection.Source;
        Parent = collection.Parent;
        ParentList = collection.ParentList;
        Comment = collection.Comment;
        Id = collection.Id;
        EpisodeStatus = collection.EpisodeStatus;
        VolumeStatus = collection.VolumeStatus;
        Tags = collection.Tags?.ToObservableCollection() ?? [];
        IsPrivate = collection.IsPrivate;
        Rating = collection.Rating;
        UpdateTime = collection.UpdateTime;
        Type = collection.Type;
        SubjectType = collection.SubjectType;
        IsMy = collection.IsMy;

        Init();
    }
    public SubjectCollectionViewModel(SubjectInterestComment comment)
    {
        Comment = comment.Comment;
        if (comment.UpdatedAt is int ut)
            UpdateTime = DateTimeOffset.FromUnixTimeSeconds(ut).ToLocalTime();
        Type = (CollectionType?)comment.Type;
        Rating = comment.Rate;
        if (comment.User != null)
            User = new(comment.User);
        CommentId = comment.Id;
        ReactionListViewModel = new(comment.Reactions, CommentId, ItemType.Subject);

        Init();
    }
    public SubjectCollectionViewModel(SlimSubjectInterest interest)
    {
        Comment = interest.Comment;
        if (interest.UpdatedAt is int ut)
            UpdateTime = DateTimeOffset.FromUnixTimeSeconds(ut).ToLocalTime();
        Type = (CollectionType?)interest.Type;
        Rating = interest.Rate;
        CommentId = interest.Id;
        Tags = interest.Tags?.ToObservableCollection();

        Init();
    }
    public SubjectCollectionViewModel(TimelineMemo_subject collection)
    {
        IsPrivate = false;
        Comment = collection.Comment;
        CommentId = collection.CollectID;
        Id = collection.Subject?.Id;
        Rating = CommonUtils.ToInt32(collection.Rate);
        SubjectType = (SubjectType?)collection.Subject?.Type;
        if (collection.Subject != null)
            Parent = new(collection.Subject);

        Init();
    }

    private void Init()
    {
        Parent?.CleanValues();
        if (EpisodeStatus == 0 && Parent?.Eps == null) EpisodeStatus = null;
        if (VolumeStatus == 0 && Parent?.VolumeCount == null) VolumeStatus = null;
        IsEpStatusEditable = EpisodeStatus != null && SubjectType == Common.SubjectType.Book;
        IsVolStatusEditable = VolumeStatus != null && SubjectType == Common.SubjectType.Book;
        IsEpStatusReadonly = EpisodeStatus != null && !IsEpStatusEditable;
        IsVolStatusReadonly = VolumeStatus != null && !IsVolStatusEditable;

        OpenEditWindowCommand = ReactiveCommand.Create(() => new SubjectCollectionEditWindow() { DataContext = new SubjectCollectionViewModel(this) }.Show());

        Title = $"修改收藏 - {Parent?.Title}";
    }

    private bool isEditCommandsInitialized;
    public void InitEditCommands()
    {
        if (isEditCommandsInitialized) return;
        isEditCommandsInitialized = true;

        RecommendedTags = Parent?.Tags?.Select(x => x.Name).ToObservableCollection()!;
        AddTagCommand = ReactiveCommand.Create(() => { if (!string.IsNullOrWhiteSpace(TagInput) && !Tags!.Contains(TagInput)) { Tags.Add(TagInput.Trim()); TagInput = string.Empty; } },
            this.WhenAnyValue(x => x.TagInput).Select(y => !string.IsNullOrWhiteSpace(y) && !Tags!.Contains(y)));
        AddRecommendedTagCommand = ReactiveCommand.Create<string>(s => { if (!string.IsNullOrWhiteSpace(s) && !Tags!.Contains(s)) Tags.Add(s); });
        DelTagCommand = ReactiveCommand.Create<string>(s => { if (s != null && Tags!.Contains(s)) Tags.Remove(s); });
        WishCommand = ReactiveCommand.Create(() => Type = CollectionType.Wish, this.WhenAnyValue(x => x.Type).Select(y => y != CollectionType.Wish));
        DoingCommand = ReactiveCommand.Create(() => Type = CollectionType.Doing, this.WhenAnyValue(x => x.Type).Select(y => y != CollectionType.Doing));
        DoneCommand = ReactiveCommand.Create(() => Type = CollectionType.Done, this.WhenAnyValue(x => x.Type).Select(y => y != CollectionType.Done));
        DropCommand = ReactiveCommand.Create(() => Type = CollectionType.Dropped, this.WhenAnyValue(x => x.Type).Select(y => y != CollectionType.Dropped));
        HoldCommand = ReactiveCommand.Create(() => Type = CollectionType.OnHold, this.WhenAnyValue(x => x.Type).Select(y => y != CollectionType.OnHold));
        SaveCommand = ReactiveCommand.CreateFromTask(UpdateCollection);
    }

    [Reactive] public partial UserSubjectCollection? Source { get; set; }
    [Reactive] public partial SubjectViewModel? Parent { get; set; }
    [Reactive] public partial SubjectCollectionListViewModel? ParentList { get; set; }
    [Reactive] public partial SubjectType? SubjectType { get; set; }
    [Reactive] public partial string? Comment { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial int? CommentId { get; set; }
    [Reactive] public partial ReactionListViewModel? ReactionListViewModel { get; set; }
    [Reactive] public partial CollectionType? Type { get; set; }
    [Reactive] public partial int? EpisodeStatus { get; set; }
    [Reactive] public partial int? VolumeStatus { get; set; }
    [Reactive] public partial int? Rating { get; set; }
    [Reactive] public partial bool? IsPrivate { get; set; }
    [Reactive] public partial DateTimeOffset? UpdateTime { get; set; }
    [Reactive] public partial ObservableCollection<string>? Tags { get; set; }
    [Reactive] public partial ObservableCollection<string>? RecommendedTags { get; set; }
    [Reactive] public partial bool IsMy { get; set; }
    [Reactive] public partial UserViewModel? User { get; set; }

    [Reactive] public partial string? TagInput { get; set; }
    [Reactive] public partial bool IsEpStatusEditable { get; set; }
    [Reactive] public partial bool IsVolStatusEditable { get; set; }
    [Reactive] public partial bool IsEpStatusReadonly { get; set; }
    [Reactive] public partial bool IsVolStatusReadonly { get; set; }

    [Reactive] public partial ICommand? OpenEditWindowCommand { get; private set; }

    [Reactive] public partial ReactiveCommand<Unit, Unit>? AddTagCommand { get; set; }
    [Reactive] public partial ReactiveCommand<string, Unit>? AddRecommendedTagCommand { get; set; }
    [Reactive] public partial ReactiveCommand<string, Unit>? DelTagCommand { get; set; }

    [Reactive] public partial ICommand? WishCommand { get; private set; }
    [Reactive] public partial ICommand? DoingCommand { get; private set; }
    [Reactive] public partial ICommand? DoneCommand { get; private set; }
    [Reactive] public partial ICommand? DropCommand { get; private set; }
    [Reactive] public partial ICommand? HoldCommand { get; private set; }

    [Reactive] public partial ReactiveCommand<Unit, bool>? SaveCommand { get; private set; }

    public async Task<bool> UpdateCollection()
    {
        if (Id == null) return false;
        try
        {
            await ApiC.V0.Users.Minus.Collections[(int)Id].PostAsync(new()
            {
                Comment = string.IsNullOrWhiteSpace(Comment) ? null : Comment,
                Private = IsPrivate,
                Rate = Rating,
                Type = (int?)Type,
                Tags = Tags?.ToList(),
                EpStatus = SubjectType == Common.SubjectType.Book ? EpisodeStatus : null,
                VolStatus = SubjectType == Common.SubjectType.Book ? VolumeStatus : null,
            });

            UpdateTime = DateTimeOffset.Now;
            Comment = string.IsNullOrWhiteSpace(Comment) ? null : Comment;
            Parent?.SubjectCollectionViewModel = this;
            var og = ParentList?.SubjectViewModels?.FirstOrDefault(x => x is SubjectCollectionViewModel vm && vm.Id == Id, null);
            if (og != null)
                ParentList?.SubjectViewModels?.Replace(og, this);
            return true;
        }
        catch (ErrorDetail e)
        {
            Trace.TraceError(e.Message);
            switch (e.ResponseStatusCode)
            {
                case 400:
                    MessageWindow.Show("非法请求格式。");
                    break;
                case 401:
                    MessageWindow.Show("未登录。");
                    break;
                case 404:
                    MessageWindow.Show("条目或者章节不存在。");
                    break;
                default: break;
            }
            return false;
        }
    }
}

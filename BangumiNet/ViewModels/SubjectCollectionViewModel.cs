﻿using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.V0.Models;
using BangumiNet.Converters;
using DynamicData;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class SubjectCollectionViewModel : ViewModelBase
{
    public SubjectCollectionViewModel(SubjectViewModel subject)
    {
        Subject = subject;
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
            Subject = new(collection.Subject);

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
        Subject = collection.Subject;
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

    private void Init()
    {
        if (EpisodeStatus == 0 && Subject?.Eps == null) EpisodeStatus = null;
        if (VolumeStatus == 0 && Subject?.Volumes == null) VolumeStatus = null;
        IsEpStatusEditable = EpisodeStatus != null && SubjectType == Api.ExtraEnums.SubjectType.Book;
        IsVolStatusEditable = VolumeStatus != null && SubjectType == Api.ExtraEnums.SubjectType.Book;
        IsEpStatusReadonly = EpisodeStatus != null && !IsEpStatusEditable;
        IsVolStatusReadonly = VolumeStatus != null && !IsVolStatusEditable;

        RecommendedTags = (Parent ?? Subject)?.Tags?.Select(x => x.Name).ToObservableCollection()!;

        OpenEditWindowCommand = ReactiveCommand.Create(() => new SubjectCollectionEditWindow() { DataContext = new SubjectCollectionViewModel(this) }.Show());
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

        Title = $"修改收藏 - {NameCnCvt.Convert(Subject) ?? "项目"} - {Title}";
    }

    [Reactive] public partial UserSubjectCollection? Source { get; set; }
    [Reactive] public partial SubjectViewModel? Subject { get; set; }
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

    public ICommand? OpenEditWindowCommand { get; private set; }
    public ReactiveCommand<Unit, Unit>? AddTagCommand { get; set; }
    public ReactiveCommand<string, Unit>? AddRecommendedTagCommand { get; set; }
    public ReactiveCommand<string, Unit>? DelTagCommand { get; set; }

    public ICommand? WishCommand { get; private set; }
    public ICommand? DoingCommand { get; private set; }
    public ICommand? DoneCommand { get; private set; }
    public ICommand? DropCommand { get; private set; }
    public ICommand? HoldCommand { get; private set; }

    public ReactiveCommand<Unit, bool>? SaveCommand { get; private set; }

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
                EpStatus = SubjectType == Api.ExtraEnums.SubjectType.Book ? EpisodeStatus : null,
                VolStatus = SubjectType == Api.ExtraEnums.SubjectType.Book ? VolumeStatus : null,
            });

            UpdateTime = DateTimeOffset.Now;
            Comment = string.IsNullOrWhiteSpace(Comment) ? null : Comment;
            Parent?.SubjectCollectionViewModel = this;
            var og = ParentList?.SubjectList.SubjectViewModels?.FirstOrDefault(x => x is SubjectCollectionViewModel vm && vm.Id == Id, null);
            if (og != null)
                ParentList?.SubjectList.SubjectViewModels?.Replace(og, this);
            return true;
        }
        catch (ErrorDetail e)
        {
            Trace.TraceError(e.Message);
            switch (e.ResponseStatusCode)
            {
                case 400:
                    MessageWindow.ShowMessage("非法请求格式。");
                    break;
                case 401:
                    MessageWindow.ShowMessage("未登录。");
                    break;
                case 404:
                    MessageWindow.ShowMessage("条目或者章节不存在。");
                    break;
                default: break;
            }
            return false;
        }
    }
}

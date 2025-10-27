using BangumiNet.Api.Interfaces;
using BangumiNet.Converters;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class RevisionViewModel : ViewModelBase
{
    public RevisionViewModel(IRevision revision, ItemViewModelBase? parent)
    {
        Source = revision;
        Id = revision.Id;
        Summary = revision.Summary;
        CreationTime = revision.CreatedAt;
        if (!string.IsNullOrWhiteSpace(revision.Creator?.Username))
            Creator = new(revision.Creator.Username) { Nickname = revision.Creator?.Nickname };
        Type = revision.Type;
        Parent = parent;

        Init();
    }

    private static readonly JsonSerializerOptions options = new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
    public RevisionViewModel(object revisionDetail, ItemViewModelBase parent)
    {
        Source = revisionDetail;
        Parent = parent;
        DATA = JsonSerializer.Serialize(revisionDetail, options);

        Init();
    }

    private void Init()
    {
        Title = $"修订 {Id} - {NameCnCvt.Convert(Parent) ?? $"{Parent?.ItemTypeEnum} {Parent?.Id}"} - {Title}";
        OpenInNewWindowCommand = ReactiveCommand.Create(() =>
        {
            new SecondaryWindow() { Content = this }.Show();
        });
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial int? Type { get; set; }
    [Reactive] public partial ItemViewModelBase? Parent { get; set; }
    [Reactive] public partial UserViewModel? Creator { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }

    [Reactive] public partial string? DATA { get; set; }

    public ICommand? OpenInNewWindowCommand { get; set; }

    // Should be changed after revision detail is done.
    public bool IsFull => Source is not IRevision;
}

using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace BangumiNet.ViewModels;

public partial class RevisionViewModel : ItemViewModelBase
{
    private static readonly JsonSerializerOptions options = new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
    public RevisionViewModel(IRevision revision, ItemViewModelBase? parent)
    {
        Source = revision;
        Id = revision.Id;
        Summary = revision.Summary;
        CreationTime = revision.CreatedAt;
        if (!string.IsNullOrWhiteSpace(revision.Creator?.Username))
            Creator = new(revision.Creator.Username) { Nickname = revision.Creator?.Nickname };
        Type = (RevisionType?)revision.Type;
        Parent = parent;

        var data = revision.GetType().GetProperty("Data")?.GetValue(revision);
        DATA = JsonSerializer.Serialize(data, options);

        Init();
    }

    private void Init()
    {
        Title = $"修订 {Id} - {Parent?.Title}";
    }

    [Reactive] public partial RevisionType? Type { get; set; }
    [Reactive] public partial ItemViewModelBase? Parent { get; set; }
    [Reactive] public partial UserViewModel? Creator { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }

    [Reactive] public partial string? DATA { get; set; }

    // Should be changed after revision detail is done.
    public bool IsFull => Source is not IRevision;
    public override ItemType ItemType { get; init; } = ItemType.Revision;
}

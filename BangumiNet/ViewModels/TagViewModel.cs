using BangumiNet.Api.Interfaces;
using ReactiveUI.SourceGenerators;

namespace BangumiNet.ViewModels;

public partial class TagViewModel : ViewModelBase
{
    public TagViewModel(ITag tag, bool isMeta = false)
    {
        Source = tag;
        Name = tag.Name;
        Count = tag.Count;
        IsMeta = isMeta;
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial int? Count { get; set; }
    [Reactive] public partial bool IsMeta { get; set; }
}

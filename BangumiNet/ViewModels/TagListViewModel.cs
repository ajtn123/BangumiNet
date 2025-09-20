using BangumiNet.Api.Interfaces;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;

namespace BangumiNet.ViewModels;

public partial class TagListViewModel : ViewModelBase
{
    public TagListViewModel(IEnumerable<ITag>? tags, IEnumerable<string>? meta)
    {
        TagViewModels = tags?.Select(t => new TagViewModel(t)).ToObservableCollection();

        if (meta is not null)
            foreach (var mT in meta)
                TagViewModels?.Where(t => t.Name == mT).FirstOrDefault(defaultValue: null)?.IsMeta = true;
    }

    [Reactive] public partial ObservableCollection<TagViewModel>? TagViewModels { get; set; }
}

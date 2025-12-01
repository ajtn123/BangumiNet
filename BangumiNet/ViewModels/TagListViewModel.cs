using BangumiNet.Api.Interfaces;
using BangumiNet.Common;

namespace BangumiNet.ViewModels;

public partial class TagListViewModel : ViewModelBase
{
    public TagListViewModel(IEnumerable<ITag>? tags, IEnumerable<string>? meta, SubjectType? subjectType)
    {
        TagViewModels = tags?.Select(t => new TagViewModel(t, subjectType)).ToObservableCollection();
        SubjectType = subjectType;

        if (meta is not null)
            foreach (var mT in meta)
                TagViewModels?.Where(t => t.Name == mT).FirstOrDefault(defaultValue: null)?.IsMeta = true;
    }

    [Reactive] public partial ObservableCollection<TagViewModel>? TagViewModels { get; set; }
    [Reactive] public partial SubjectType? SubjectType { get; set; }
}

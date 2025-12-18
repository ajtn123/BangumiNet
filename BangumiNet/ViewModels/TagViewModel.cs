using BangumiNet.Api.Interfaces;
using BangumiNet.Common;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class TagViewModel : ViewModelBase
{
    public TagViewModel(ITag tag, SubjectType? subjectType, bool isMeta = false)
    {
        Source = tag;
        Name = tag.Name;
        Count = tag.Count;
        IsMeta = isMeta;
        SubjectType = subjectType;

        SearchTagCommand = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrWhiteSpace(Name)) return;
            var vm = new SearchViewModel();
            vm.Type.FirstOrDefault(x => x?.SubjectType == SubjectType, null)?.IsSelected = true;
            vm.Tag.Add(Name);
            vm.SearchCommand.Execute().Subscribe();
            SecondaryWindow.Show(vm);
        });
        SearchMetaTagCommand = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrWhiteSpace(Name)) return;
            var vm = new SearchViewModel();
            vm.Type.FirstOrDefault(x => x?.SubjectType == SubjectType, null)?.IsSelected = true;
            vm.MetaTag.Add(Name);
            vm.SearchCommand.Execute().Subscribe();
            SecondaryWindow.Show(vm);
        });
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial int? Count { get; set; }
    [Reactive] public partial bool IsMeta { get; set; }
    [Reactive] public partial SubjectType? SubjectType { get; set; }

    public ICommand SearchTagCommand { get; }
    public ICommand SearchMetaTagCommand { get; }
}

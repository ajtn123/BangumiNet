using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
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
            var view = new SearchView();
            view.ViewModel?.Type.FirstOrDefault(x => x?.SubjectType == SubjectType, null)?.IsSelected = true;
            view.ViewModel?.Tag.Add(Name);
            view.ViewModel?.SearchCommand.Execute(null);
            new SecondaryWindow() { Content = view }.Show();
        });
        SearchMetaTagCommand = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrWhiteSpace(Name)) return;
            var view = new SearchView();
            view.ViewModel?.Type.FirstOrDefault(x => x?.SubjectType == SubjectType, null)?.IsSelected = true;
            view.ViewModel?.MetaTag.Add(Name);
            view.ViewModel?.SearchCommand.Execute(null);
            new SecondaryWindow() { Content = view }.Show();
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

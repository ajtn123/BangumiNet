using BangumiNet.Converters;
using BangumiNet.Models;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class ItemViewModelBase : ViewModelBase
{
    public ItemViewModelBase()
    {
        SearchWebCommand = ReactiveCommand.Create(() => CommonUtils.SearchWeb(Name));
        ShowRevisionsCommand = ReactiveCommand.Create(() => SecondaryWindow.Show(RevisionListViewModel));
        OpenInNewWindowCommand = ReactiveCommand.Create(() => SecondaryWindow.Show(this));
        ShowNetworkCommand = ReactiveCommand.Create(() => SecondaryWindow.Show(new ItemNetworkViewModel(this)));

        this.WhenAnyValue(x => x.ItemType, x => x.Name, x => x.NameCn).Skip(1).Subscribe(x =>
        {
            Title = $"{NameCnCvt.Convert(this) ?? $"{ItemType.GetNameCn()} {Id}"} - {Constants.ApplicationName}";
        });
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial ItemType ItemType { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial int? Order { get; set; }
    [Reactive] public partial int? Redirect { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? NameCn { get; set; }
    [Reactive] public partial RevisionListViewModel? RevisionListViewModel { get; set; }
    [Reactive] public partial SubjectListViewModel? RelationItems { get; set; }
    [Reactive] public partial IndexRelation? IndexRelation { get; set; }

    public ICommand? OpenInNewWindowCommand { get; set; }
    public ICommand? SearchWebCommand { get; set; }
    public ICommand? OpenInBrowserCommand { get; set; }
    public ICommand? ShowRevisionsCommand { get; set; }
    public ICommand? ShowNetworkCommand { get; set; }
}

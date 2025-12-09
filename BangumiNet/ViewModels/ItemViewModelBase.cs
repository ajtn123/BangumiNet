using BangumiNet.Converters;
using BangumiNet.Shared.Extensions;
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

        this.WhenAnyValue(x => x.Name, x => x.NameCn).Subscribe(x =>
        {
            Title = $"{NameCnCvt.Convert(this) ?? $"{ItemType.ToStringSC()} {Id}"} - {Constants.ApplicationName}";
        });
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial int? Order { get; set; }
    [Reactive] public partial int? Redirect { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? NameCn { get; set; }
    [Reactive] public partial RevisionListViewModel? RevisionListViewModel { get; set; }
    [Reactive] public partial SubjectListViewModel? RelationItems { get; set; }

    public ICommand? OpenInNewWindowCommand { get; set; }
    public ICommand? SearchWebCommand { get; set; }
    public ICommand? OpenInBrowserCommand { get; set; }
    public ICommand? ShowRevisionsCommand { get; set; }
    public ICommand? ShowNetworkCommand { get; set; }

    public ItemType ItemType { get; protected set; }
}

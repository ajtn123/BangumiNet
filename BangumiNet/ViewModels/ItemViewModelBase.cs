using BangumiNet.Converters;
using BangumiNet.Models;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public abstract partial class ItemViewModelBase : ViewModelBase, IActivatableViewModel, IHasIcon
{
    public ItemViewModelBase()
    {
        this.WhenActivated(disposables =>
        {
            SearchWebCommand = ReactiveCommand.Create(() => CommonUtils.SearchWeb(Name)).DisposeWith(disposables);
            ShowRevisionsCommand = ReactiveCommand.Create(() => { SecondaryWindow.Show(RevisionListViewModel); }).DisposeWith(disposables);
            OpenInNewWindowCommand = ReactiveCommand.Create(() => { SecondaryWindow.Show(this); }).DisposeWith(disposables);
            ShowNetworkCommand = ReactiveCommand.Create(() => { SecondaryWindow.Show(new ItemNetworkViewModel(this)); }).DisposeWith(disposables);

            this.WhenAnyValue(x => x.Name, x => x.NameCn).Subscribe(x =>
            {
                Title = NameCnCvt.Convert(this) ?? $"{ItemType.GetNameCn()} {Id}";
            }).DisposeWith(disposables);

            Activate(disposables);
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
    [Reactive] public partial IndexRelation? IndexRelation { get; set; }

    [Reactive] public partial ReactiveCommand<Unit, Unit>? OpenInNewWindowCommand { get; set; }
    [Reactive] public partial ReactiveCommand<Unit, Unit>? SearchWebCommand { get; set; }
    [Reactive] public partial ReactiveCommand<Unit, Unit>? OpenInBrowserCommand { get; set; }
    [Reactive] public partial ReactiveCommand<Unit, Unit>? ShowRevisionsCommand { get; set; }
    [Reactive] public partial ReactiveCommand<Unit, Unit>? ShowNetworkCommand { get; set; }

    public abstract ItemType ItemType { get; init; }

    public FluentIcons.Common.Icon Icon => IconHelper.GetIcon(ItemType);

    protected abstract void Activate(CompositeDisposable disposables);
    public ViewModelActivator Activator { get; } = new();
}

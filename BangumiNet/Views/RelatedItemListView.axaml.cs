using Avalonia.Controls.Primitives;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class RelatedItemListView : ReactiveUserControl<RelatedItemListViewModel>
{
    public RelatedItemListView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Select(vm => vm.WhenAnyValue(x => x.IsFullyLoaded, x => x.Total))
                .Switch()
                .Where(x => x.Item1)
                .Subscribe(x => IsVisible = x.Item2 != 0)
                .DisposeWith(disposables);
        });
    }

    private void ToggleWrap(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => BadgeScroll.HorizontalScrollBarVisibility = ((ToggleButton)sender!).IsChecked ?? false
            ? ScrollBarVisibility.Disabled
            : ScrollBarVisibility.Auto;
}
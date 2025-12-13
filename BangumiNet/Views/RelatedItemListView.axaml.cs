using Avalonia.Controls.Primitives;
using FluentIcons.Avalonia;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class RelatedItemListView : ReactiveUserControl<RelatedItemListViewModel>
{
    public RelatedItemListView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Select(vm => vm.WhenAnyValue(x => x.IsFullyLoaded, x => x.Total))
                .Switch()
                .Where(x => x.Item1)
                .Subscribe(x => IsVisible = x.Item2 != 0)
                .DisposeWith(d);
        });
    }

    private void ChangeWrap(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (BadgeScroll.HorizontalScrollBarVisibility == ScrollBarVisibility.Auto)
        {
            BadgeScroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            WrapButton.Content = new FluentIcon() { Icon = FluentIcons.Common.Icon.ArrowWrapOff };
        }
        else
        {
            BadgeScroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            WrapButton.Content = new FluentIcon() { Icon = FluentIcons.Common.Icon.ArrowWrap };
        }
    }
}
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using FluentIcons.Avalonia;

namespace BangumiNet.Views;

public partial class PersonBadgeListView : UserControl
{
    public PersonBadgeListView()
    {
        InitializeComponent();
    }

    private void ChangeWrap(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (PersonScroll.HorizontalScrollBarVisibility == ScrollBarVisibility.Auto)
        {
            PersonScroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            WrapButton.Content = new FluentIcon() { Icon = FluentIcons.Common.Icon.ArrowWrapOff };
        }
        else
        {
            PersonScroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            WrapButton.Content = new FluentIcon() { Icon = FluentIcons.Common.Icon.ArrowWrap };
        }
    }
}
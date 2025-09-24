using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using FluentIcons.Avalonia;

namespace BangumiNet.Views;

public partial class CharacterBadgeListView : UserControl
{
    public CharacterBadgeListView()
    {
        InitializeComponent();
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
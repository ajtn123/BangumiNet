using FluentAvalonia.UI.Windowing;
using FluentIcons.Common;

namespace BangumiNet.Views;

public partial class MessageWindow : AppWindow
{
    public MessageWindow()
    {
        InitializeComponent();

        OkButton.Click += (s, e) => Close();
    }

    public static MessageWindow Show(string message, string title = "信息", Icon icon = FluentIcons.Common.Icon.Info)
        => Show(new TextViewModel(message), title, icon);
    public static MessageWindow Show(TextViewModel message, string title = "信息", Icon icon = FluentIcons.Common.Icon.Info)
    {
        var window = new MessageWindow { DataContext = new MessageWindowViewModel { Message = message, Title = title, Icon = icon } };
        window.Show();
        return window;
    }
}
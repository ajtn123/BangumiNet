using Avalonia.Controls;
using FluentIcons.Common;

namespace BangumiNet.Views;

public partial class MessageWindow : Window
{
    public MessageWindow()
    {
        InitializeComponent();

        OkButton.Click += (s, e) => Close();
    }

    public static void ShowMessage(string message, string title = "信息", Icon icon = FluentIcons.Common.Icon.Info)
        => new MessageWindow() { DataContext = new MessageWindowViewModel() { Message = message, Title = title, Icon = icon } }.Show();
}
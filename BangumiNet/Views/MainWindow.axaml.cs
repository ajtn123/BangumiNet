using Avalonia.Controls;
using BangumiNet.ViewModels;

namespace BangumiNet.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        NavigatorButton.Click += (s, e) => new NavigatorWindow() { DataContext = new NavigatorViewModel() }.Show();
    }
}
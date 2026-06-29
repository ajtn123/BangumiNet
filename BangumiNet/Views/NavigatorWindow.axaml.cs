namespace BangumiNet.Views;

public partial class NavigatorWindow : ReactiveWindow<NavigatorViewModel>
{
    public NavigatorWindow()
    {
        InitializeComponent();

        ViewModel = new NavigatorViewModel();
        Navigator.AsyncPopulator = ViewModel.PopulateAsync;

        KeyDown += (s, e) => { if (e.Key == Avalonia.Input.Key.Escape) Close(); };
        Opened += (s, e) =>
        {
            var sender = (NavigatorWindow?)s;
            if (sender?.Owner is SecondaryWindow owner)
                sender.ViewModel?.TargetWindow = owner;
        };
        Deactivated += (s, e) => Close();
    }
}

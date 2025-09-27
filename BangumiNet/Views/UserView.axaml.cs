using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class UserView : UserControl
{
    public UserView()
    {
        InitializeComponent();
    }

    public UserView(bool loadMe)
    {
        InitializeComponent();

        if (loadMe) _ = LoadMe();
    }

    private async Task LoadMe()
        => DataContext = await ApiC.GetViewModelAsync<UserViewModel>();
}
using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class UserView : ReactiveUserControl<UserViewModel>
{
    public UserView()
    {
        InitializeComponent();

        DataContextChanged += async (s, e) =>
        {
            if (dataContextChanges >= 1) return;
            if (DataContext is not UserViewModel viewModel) return;
            if (!viewModel.IsFull)
            {
                var fullUser = await ApiC.GetViewModelAsync<UserViewModel>(username: viewModel.Username);
                if (fullUser == null) return;
                dataContextChanges += 1;
                DataContext = fullUser;
            }
            _ = ViewModel?.Timeline?.Load();
        };

        UserContentTabs.SelectionChanged += (s, e) =>
        {
            if (loadedTabs.Contains(UserContentTabs.SelectedIndex)) return;
            if (UserContentTabs.SelectedContent is Control { DataContext: ILoadable vm })
            {
                loadedTabs.Add(UserContentTabs.SelectedIndex);
                vm.Load();
            }
        };
    }

    private readonly HashSet<int> loadedTabs = [0];
    private uint dataContextChanges = 0;
}

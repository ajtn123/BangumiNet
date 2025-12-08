using Avalonia.Controls;
using BangumiNet.Shared.Interfaces;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class UserView : ReactiveUserControl<UserViewModel>
{
    public UserView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => !vm.IsFull)
            .Subscribe(async vm =>
            {
                var fullItem = await ApiC.GetViewModelAsync<UserViewModel>(username: vm.Username);
                if (fullItem == null) return;
                ViewModel = fullItem;
            });
        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => vm.IsFull)
            .Subscribe(async vm =>
            {
                ViewModel?.Timeline?.LoadCommand.Execute().Subscribe();
            });

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
}

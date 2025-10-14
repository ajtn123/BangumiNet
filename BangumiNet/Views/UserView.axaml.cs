using Avalonia.ReactiveUI;
using System.Reactive.Linq;

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
                if (CollectionTabs.SelectedContent is SubjectCollectionListView v && v.ViewModel != null && v.ViewModel.Total == null && !await v.ViewModel.LoadPageCommand.IsExecuting.FirstAsync())
                    _ = v.ViewModel.LoadPageCommand.Execute(1).Subscribe();
            }
        };

        CollectionTabs.WhenAnyValue(x => x.SelectedContent).Subscribe(async y =>
        {
            if (!(ViewModel?.IsFull ?? false)) return;
            if (y is SubjectCollectionListView v && v.ViewModel != null && v.ViewModel.Total == null && !await v.ViewModel.LoadPageCommand.IsExecuting.FirstAsync())
                _ = v.ViewModel.LoadPageCommand.Execute(1).Subscribe();
        });
    }

    private uint dataContextChanges = 0;
}
using Avalonia.ReactiveUI;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class UserView : ReactiveUserControl<UserViewModel>
{
    public UserView()
    {
        InitializeComponent();

        _ = Init();
    }

    public UserView(bool loadMe)
    {
        InitializeComponent();

        _ = Init(loadMe);
    }

    public async Task Init(bool loadMe = false)
    {
        if (loadMe) await LoadMe();

        CollectionTabs.WhenAnyValue(x => x.SelectedContent).Subscribe(static async y =>
        {
            if (y is SubjectCollectionListView v && v.ViewModel != null && v.ViewModel.Total == null && !await v.ViewModel.LoadPageCommand.IsExecuting.FirstAsync())
                _ = v.ViewModel.LoadPageCommand.Execute(1).Subscribe();
        });
    }

    private async Task LoadMe()
        => DataContext = await ApiC.GetViewModelAsync<UserViewModel>();
}
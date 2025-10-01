using Avalonia.ReactiveUI;
using System.Reactive.Disposables;

namespace BangumiNet.Views;

public partial class UserView : ReactiveUserControl<UserViewModel>
{
    public UserView()
    {
        InitializeComponent();

        Init();
    }

    public UserView(bool loadMe)
    {
        InitializeComponent();

        if (loadMe) _ = LoadMe();

        Init();
    }

    public void Init()
    {
        this.WhenActivated(d =>
        {
            CollectionTabs.WhenAnyValue(x => x.SelectedContent).Subscribe(static y =>
            {
                if (y is SubjectCollectionListView v && v.ViewModel?.Total == null)
                    _ = v.ViewModel?.LoadPageAsync(1);
            }).DisposeWith(d);
        });
    }

    private async Task LoadMe()
        => DataContext = await ApiC.GetViewModelAsync<UserViewModel>();
}
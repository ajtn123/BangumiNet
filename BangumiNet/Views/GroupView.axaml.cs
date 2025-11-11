namespace BangumiNet.Views;

public partial class GroupView : ReactiveUserControl<GroupViewModel>
{
    public GroupView()
    {
        InitializeComponent();
        DataContextChanged += async (s, e) =>
        {
            if (dataContextChanges >= 1) return;
            if (DataContext is not GroupViewModel viewModel) return;
            if (!viewModel.IsFull)
            {
                if (viewModel.Groupname is not string id) return;
                var fullSubject = await ApiC.GetViewModelAsync<GroupViewModel>(username: id);
                if (fullSubject == null) return;
                dataContextChanges += 1;
                DataContext = fullSubject;
            }
            _ = ViewModel?.Members?.Load(1);
            _ = ViewModel?.Topics?.Load(1);
        };
    }

    private uint dataContextChanges = 0;
}
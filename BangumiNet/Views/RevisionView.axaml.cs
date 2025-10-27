namespace BangumiNet.Views;

public partial class RevisionView : ReactiveUserControl<RevisionViewModel>
{
    public RevisionView()
    {
        InitializeComponent();
        DataContextChanged += async (s, e) =>
        {
            if (dataContextChanges >= 1) return;
            if (DataContext is not RevisionViewModel viewModel) return;
            if (!viewModel.IsFull)
            {
                var fullRev = await ApiC.GetViewModelAsync<RevisionViewModel>(viewModel);
                if (fullRev == null) return;
                dataContextChanges += 1;
                DataContext = fullRev;
            }
        };
    }

    private uint dataContextChanges = 0;
}
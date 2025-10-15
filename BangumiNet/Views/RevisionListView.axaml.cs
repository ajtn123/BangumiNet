namespace BangumiNet.Views;

public partial class RevisionListView : ReactiveUserControl<RevisionListViewModel>
{
    public RevisionListView()
    {
        InitializeComponent();

        DataContextChanged += (s, e) =>
        {
            if (DataContext is not RevisionListViewModel viewModel) return;
            if (ViewModel?.RevisionList.SubjectViewModels == null)
                _ = ViewModel?.LoadPageAsync(1);
        };
    }
}
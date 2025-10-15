namespace BangumiNet.Views;

public partial class EpisodeFullView : ReactiveUserControl<EpisodeViewModel>
{
    public EpisodeFullView()
    {
        InitializeComponent();

        DataContextChanged += (s, e) =>
        {
            if (dataContextChanges >= 1) return;
            if (DataContext is not EpisodeViewModel viewModel) return;
            //if (!viewModel.IsFull)
            //{
            //    if (viewModel.Id is not int id) return;
            //    var fullSubject = await ApiC.V0.Subjects[id].GetAsync();
            //    if (fullSubject == null) return;
            //    dataContextChanges += 1;
            //    var vm = new SubjectViewModel(fullSubject);
            //    DataContext = vm;
            //}
            _ = ViewModel?.CommentListViewModel?.LoadPageAsync(1);
        };
    }

    private uint dataContextChanges = 0;
}
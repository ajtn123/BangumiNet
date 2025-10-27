namespace BangumiNet.Views;

public partial class SubjectView : ReactiveUserControl<SubjectViewModel>
{
    public SubjectView()
    {
        InitializeComponent();

        DataContextChanged += async (s, e) =>
        {
            if (dataContextChanges >= 1) return;
            if (DataContext is not SubjectViewModel viewModel) return;
            if (!viewModel.IsFull)
            {
                if (viewModel.Id is not int id) return;
                var fullSubject = await ApiC.GetViewModelAsync<SubjectViewModel>(id);
                if (fullSubject == null) return;
                dataContextChanges += 1;
                DataContext = fullSubject;
            }
            _ = ViewModel?.EpisodeListViewModel?.LoadEpisodes();
            _ = ViewModel?.PersonBadgeListViewModel?.LoadPersons();
            _ = ViewModel?.CharacterBadgeListViewModel?.LoadCharacters();
            _ = ViewModel?.SubjectBadgeListViewModel?.LoadSubjects();
            _ = ViewModel?.CommentListViewModel?.LoadPageAsync(1);
            ViewModel?.SubjectCollectionViewModel = await ApiC.GetViewModelAsync<SubjectCollectionViewModel>(ViewModel.Id);
            ViewModel?.SubjectCollectionViewModel?.Parent = ViewModel;
        };
    }

    private uint dataContextChanges = 0;
}
namespace BangumiNet.Views;

public partial class CharacterView : ReactiveUserControl<CharacterViewModel>
{
    public CharacterView()
    {
        InitializeComponent();

        DataContextChanged += async (s, e) =>
        {
            if (dataContextChanges >= 1) return;
            if (DataContext is not CharacterViewModel viewModel) return;
            if (!viewModel.IsFull)
            {
                if (viewModel.Id is not int id) return;
                var fullChar = await ApiC.GetViewModelAsync<CharacterViewModel>(id);
                if (fullChar == null) return;
                dataContextChanges += 1;
                DataContext = fullChar;
            }
            _ = ViewModel?.SubjectBadgeListViewModel?.LoadSubjects();
            _ = ViewModel?.PersonBadgeListViewModel?.LoadPersons();
            _ = ViewModel?.CommentListViewModel?.LoadPageAsync(1);
            ViewModel?.CollectionTime ??= await ApiC.GetIsCollected(ItemType.Character, ViewModel.Id);
        };
    }

    private int dataContextChanges = 0;
}

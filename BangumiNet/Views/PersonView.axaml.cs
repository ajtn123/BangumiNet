namespace BangumiNet.Views;

public partial class PersonView : ReactiveUserControl<PersonViewModel>
{
    public PersonView()
    {
        InitializeComponent();

        DataContextChanged += async (s, e) =>
        {
            if (dataContextChanges >= 1) return;
            if (DataContext is not PersonViewModel viewModel) return;
            if (!viewModel.IsFull)
            {
                if (viewModel.Id is not int id) return;
                var fullPerson = await ApiC.GetViewModelAsync<PersonViewModel>(id);
                if (fullPerson == null) return;
                dataContextChanges += 1;
                DataContext = fullPerson;
            }
            _ = ViewModel?.SubjectBadgeListViewModel?.LoadSubjects();
            _ = ViewModel?.CharacterBadgeListViewModel?.LoadCharacters();
            _ = ViewModel?.CommentListViewModel?.LoadPageAsync(1);
            ViewModel?.CollectionTime ??= await ApiC.GetIsCollected(ItemType.Person, ViewModel.Id);
        };
    }

    private uint dataContextChanges = 0;
}

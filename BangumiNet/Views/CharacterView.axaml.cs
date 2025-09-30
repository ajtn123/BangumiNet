using Avalonia.ReactiveUI;

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
                var fullSubject = await ApiC.V0.Characters[id].GetAsync();
                if (fullSubject == null) return;
                dataContextChanges += 1;
                var vm = new CharacterViewModel(fullSubject);
                DataContext = vm;
            }
            _ = ViewModel?.SubjectBadgeListViewModel?.LoadSubjects();
            _ = ViewModel?.PersonBadgeListViewModel?.LoadPersons();
            ViewModel?.CollectionTime = await ApiC.GetIsCollected(ItemType.Character, ViewModel.Id);
        };
    }

    private int dataContextChanges = 0;
}
using BangumiNet.Api.V0.V0.Search.Subjects;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class SearchView : ReactiveUserControl<SearchViewModel>
{
    public SearchView()
    {
        InitializeComponent();
        Input.KeyDown += async (s, e) =>
        {
            if (e.Key is Avalonia.Input.Key.Enter && (await ViewModel!.SearchCommand.CanExecute.FirstAsync()))
                ViewModel?.SearchCommand.Execute().Subscribe();
        };
        SortComboBox.ItemsSource = Enum.GetValues<SubjectsPostRequestBody_sort>();
        SearchTypeBox.ItemsSource = (ItemType[])[ItemType.Subject, ItemType.Character, ItemType.Person];

        TagInputBox.KeyDown += (s, e) =>
        {
            if (e.Key == Avalonia.Input.Key.Enter)
                ViewModel?.AddTagCommand?.Execute().Subscribe();
        };
        MetaTagInputBox.KeyDown += (s, e) =>
        {
            if (e.Key == Avalonia.Input.Key.Enter)
                ViewModel?.AddMetaTagCommand?.Execute().Subscribe();
        };
    }
}

using Avalonia.ReactiveUI;
using BangumiNet.Api.V0.V0.Search.Subjects;

namespace BangumiNet.Views;

public partial class SearchView : ReactiveUserControl<SearchViewModel>
{
    public SearchView()
    {
        DataContext = new SearchViewModel();
        InitializeComponent();
        Input.KeyDown += (s, e) =>
        {
            if (e.Key is Avalonia.Input.Key.Enter && (ViewModel?.SearchCommand.CanExecute(null) ?? false))
                ViewModel?.SearchCommand.Execute(null);
        };
        SortComboBox.ItemsSource = Enum.GetValues<SubjectsPostRequestBody_sort>();
        SearchTypeBox.ItemsSource = Enum.GetValues<SearchType>();

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

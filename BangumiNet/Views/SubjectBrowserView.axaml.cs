using BangumiNet.Api.ExtraEnums;
using BangumiNet.Common;

namespace BangumiNet.Views;

public partial class SubjectBrowserView : ReactiveUserControl<SubjectBrowserViewModel>
{
    public SubjectBrowserView()
    {
        InitializeComponent();
        SortComboBox.ItemsSource = Enum.GetValues<SubjectBrowserSort>();
        TypeComboBox.ItemsSource = Enum.GetValues<SubjectType>();
        BookCatComboBox.ItemsSource = (BookType?[])[null, .. Enum.GetValues<BookType>()];
        AnimeCatComboBox.ItemsSource = (AnimeType?[])[null, .. Enum.GetValues<AnimeType>()];
        GameCatComboBox.ItemsSource = (GameType?[])[null, .. Enum.GetValues<GameType>()];
        RealCatComboBox.ItemsSource = (RealType?[])[null, .. Enum.GetValues<RealType>()];
    }

    private void ResetIsSeries(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => ViewModel?.IsSeries = null;
    private void ResetViewModel(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => ViewModel = new();
}

using Avalonia.ReactiveUI;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.ViewModels;

namespace BangumiNet.Views;

public partial class SubjectBrowserView : ReactiveUserControl<SubjectBrowserViewModel>
{
    public SubjectBrowserView()
    {
        DataContext = new SubjectBrowserViewModel();
        InitializeComponent();
        SortComboBox.ItemsSource = Enum.GetValues<SubjectBrowserSort>();
        TypeComboBox.ItemsSource = Enum.GetValues<SubjectType>();
        List<SubjectCategory.Book?> bookCatList = [null, .. Enum.GetValues<SubjectCategory.Book>()];
        List<SubjectCategory.Anime?> AnimeCatList = [null, .. Enum.GetValues<SubjectCategory.Anime>()];
        List<SubjectCategory.Game?> gameCatList = [null, .. Enum.GetValues<SubjectCategory.Game>()];
        List<SubjectCategory.Real?> realCatList = [null, .. Enum.GetValues<SubjectCategory.Real>()];
        BookCatComboBox.ItemsSource = bookCatList;
        AnimeCatComboBox.ItemsSource = AnimeCatList;
        GameCatComboBox.ItemsSource = gameCatList;
        RealCatComboBox.ItemsSource = realCatList;
    }

    private void ResetIsSeries(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => ViewModel?.IsSeries = null;
    private void ResetViewModel(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        => ViewModel = new();
}

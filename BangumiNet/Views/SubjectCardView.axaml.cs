namespace BangumiNet.Views;

public partial class SubjectCardView : ReactiveUserControl<SubjectViewModel>
{
    public SubjectCardView()
    {
        InitializeComponent();

        CardGrid.ContextFlyout?.Opened += (s, e) =>
        {
            if (ViewModel?.Id is not int id) return;
            OpenInBrowserSplitButton.Flyout ??= SubjectView.GetOpenInBrowserFlyout(id);
        };
    }
}
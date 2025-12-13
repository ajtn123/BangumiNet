namespace BangumiNet.Views;

public partial class SubjectBadgeView : ReactiveUserControl<SubjectViewModel>
{
    public SubjectBadgeView()
    {
        InitializeComponent();

        BadgeGrid.ContextFlyout?.Opened += (s, e) =>
        {
            if (ViewModel?.Id is not int id) return;
            OpenInBrowserSplitButton.Flyout ??= SubjectView.GetOpenInBrowserFlyout(id);
        };
    }
}
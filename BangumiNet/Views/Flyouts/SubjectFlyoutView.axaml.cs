namespace BangumiNet.Views;

public partial class SubjectFlyoutView : ReactiveUserControl<SubjectViewModel>
{
    public SubjectFlyoutView()
    {
        InitializeComponent();

        AttachedToVisualTree += (s, e) =>
        {
            if (ViewModel?.Id is not int id) return;
            OpenInBrowserSplitButton.Flyout ??= SubjectView.GetOpenInBrowserFlyout(id);
        };
    }
}

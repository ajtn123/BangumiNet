namespace BangumiNet.ViewModels;

public partial class BBCodeViewModel : ViewModelBase
{
    public BBCodeViewModel(string? text) => Text = text;
    [Reactive] public partial string? Text { get; set; }
}

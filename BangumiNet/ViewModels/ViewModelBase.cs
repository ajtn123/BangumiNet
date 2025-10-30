namespace BangumiNet.ViewModels;

public partial class ViewModelBase : ReactiveObject
{
    public ViewModelBase()
    {
        Title = Shared.Constants.ApplicationName;
        IsVisible = true;
    }
    public static Settings CurrentSettings => SettingProvider.CurrentSettings;
    [Reactive] public partial string Title { get; set; }
    [Reactive] public partial bool IsVisible { get; set; }
}

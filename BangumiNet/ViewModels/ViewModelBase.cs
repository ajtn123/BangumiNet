namespace BangumiNet.ViewModels;

public partial class ViewModelBase : ReactiveObject
{
    public ViewModelBase()
    {
        Title = Constants.ApplicationName;
    }
    public static Settings CurrentSettings => SettingProvider.CurrentSettings;
    [Reactive] public partial string Title { get; set; }
}

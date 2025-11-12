namespace BangumiNet.ViewModels;

public partial class ViewModelBase : ReactiveObject
{
    public ViewModelBase()
    {
        Title = Shared.Constants.ApplicationName;
        IsVisible = true;
        IsEmphasized = false;
    }
    public static Settings CurrentSettings => SettingProvider.CurrentSettings;
    [Reactive] public partial string Title { get; set; }
    [Reactive] public partial bool IsVisible { get; set; }
    [Reactive] public partial bool IsEmphasized { get; set; }
}

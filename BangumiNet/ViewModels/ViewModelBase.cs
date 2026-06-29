using FluentIcons.Common;

namespace BangumiNet.ViewModels;

public partial class ViewModelBase : ReactiveObject
{
    public ViewModelBase()
    {
        Icon = Icon.Document;
        Title = Constants.ApplicationName;
        IsVisible = true;
        IsHighlighted = false;
        IsLoaded = false;
    }

    public static Settings Settings => SettingProvider.Current;

    [Reactive] public virtual partial Icon Icon { get; set; }
    [Reactive] public partial string Title { get; set; }
    [Reactive] public partial bool IsVisible { get; set; }
    [Reactive] public partial bool IsHighlighted { get; set; }
    [Reactive] public partial bool IsLoaded { get; set; }
}

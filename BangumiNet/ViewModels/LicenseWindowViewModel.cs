using BangumiNet.Models;

namespace BangumiNet.ViewModels;

public partial class LicenseWindowViewModel : ViewModelBase
{
    public LicenseWindowViewModel(LicenseItem item)
    {
        Item = item;
        Title = $"{Item.Name} - {Item.Version} - 许可证";
        Subtitle = string.Join(" · ", new string?[] { item.Version, item.License, item.RepositoryUrl }.Where(x => !string.IsNullOrWhiteSpace(x)));
    }

    [Reactive] public partial LicenseItem Item { get; protected set; }
    [Reactive] public partial string Subtitle { get; protected set; }
}

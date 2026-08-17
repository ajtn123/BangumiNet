using BangumiNet.Models;

namespace BangumiNet.ViewModels;

public partial class LicenseWindowViewModel : ViewModelBase
{
    public LicenseWindowViewModel(LicenseItem item)
    {
        Item = item;
        Title = $"{Item.Name} - 许可证";
    }

    [Reactive] public partial LicenseItem Item { get; protected set; }
}

using System.Diagnostics.CodeAnalysis;

namespace BangumiNet.Utils;

public class ItemViewModelEqualityComparer : IEqualityComparer<ItemViewModelBase>
{
    public bool Equals(ItemViewModelBase? x, ItemViewModelBase? y)
    {
        if (x == null || y == null) return false;
        if (x.ItemType == y.ItemType && x.Id == y.Id) return true;
        return false;
    }
    public int GetHashCode([DisallowNull] ItemViewModelBase obj)
    {
        return (obj.Id ?? -1) * (int)obj.ItemType * 1024;
    }
}

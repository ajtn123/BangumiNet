using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;

namespace BangumiNet.Utils;

public class UserControlHelpers
{
    public static readonly AttachedProperty<string> ParentTitleProperty
        = AvaloniaProperty.RegisterAttached<UserControl, string>("ParentTitle", typeof(UserControlHelpers), "UnsetTitle");

    static UserControlHelpers()
    {
        ParentTitleProperty.Changed.AddClassHandler<UserControl>((uc, e) => uc.GetLogicalParent<SecondaryWindow>()?.Title = e.NewValue?.ToString());
        StyledElement.ParentProperty.Changed.AddClassHandler<UserControl>((uc, e) => uc.GetLogicalParent<SecondaryWindow>()?.Title = uc.GetValue(ParentTitleProperty)?.ToString());
    }

    public static void SetParentTitle(UserControl element, string value)
        => element.SetValue(ParentTitleProperty, value);
    public static string GetParentTitle(UserControl element)
        => element.GetValue(ParentTitleProperty);
}

using System.ComponentModel;
using System.Globalization;
using FluentIcons.Common;

namespace FluentIcons.Avalonia.Fluent;

public class FluentIconConverter<T> : TypeConverter where T : IHasIcon
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string) || sourceType == typeof(Icon))
        {
            return true;
        }
        return base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        return value switch
        {
            string name => new FluentIconSource { Icon = Enum.Parse<Icon>(name) },
            Icon icon => new FluentIconSource { Icon = icon },
            _ => base.ConvertFrom(context, culture, value)
        };
    }
}

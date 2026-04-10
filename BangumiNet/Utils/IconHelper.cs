using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using FluentAvalonia.UI.Controls;
using FluentIcons.Avalonia.Fluent;
using FluentIcons.Common;
using System.Collections.Concurrent;

namespace BangumiNet.Utils;

public static class IconHelper
{
    public static Icon GetIcon(ItemType type) => type switch
    {
        ItemType.Subject => Icon.MoviesAndTv,
        ItemType.Episode => Icon.Layer,
        ItemType.Character => Icon.PersonSquare,
        ItemType.Person => Icon.Person,
        ItemType.User => Icon.PersonCircle,
        ItemType.Topic => Icon.Comment,
        ItemType.Group => Icon.PeopleTeam,
        ItemType.Timeline => Icon.Timeline,
        ItemType.Revision => Icon.History,
        ItemType.Blog => Icon.DrawText,
        ItemType.Photo => Icon.Image,
        ItemType.Index => Icon.AppsListDetail,
        ItemType.Cover => Icon.Image,
        _ => Icon.Document,
    };

    private static readonly ConcurrentDictionary<Icon, FluentImage> fluentImages = [];
    public static FluentImage GetFluentImage(Icon icon) => fluentImages.GetOrAdd(icon, icon => new(icon));

    public class FluentImage(Icon icon) : IImage
    {
        public Size Size => new(16, 16);

        public void Draw(DrawingContext context, Rect sourceRect, Rect destRect)
        {
            (brush.Visual as FAIconElement)?.Foreground = App.Current.FindResource(App.Current.ActualThemeVariant, "TextFillColorPrimaryBrush") as IBrush;
            context.DrawRectangle(brush, null, destRect);
        }

        private readonly VisualBrush brush = new(new FluentIcon() { Icon = icon });
    }
}

public interface IHasIcon
{
    Icon Icon { get; }
}

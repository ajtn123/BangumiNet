using FluentAvalonia.UI.Controls;
using FluentIcons.Common;

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

    public static ImageIconSource GetIconSource(ItemType type)
        => IconSource.FromIcon(GetIcon(type));
}

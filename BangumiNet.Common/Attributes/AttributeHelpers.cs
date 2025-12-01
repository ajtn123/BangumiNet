using System.ComponentModel;
using System.Reflection;

namespace BangumiNet.Common.Attributes;

public static class AttributeHelpers
{
    private static T? GetAttribute<T>(this Enum value) where T : Attribute
    {
        var type = value.GetType();
        var name = value.ToString();
        var field = type.GetField(name);

        return field?.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
    }

    public static string GetNameCn(this PersonCharacterRelationType value)
        => value.GetAttribute<NameCnAttribute>()!.NameCn;
    public static string? GetDescription(this PersonCharacterRelationType value)
        => value.GetAttribute<DescriptionAttribute>()?.Description;
    public static PersonCharacterRelationType? GetViceVersa(this PersonCharacterRelationType value)
        => value.GetAttribute<ViceVersaAttribute<PersonCharacterRelationType>>()?.Value;
    public static bool GetIsPrimary(this PersonCharacterRelationType value)
        => value.GetAttribute<PrimaryAttribute>() != null;
    public static bool GetIsViceVersaSkipped(this PersonCharacterRelationType value)
        => value.GetAttribute<SkipViceVersaAttribute>() != null;
    public static PersonCharacterRelationCategory GetCategory(this PersonCharacterRelationType value)
        => (int)value switch
        {
            <= 999 => PersonCharacterRelationCategory.PersonCharacter,
            <= 1999 => PersonCharacterRelationCategory.Person,
            _ => PersonCharacterRelationCategory.Character,
        };

    public static string[] GetSortKeys(this SubjectType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.SortKeys!;
    public static string GetWikiTemplate(this SubjectType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.WikiTemplate!;
    public static Type GetSpecificType(this SubjectType value)
        => value.GetAttribute<SpecificTypeAttribute>()!.SpecificType;

    public static string GetName(this GamePlatform value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Name;
    public static string GetNameCn(this GamePlatform value)
        => value.GetAttribute<PlatformInfoAttribute>()!.NameCn;
    public static string GetAlias(this GamePlatform value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Alias;
    public static string[]? GetSearchKeywords(this GamePlatform value)
        => value.GetAttribute<PlatformInfoAttribute>()!.SearchKeywords;
    public static int GetOrder(this GamePlatform value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Order;

    public static string GetName(this BookType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Name;
    public static string GetNameCn(this BookType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.NameCn;
    public static string GetAlias(this BookType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Alias;
    public static string GetWikiTemplate(this BookType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.WikiTemplate!;
    public static bool GetIsHeaderEnabled(this BookType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.IsHeaderEnabled;
    public static int GetOrder(this BookType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Order;

    public static string GetName(this BookSeriesType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Name;
    public static string GetNameCn(this BookSeriesType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.NameCn;
    public static string GetAlias(this BookSeriesType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Alias;
    public static int GetOrder(this BookSeriesType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Order;

    public static string GetName(this AnimeType value)
    => value.GetAttribute<PlatformInfoAttribute>()!.Name;
    public static string GetNameCn(this AnimeType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.NameCn;
    public static string GetAlias(this AnimeType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Alias;
    public static string GetWikiTemplate(this AnimeType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.WikiTemplate!;
    public static bool GetIsHeaderEnabled(this AnimeType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.IsHeaderEnabled;
    public static int GetOrder(this AnimeType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Order;
    public static string[]? GetSortKeys(this AnimeType value)
    {
        var keys = value.GetAttribute<PlatformInfoAttribute>()!.SortKeys;
        if (keys == null)
            return null;
        else if (keys.Length == 0)
            return value.GetParentType().GetSortKeys();
        else
            return keys;
    }

    public static string GetName(this RealType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Name;
    public static string GetNameCn(this RealType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.NameCn;
    public static string GetAlias(this RealType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Alias;
    public static string GetWikiTemplate(this RealType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.WikiTemplate!;
    public static bool GetIsHeaderEnabled(this RealType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.IsHeaderEnabled;
    public static int GetOrder(this RealType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Order;

    public static string GetName(this GameType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Name;
    public static string GetNameCn(this GameType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.NameCn;
    public static string GetAlias(this GameType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Alias;
    public static bool GetIsHeaderEnabled(this GameType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.IsHeaderEnabled;
    public static int GetOrder(this GameType value)
        => value.GetAttribute<PlatformInfoAttribute>()!.Order;

    public static SubjectType GetParentType(this BookType value)
        => value.GetType().GetCustomAttribute<ParentTypeAttribute<SubjectType>>()!.ParentType;
    public static SubjectType GetParentType(this AnimeType value)
        => value.GetType().GetCustomAttribute<ParentTypeAttribute<SubjectType>>()!.ParentType;
    public static SubjectType GetParentType(this MusicType value)
        => value.GetType().GetCustomAttribute<ParentTypeAttribute<SubjectType>>()!.ParentType;
    public static SubjectType GetParentType(this GameType value)
        => value.GetType().GetCustomAttribute<ParentTypeAttribute<SubjectType>>()!.ParentType;
    public static SubjectType GetParentType(this RealType value)
        => value.GetType().GetCustomAttribute<ParentTypeAttribute<SubjectType>>()!.ParentType;

    public static string GetName(this NetworkService value)
        => value.GetAttribute<ServiceInfoAttribute>()!.Name;
    public static string GetTitle(this NetworkService value)
        => value.GetAttribute<ServiceInfoAttribute>()!.Title;
    public static string GetBackgroundColor(this NetworkService value)
        => value.GetAttribute<ServiceInfoAttribute>()!.BackgroundColor;
    public static string? GetUrl(this NetworkService value)
        => value.GetAttribute<ServiceInfoAttribute>()!.Url;
    public static string? GetValidationRegex(this NetworkService value)
        => value.GetAttribute<ServiceInfoAttribute>()!.ValidationRegex;

    public static string GetName(this TimelineSource value)
        => value.GetAttribute<SourceInfoAttribute>()!.Name;
    public static string? GetUrl(this TimelineSource value)
        => value.GetAttribute<SourceInfoAttribute>()!.Url;
    public static string? GetAppId(this TimelineSource value)
        => value.GetAttribute<SourceInfoAttribute>()!.AppId;

    public static string GetNameEn(this StaffCategory value)
        => value.GetAttribute<NameEnAttribute>()!.NameEn;
    public static string GetNameCn(this StaffCategory value)
        => value.GetAttribute<NameCnAttribute>()!.NameCn;
    public static SubjectType GetSubjectType(this StaffCategory value)
        => (int)value switch
        {
            <= 999 => SubjectType.Anime,
            _ => SubjectType.Game,
        };

    public static string GetNameCn(this SubjectStaff value)
        => value.GetAttribute<NameCnAttribute>()!.NameCn;
    public static string? GetNameEn(this SubjectStaff value)
        => value.GetAttribute<NameEnAttribute>()?.NameEn;
    public static string? GetNameJp(this SubjectStaff value)
        => value.GetAttribute<NameJpAttribute>()?.NameJp;
    public static string? GetDescription(this SubjectStaff value)
        => value.GetAttribute<DescriptionAttribute>()?.Description;
    public static StaffCategory[]? GetCategories(this SubjectStaff value)
        => value.GetAttribute<CategoriesAttribute<StaffCategory>>()?.Categories;
    public static SubjectType GetSubjectType(this SubjectStaff value)
        => (int)value switch
        {
            <= 999 => SubjectType.Anime,
            <= 1999 => SubjectType.Game,
            <= 2999 => SubjectType.Book,
            <= 3999 => SubjectType.Music,
            _ => SubjectType.Real,
        };
}

using AngleSharp.Dom;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Html.Models;
using BangumiNet.Shared;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BangumiNet.Api.Html.Parsers;

public static partial class CommentParser
{
    public static CommentPage? ParseSubjectComment(IDocument? doc)
    {
        if (doc == null) return null;


        var pageInfoStr = doc.QuerySelector(".p_edge")?.TextContent;
        int? pageTotal = ParsePageInfo(pageInfoStr)?.ttl;

        List<Comment> comments = [];
        var commentBox = doc.GetElementById("comment_box");
        var items = commentBox?.QuerySelectorAll(".item");
        if (items != null)
            foreach (var item in items)
            {
                var username = item.GetAttribute("data-item-user");
                var avatarStyle = item.QuerySelector(@".avatar > [style*=""background-image""]")?.GetAttribute("style");
                var avatarUrl = GetUrlFromStyle(avatarStyle);
                var container = item.QuerySelector(".text");
                var nickname = container?.QuerySelector("a.l[href]")?.TextContent;
                var info = container?.QuerySelectorAll("small.grey").Select(info => info.TextContent);
                var cType = info?.Select(EnumExtensions.ParseCollectionType).FirstNotNull();
                var cTime = info?.Select<string?, DateTime?>(i => DateTime.TryParse(i?.Replace("@", ""), CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var time) ? time : null)
                    .FirstNotNull();
                var cTimeStr = info?.Select(s => s.Trim()).FirstOrDefault(s => s!.StartsWith('@'), null)?.Replace("@", "")?.Trim();
                var content = container?.QuerySelector(".comment")?.TextContent;
                var score = container?.QuerySelector(".starlight")?.ClassList.Select(ParseScoreClass).FirstNotNull();
                var com = new Comment()
                {
                    Username = username,
                    Nickname = nickname,
                    AvatarUrl = avatarUrl,
                    CollectionType = cType,
                    CollectionTime = cTime != null ? new(cTime.Value) : null,
                    CollectionTimeString = cTimeStr,
                    Content = content,
                    Score = score
                };
                comments.Add(com);
            }

        return new() { Comments = comments, TotalPage = pageTotal };
    }

    [GeneratedRegex(@"background-image:url\('(?<url>.+)'\)")]
    private static partial Regex BackgroundImageStyle();
    private static string GetUrlFromStyle(string? style)
    {
        if (string.IsNullOrWhiteSpace(style)) return UrlProvider.DefaultUserAvatarUrl;
        var match = BackgroundImageStyle().Match(style);

        if (match.Groups.TryGetValue("url", out var group) && group.Value is string url)
            if (url.StartsWith("//")) return "https:" + url;
            else return url;
        else return UrlProvider.DefaultUserAvatarUrl;
    }

    [GeneratedRegex(@"stars(?<score>[0-9]+)")]
    private static partial Regex ScoreClass();
    private static int? ParseScoreClass(string? str)
    {
        if (string.IsNullOrWhiteSpace(str)) return null;
        var match = ScoreClass().Match(str);

        if (match.Groups.TryGetValue("score", out var group))
            return int.TryParse(group.Value, out var result) ? result : null;
        else return null;
    }

    [GeneratedRegex(@"[^0-9]*(?<cur>[0-9]+)[^0-9]+(?<ttl>[0-9]+)[^0-9]*")]
    private static partial Regex PageInfo();
    private static (int cur, int ttl)? ParsePageInfo(string? str)
    {
        if (string.IsNullOrWhiteSpace(str)) return null;
        var match = PageInfo().Match(str);

        if (match.Groups.TryGetValue("cur", out var cur) && match.Groups.TryGetValue("ttl", out var ttl))
            return (int.Parse(cur.Value), int.Parse(ttl.Value));
        else return null;
    }



    private static T? FirstNotNull<T>(this IEnumerable<T> ts)
        => ts.FirstOrDefault(t => t != null);
}

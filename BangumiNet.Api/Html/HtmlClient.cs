using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io.Network;
using BangumiNet.Api.Html.Models;
using BangumiNet.Api.Html.Parsers;
using BangumiNet.Shared;
using BangumiNet.Shared.Interfaces;
using System.Net;

namespace BangumiNet.Api.Html;

public class HtmlClient
{
    private readonly HttpClient httpClient;
    private readonly IApiSettings settings;
    private readonly IBrowsingContext context;

    public HtmlClient(IApiSettings settings, HttpClient httpClient)
    {
        this.settings = settings;
        this.httpClient = httpClient;
        var config = Configuration.Default
            .With(new HttpClientRequester(httpClient))
            .WithDefaultLoader();
        context = BrowsingContext.New(config);
    }

    public async Task<CommentPage?> GetCommentsAsync(ItemType type, int id, int page)
    {
        var doc = await GetDocumentAsync($"{settings.BangumiTvUrlBase}/subject/{id}/comments?page={page}");
        var cp = CommentParser.ParseSubjectComment(doc);
        cp?.Page = page;
        cp?.Type = type;
        cp?.Id = id;
        return cp;
    }

    public async Task<IDocument?> GetDocumentAsync(string url)
    {
        var document = await context.OpenAsync(url);

        if (document.StatusCode != HttpStatusCode.OK) return null;

        else return document;
    }
}

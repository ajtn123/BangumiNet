using BangumiNet.Common.Attributes;

namespace BangumiNet.Common.Test;

[TestClass]
public sealed class PlatformTest
{
    [TestMethod]
    public void SortKeys()
    {
        var keys = Enum.GetValues<MediaType>().Select(x => x.GetSortKeys());
        foreach (var item in keys)
            Assert.IsNotEmpty(item);
    }

    [TestMethod]
    public void WikiTemplate()
    {
        string[] keys = [
            .. Enum.GetValues<MediaType>().Select(x=>x.GetWikiTemplate()),
            .. Enum.GetValues<AnimeType>().Select(x=>x.GetWikiTemplate()),
            .. Enum.GetValues<RealType>().Select(x=>x.GetWikiTemplate()),
        ];
        foreach (var item in keys)
            Assert.IsNotEmpty(item);
    }

    [TestMethod]
    public void SpecificType()
    {
        var keys = Enum.GetValues<MediaType>().Select(x => x.GetSpecificType());
        foreach (var item in keys)
            Assert.IsTrue(item.IsEnum);
    }

    [TestMethod]
    public void ParentType()
    {
        Assert.AreEqual(MediaType.Book, BookType.Official.GetParentType());
        Assert.AreEqual(MediaType.Anime, AnimeType.AnimeComic.GetParentType());
        Assert.AreEqual(MediaType.Music, MusicType.Drama.GetParentType());
        Assert.AreEqual(MediaType.Game, GameType.Software.GetParentType());
        Assert.AreEqual(MediaType.Real, RealType.CN.GetParentType());
    }

    [TestMethod]
    public void Name()
    {
        string[] keys = [
            .. Enum.GetValues<GamePlatform>().Select(x => x.GetName()),
            .. Enum.GetValues<BookSeriesType>().Select(x => x.GetName()),
            .. Enum.GetValues<BookType>().Select(x => x.GetName()),
            .. Enum.GetValues<AnimeType>().Select(x => x.GetName()),
            .. Enum.GetValues<RealType>().Select(x => x.GetName()),
            .. Enum.GetValues<GameType>().Select(x => x.GetName()),
        ];
        foreach (var item in keys)
            Assert.IsNotEmpty(item);
    }

    [TestMethod]
    public void NameCn()
    {
        string[] keys = [
            .. Enum.GetValues<GamePlatform>().Select(x => x.GetNameCn()),
            .. Enum.GetValues<BookSeriesType>().Select(x => x.GetNameCn()),
            .. Enum.GetValues<BookType>().Select(x => x.GetNameCn()),
            .. Enum.GetValues<AnimeType>().Select(x => x.GetNameCn()),
            .. Enum.GetValues<RealType>().Select(x => x.GetNameCn()),
            .. Enum.GetValues<GameType>().Select(x => x.GetNameCn()),
        ];
        foreach (var item in keys)
            Assert.IsNotEmpty(item);
    }

    [TestMethod]
    public void Alias()
    {
        string[] keys = [
            .. Enum.GetValues<GamePlatform>().Select(x => x.GetAlias()),
            .. Enum.GetValues<BookSeriesType>().Select(x => x.GetAlias()),
            .. Enum.GetValues<BookType>().Select(x => x.GetAlias()),
            .. Enum.GetValues<AnimeType>().Select(x => x.GetAlias()),
            .. Enum.GetValues<RealType>().Select(x => x.GetAlias()),
            .. Enum.GetValues<GameType>().Select(x => x.GetAlias()),
        ];
        foreach (var item in keys)
            Assert.IsNotEmpty(item);
    }

    [TestMethod]
    public void SearchKeywords()
    {
        var keys = Enum.GetValues<GamePlatform>().Select(x => x.GetSearchKeywords());
        foreach (var item in keys)
        {
            Assert.IsNotNull(item);
            Assert.IsNotEmpty(item);
        }
    }

    [TestMethod]
    public void Order()
    {
        int[] keys = [
            .. Enum.GetValues<GamePlatform>().Select(x => x.GetOrder()),
            .. Enum.GetValues<BookSeriesType>().Select(x => x.GetOrder()),
            .. Enum.GetValues<BookType>().Select(x => x.GetOrder()),
            .. Enum.GetValues<AnimeType>().Select(x => x.GetOrder()),
            .. Enum.GetValues<RealType>().Select(x => x.GetOrder()),
            .. Enum.GetValues<GameType>().Select(x => x.GetOrder()),
        ];
        foreach (var item in keys)
            Assert.IsGreaterThanOrEqualTo(0, item);
    }

    [TestMethod]
    public void IsHeaderEnabled()
    {
        bool[] keys = [
            .. Enum.GetValues<GameType>().Select(x => x.GetIsHeaderEnabled()),
            .. Enum.GetValues<RealType>().Select(x => x.GetIsHeaderEnabled()),
            .. Enum.GetValues<AnimeType>().Select(x => x.GetIsHeaderEnabled()),
            .. Enum.GetValues<BookType>().Select(x => x.GetIsHeaderEnabled()),
        ];
        Assert.IsNotEmpty(keys.Where(x => x));
        Assert.IsNotEmpty(keys.Where(x => !x));
    }
}
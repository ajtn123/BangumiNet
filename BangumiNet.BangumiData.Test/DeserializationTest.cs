namespace BangumiNet.BangumiData.Test;

[TestClass]
public sealed class DeserializationTest
{
    [TestMethod]
    public async Task LoadFile()
    {
        using var fs = await new HttpClient().GetStreamAsync("https://unpkg.com/bangumi-data@0.3/dist/data.json", TestContext.CancellationToken);
        var obj = await BangumiDataLoader.LoadAsync(fs);

        Assert.IsNotEmpty(obj.SiteMeta);
        Assert.IsNotEmpty(obj.Items);
    }

    public TestContext TestContext { get; set; }
}

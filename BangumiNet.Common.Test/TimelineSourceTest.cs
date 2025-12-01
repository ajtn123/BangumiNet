using BangumiNet.Common.Attributes;

namespace BangumiNet.Common.Test;

[TestClass]
public sealed class TimelineSourceTest
{
    [TestMethod]
    public void Name()
    {
        var keys = Enum.GetValues<TimelineSource>().Select(x => x.GetName());
        foreach (var item in keys)
            Assert.IsNotEmpty(item);
    }

    [TestMethod]
    public void Url()
    {
        var keys = Enum.GetValues<TimelineSource>().Select(x => x.GetUrl());

        Assert.IsNotEmpty(keys.Where(string.IsNullOrEmpty));
    }

    [TestMethod]
    public void AppId()
    {
        var keys = Enum.GetValues<TimelineSource>().Select(x => x.GetAppId());

        Assert.IsNotEmpty(keys.Where(string.IsNullOrEmpty));
    }
}

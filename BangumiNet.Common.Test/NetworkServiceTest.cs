using BangumiNet.Common.Attributes;

namespace BangumiNet.Common.Test;

[TestClass]
public sealed class NetworkServiceTest
{
    [TestMethod]
    public void Name()
    {
        var keys = Enum.GetValues<NetworkService>().Select(x => x.GetName());
        foreach (var item in keys)
            Assert.IsNotEmpty(item);
    }

    [TestMethod]
    public void Title()
    {
        var keys = Enum.GetValues<NetworkService>().Select(x => x.GetTitle());
        foreach (var item in keys)
            Assert.IsNotEmpty(item);
    }

    [TestMethod]
    public void BackgroundColor()
    {
        var keys = Enum.GetValues<NetworkService>().Select(x => x.GetBackgroundColor());
        foreach (var item in keys)
            Assert.IsNotEmpty(item);
    }

    [TestMethod]
    public void Url()
    {
        Assert.IsNotNull(NetworkService.Instagram.GetUrl());
        Assert.IsNull(NetworkService.FriendCode.GetUrl());
    }

    [TestMethod]
    public void ValidationRegex()
    {
        Assert.IsNotNull(NetworkService.BattleTag.GetValidationRegex());
        Assert.IsNull(NetworkService.GitHub.GetValidationRegex());
    }
}

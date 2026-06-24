using BangumiNet.Api.Helpers;

namespace BangumiNet.Api.Test;

[TestClass]
public sealed class BangumiImageTest
{
    public const int TargetWidth = 800;
    public const int TargetHeight = 600;

    public const string ResizedImage = "https://lain.bgm.tv/r/800x600/pic/cover/l/7d/c3/4124_pPP9r.jpg";
    public const string OriginalImage = "https://lain.bgm.tv/pic/cover/l/7d/c3/4124_pPP9r.jpg";
    public const string InvalidImage = "https://example.com/photo.png";

    [TestMethod]
    public void IsBangumiImage()
    {
        var r = BangumiImageHelper.IsBangumiImage(ResizedImage);
        Assert.IsTrue(r);

        var o = BangumiImageHelper.IsBangumiImage(OriginalImage);
        Assert.IsTrue(o);

        var i = BangumiImageHelper.IsBangumiImage(InvalidImage);
        Assert.IsFalse(i);
    }

    [TestMethod]
    public void IsBangumiResizedImage()
    {
        var r = BangumiImageHelper.IsBangumiResizedImage(ResizedImage);
        Assert.IsTrue(r);

        var o = BangumiImageHelper.IsBangumiResizedImage(OriginalImage);
        Assert.IsFalse(o);

        var i = BangumiImageHelper.IsBangumiResizedImage(InvalidImage);
        Assert.IsFalse(i);
    }

    [TestMethod]
    public void GetOriginal()
    {
        var r = BangumiImageHelper.GetOriginalImage(ResizedImage);
        Assert.AreEqual(OriginalImage, r);

        var o = BangumiImageHelper.GetOriginalImage(OriginalImage);
        Assert.AreEqual(OriginalImage, o);

        Assert.Throws<ArgumentException>(() =>
        {
            BangumiImageHelper.GetOriginalImage(InvalidImage);
        });
    }

    [TestMethod]
    public void GetResized()
    {
        var r = BangumiImageHelper.GetResizedImage(ResizedImage, TargetWidth, TargetHeight);
        Assert.AreEqual(ResizedImage, r);

        var o = BangumiImageHelper.GetResizedImage(OriginalImage, TargetWidth, TargetHeight);
        Assert.AreEqual(ResizedImage, o);

        Assert.Throws<ArgumentException>(() =>
        {
            BangumiImageHelper.GetResizedImage(InvalidImage, TargetWidth, TargetHeight);
        });
    }
}

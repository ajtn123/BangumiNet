using BangumiNet.Api.Misc;

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
        var r = BangumiImage.IsBangumiImage(ResizedImage);
        Assert.IsTrue(r);

        var o = BangumiImage.IsBangumiImage(OriginalImage);
        Assert.IsTrue(o);

        var i = BangumiImage.IsBangumiImage(InvalidImage);
        Assert.IsFalse(i);
    }

    [TestMethod]
    public void IsBangumiResizedImage()
    {
        var r = BangumiImage.IsBangumiResizedImage(ResizedImage);
        Assert.IsTrue(r);

        var o = BangumiImage.IsBangumiResizedImage(OriginalImage);
        Assert.IsFalse(o);

        var i = BangumiImage.IsBangumiResizedImage(InvalidImage);
        Assert.IsFalse(i);
    }

    [TestMethod]
    public void GetOriginal()
    {
        var r = BangumiImage.GetOriginalImage(ResizedImage);
        Assert.AreEqual(OriginalImage, r);

        var o = BangumiImage.GetOriginalImage(OriginalImage);
        Assert.AreEqual(OriginalImage, o);

        Assert.Throws<ArgumentException>(() =>
        {
            BangumiImage.GetOriginalImage(InvalidImage);
        });
    }

    [TestMethod]
    public void GetResized()
    {
        var r = BangumiImage.GetResizedImage(ResizedImage, TargetWidth, TargetHeight);
        Assert.AreEqual(ResizedImage, r);

        var o = BangumiImage.GetResizedImage(OriginalImage, TargetWidth, TargetHeight);
        Assert.AreEqual(ResizedImage, o);

        Assert.Throws<ArgumentException>(() =>
        {
            BangumiImage.GetResizedImage(InvalidImage, TargetWidth, TargetHeight);
        });
    }
}

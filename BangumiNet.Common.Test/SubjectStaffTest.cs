using BangumiNet.Common.Attributes;

namespace BangumiNet.Common.Test;

[TestClass]
public sealed class SubjectStaffTest
{
    [TestMethod]
    public void NameCn()
    {
        string[] keys = [
            .. Enum.GetValues<StaffCategory>().Select(x=>x.GetNameCn()),
            .. Enum.GetValues<SubjectStaff>().Select(x=>x.GetNameCn()),
        ];
        foreach (var item in keys)
            Assert.IsNotEmpty(item);
    }

    [TestMethod]
    public void NameEn()
    {
        var keys = Enum.GetValues<StaffCategory>().Select(x => x.GetNameEn());
        foreach (var item in keys)
            Assert.IsNotEmpty(item);
        var keys1 = Enum.GetValues<SubjectStaff>().Select(x => x.GetNameEn());
        Assert.IsNotEmpty(keys1.Where(string.IsNullOrEmpty));
    }

    [TestMethod]
    public void NameJp()
    {
        var keys = Enum.GetValues<SubjectStaff>().Select(x => x.GetNameJp());
        Assert.IsNotEmpty(keys.Where(string.IsNullOrEmpty));
    }

    [TestMethod]
    public void Description()
    {
        var keys = Enum.GetValues<SubjectStaff>().Select(x => x.GetDescription());
        Assert.IsNotEmpty(keys.Where(string.IsNullOrEmpty));
    }

    [TestMethod]
    public void Categories()
    {
        var keys = Enum.GetValues<SubjectStaff>().Select(x => x.GetCategories()).Where(x => x != null);
        Assert.IsNotEmpty(keys);
        foreach (var item in keys)
            Assert.IsNotEmpty(item!);
    }

    [TestMethod]
    public void SubjectTypeT()
    {
        var keys = Enum.GetValues<SubjectStaff>().Select(x => x.GetSubjectType()).Distinct();
        Assert.HasCount(5, keys);
        var keys1 = Enum.GetValues<StaffCategory>().Select(x => x.GetSubjectType()).Distinct();
        Assert.HasCount(2, keys1);
        Assert.Contains(SubjectType.Anime, keys1);
        Assert.Contains(SubjectType.Game, keys1);
    }
}

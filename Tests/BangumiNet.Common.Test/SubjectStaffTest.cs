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
        var keys = Enum.GetValues<SubjectStaff>().Select(x => x.GetCategories());
        Assert.IsNotEmpty(keys);
    }

    [TestMethod]
    public void PresentationGroups()
    {
        var keys = Enum.GetValues<SubjectStaff>().Select(x => x.GetPresentationGroups());
        Assert.IsNotEmpty(keys);
    }

    [TestMethod]
    public void IsFolded()
    {
        var keys = Enum.GetValues<SubjectStaff>().Select(x => x.GetIsFolded());
        Assert.Contains(true, keys);
        Assert.Contains(false, keys);
    }

    [TestMethod]
    public void StaffSubjectType()
    {
        var keys = Enum.GetValues<SubjectStaff>().Select(x => x.GetSubjectType());
        Assert.IsNotEmpty(keys);
    }

    [TestMethod]
    public void StaffCategorySubjectType()
    {
        var keys = Enum.GetValues<StaffCategory>().Select(x => x.GetSubjectType());
        Assert.IsNotEmpty(keys);
    }
}

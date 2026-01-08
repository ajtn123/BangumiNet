using BangumiNet.Common.Attributes;

namespace BangumiNet.Common.Test;

[TestClass]
public sealed class PersonCharacterRelationTest
{
    [TestMethod]
    public void NameCn()
    {
        Assert.AreEqual("恋人", PersonCharacterRelationType.Lover.GetNameCn());
        Assert.AreEqual("CV", PersonCharacterRelationType.CV.GetNameCn());
    }

    [TestMethod]
    public void Description()
    {
        Assert.AreEqual("侍从的主人", PersonCharacterRelationType.Master.GetDescription());
        Assert.IsNull(PersonCharacterRelationType.Superior.GetDescription());
    }

    [TestMethod]
    public void ViceVersa()
    {
        Assert.IsNotNull(PersonCharacterRelationType.Subordinate.GetViceVersa());
        Assert.IsNull(PersonCharacterRelationType.Colleague.GetViceVersa());
        Assert.AreEqual(PersonCharacterRelationType.Adaptation, PersonCharacterRelationType.Prototype.GetViceVersa());
        Assert.AreEqual(PersonCharacterRelationType.Prototype, PersonCharacterRelationType.Adaptation.GetViceVersa());
    }

    [TestMethod]
    public void IsViceVersaSkipped()
    {
        Assert.IsTrue(PersonCharacterRelationType.UnrequitedLove.GetIsViceVersaSkipped());
        Assert.IsFalse(PersonCharacterRelationType.ParentCompany.GetIsViceVersaSkipped());
    }

    [TestMethod]
    public void IsPrimary()
    {
        Assert.IsTrue(PersonCharacterRelationType.Actor.GetIsPrimary());
        Assert.IsFalse(PersonCharacterRelationType.ChineseDub.GetIsPrimary());
    }

    [TestMethod]
    public void Category()
    {
        Assert.AreEqual(PersonCharacterRelationCategory.PersonCharacter, PersonCharacterRelationType.KoreanDub.GetCategory());
        Assert.AreEqual(PersonCharacterRelationCategory.Person, PersonCharacterRelationType.Teacher.GetCategory());
        Assert.AreEqual(PersonCharacterRelationCategory.Character, PersonCharacterRelationType.TeacherC.GetCategory());
    }
}

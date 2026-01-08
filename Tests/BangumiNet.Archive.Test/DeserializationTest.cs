using BangumiNet.Archive.Models;

namespace BangumiNet.Archive.Test;

[TestClass]
[TestCategory("LargeData")]
public sealed class DeserializationTest
{
    private static string GetFileName(string fileName)
    {
        var path = "../../../.archives/" + fileName;
        if (File.Exists(path)) return path;
        path = "../" + path;
        if (File.Exists(path)) return path;
        throw new FileNotFoundException(fileName);
    }

    [TestMethod]
    public async Task PersonCharacterRelations()
    {
        using var str = File.OpenRead(GetFileName("person-characters.jsonlines"));
        var obj = await ArchiveLoader.Load<PersonCharacterRelation>(str, TestContext.CancellationToken).ToArrayAsync(TestContext.CancellationToken);

        Assert.IsPositive(obj.Length);
    }

    [TestMethod]
    public async Task SubjectPersonRelations()
    {
        using var str = File.OpenRead(GetFileName("subject-persons.jsonlines"));
        var obj = await ArchiveLoader.Load<SubjectPersonRelation>(str, TestContext.CancellationToken).ToArrayAsync(TestContext.CancellationToken);

        Assert.IsPositive(obj.Length);
    }

    [TestMethod]
    public async Task SubjectCharacterRelations()
    {
        using var str = File.OpenRead(GetFileName("subject-characters.jsonlines"));
        var obj = await ArchiveLoader.Load<SubjectCharacterRelation>(str, TestContext.CancellationToken).ToArrayAsync(TestContext.CancellationToken);

        Assert.IsPositive(obj.Length);
    }

    [TestMethod]
    public async Task SubjectRelations()
    {
        using var str = File.OpenRead(GetFileName("subject-relations.jsonlines"));
        var obj = await ArchiveLoader.Load<SubjectRelation>(str, TestContext.CancellationToken).ToArrayAsync(TestContext.CancellationToken);

        Assert.IsPositive(obj.Length);
    }

    [TestMethod]
    public async Task Subjects()
    {
        using var str = File.OpenRead(GetFileName("subject.jsonlines"));
        var obj = await ArchiveLoader.Load<Subject>(str, TestContext.CancellationToken).ToArrayAsync(TestContext.CancellationToken);

        Assert.IsPositive(obj.Length);
    }

    [TestMethod]
    public async Task Characters()
    {
        using var str = File.OpenRead(GetFileName("character.jsonlines"));
        var obj = await ArchiveLoader.Load<Character>(str, TestContext.CancellationToken).ToArrayAsync(TestContext.CancellationToken);

        Assert.IsPositive(obj.Length);
    }

    [TestMethod]
    public async Task Persons()
    {
        using var str = File.OpenRead(GetFileName("person.jsonlines"));
        var obj = await ArchiveLoader.Load<Person>(str, TestContext.CancellationToken).ToArrayAsync(TestContext.CancellationToken);

        Assert.IsPositive(obj.Length);
    }

    [TestMethod]
    public async Task Episodes()
    {
        using var str = File.OpenRead(GetFileName("episode.jsonlines"));
        var obj = await ArchiveLoader.Load<Episode>(str, TestContext.CancellationToken).ToArrayAsync(TestContext.CancellationToken);

        Assert.IsPositive(obj.Length);
    }

    public TestContext TestContext { get; set; }
}

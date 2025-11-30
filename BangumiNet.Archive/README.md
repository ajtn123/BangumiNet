# BangumiNet.Archive

一个 [Bangumi Wiki Archive](https://github.com/bangumi-data/bangumi-data) 的 .NET 包装器，来自 [BangumiNet](https://github.com/ajtn123/BangumiNet)。

```
using var fs = File.OpenRead(@".\person-characters.jsonlines");
var obj = await ArchiveLoader.Load<PersonCharacterRelation>(fs).ToArrayAsync();
```

使用 `BangumiNet.Archive.ArchiveLoader.Load<类型>(<JSON Lines 文件 Stream>)` 加载数据。类型位于 `BangumiNet.Archive.Models` 命名空间，对应文件见下表。

| 数据      | 文件                           | 类型                       |
| --------- | ------------------------------ | -------------------------- |
| 条目      | `subject.jsonlines`            | `Subject`                  |
| 人物      | `person.jsonlines`             | `Person`                   |
| 角色      | `character.jsonlines`          | `Character`                |
| 章节      | `episode.jsonlines`            | `Episode`                  |
| 条目-条目 | `subject-relations.jsonlines`  | `SubjectRelation`          |
| 条目-角色 | `subject-characters.jsonlines` | `SubjectCharacterRelation` |
| 条目-人物 | `subject-persons.jsonlines`    | `SubjectPersonRelation`    |
| 人物-角色 | `person-characters.jsonlines`  | `PersonCharacterRelation`  |

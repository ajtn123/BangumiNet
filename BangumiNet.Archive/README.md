# BangumiNet.Archive

一个 [Bangumi Wiki Archive](https://github.com/bangumi/Archive) 的 .NET 包装器，来自 [BangumiNet](https://github.com/ajtn123/BangumiNet)。

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

### NativeAOT 支持

将 [`JsonContext.cs`](https://github.com/ajtn123/BangumiNet/blob/master/BangumiNet.Archive/JsonContext.cs) 中注释的代码添加到你的项目，即可使用 JSON 源生成。
非 NativeAOT 编译不建议使用，会显著增加程序集大小，并且没有性能优势。

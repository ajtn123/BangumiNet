# BangumiNet.BangumiData

一个 [bangumi-data](https://github.com/bangumi-data/bangumi-data) 的 .NET 包装器，来自 [BangumiNet](https://github.com/ajtn123/BangumiNet)。

```
using var client = new HttpClient();
using var stream = await client.GetStreamAsync("https://unpkg.com/bangumi-data@0.3/dist/data.json");
var obj = await BangumiDataLoader.LoadAsync(stream);
```

### NativeAOT 支持

将 [`JsonContext.cs`](https://github.com/ajtn123/BangumiNet/blob/master/BangumiNet.BangumiData/JsonContext.cs) 中注释的代码添加到你的项目，即可使用 JSON 源生成。
非 NativeAOT 编译不建议使用，会显著增加程序集大小，并且没有性能优势。

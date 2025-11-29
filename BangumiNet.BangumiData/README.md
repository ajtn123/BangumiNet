# BangumiNet.BangumiData

一个 [bangumi-data](https://github.com/bangumi-data/bangumi-data) 的 .NET 包装器，来自 [BangumiNet](https://github.com/ajtn123/BangumiNet)。

```
using var client = new HttpClient();
using var stream = await client.GetStreamAsync("https://unpkg.com/bangumi-data@0.3/dist/data.json");
var obj = await BangumiDataLoader.LoadAsync(stream);
```

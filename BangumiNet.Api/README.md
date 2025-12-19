# BangumiNet.Api

一个由 [Kiota](https://learn.microsoft.com/zh-cn/openapi/kiota/overview) 根据 [Bangumi](https://bgm.tv) 提供的 OpenAPI 定义生成的 API 客户端，来自 [BangumiNet](https://github.com/ajtn123/BangumiNet)。

- Legacy API [//api.bgm.tv/](https://bangumi.github.io/api/#/%E6%9D%A1%E7%9B%AE/getCalendar)
- Open API [//api.bgm.tv/v0/](https://bangumi.github.io/api/#/)
- Private API [//next.bgm.tv/p1/](https://next.bgm.tv/p1/#/)
  - socket.io 订阅通知
  - 时间线事件流 (SSE)

## 使用

### 构建 Client

```csharp
// Access Token 或 chiiNextSessionID Cookie
var authProvider = new BangumiAuthenticationProvider(Token);

// 如无需登录，使用 AnonymousAuthenticationProvider
// var authProvider = new AnonymousAuthenticationProvider();

// 尽可能复用已有的 HttpClient，User-Agent 符合 bangumi 要求
var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", UserAgent);

var requestAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
var client = new BangumiNet.Api.P1.ApiClient(requestAdapter);

// 不要使用同一个 RequestAdapter
// AuthenticationProvider 和 HttpClient 可以复用
var requestAdapterV0 = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
var client = new BangumiNet.Api.V0.ApiClient(requestAdapterV0);
```

Token 可以来自:
- 网页（持久）：<https://next.bgm.tv/demo/access-token>
- Private API（临时）：[https://next.bgm.tv/p1/login](https://next.bgm.tv/p1/#/auth/login)
- [OAuth 2.0](https://github.com/bangumi/api/blob/master/docs-raw/How-to-Auth.md)

关于 User-Agent 的要求，见[此处](https://github.com/bangumi/api/blob/master/docs-raw/user%20agent.md)。

### 发起请求

API 文档见顶部链接。

```csharp
var response = await client.P1.Calendar.GetAsync();
```

```csharp
int id = 9912;
var response = await client.P1.Subject[id].GetAsync();

var type = (SubjectType?)response.Type;
```

```csharp
int id = 77560;
var response = await client.V0.Users.Minus.Collections.Minus.Episodes[id].PutAsync(new()
{
    Type = (int)EpisodeCollectionType.Done
});
```

#### 时间线事件流 (SSE)

```csharp
CancellationTokenSource cts = new();

var events = new BangumiNet.Api.Misc.TimelineEventStream(httpClient, token);
List<Timeline> items = [];
await foreach (var item in events.StartAsync(FilterMode.All, null, cts.Token))
{
    items.Add(item);
}

// 请一定要在某处调用，否则连接将永远不会断开
cts.Cancel();
```

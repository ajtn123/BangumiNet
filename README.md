# BangumiNet

![license](https://img.shields.io/github/license/ajtn123/BangumiNet)
![release](https://img.shields.io/github/release/ajtn123/BangumiNet)
![framework](https://img.shields.io/badge/.NET-10-blue)
![framework](https://img.shields.io/badge/Avalonia-11-blue)

## 安装

### 发布版

- 安装 [.NET 运行时 10](https://dotnet.microsoft.com/zh-cn/download/dotnet/10.0)。

> 如已有 .NET 桌面运行时 / SDK 则无需额外安装。

- 下载 [Release](https://github.com/ajtn123/BangumiNet/releases) 并解压到任何位置，最好无需管理员权限以写入文件。
- 运行可执行文件即可使用。

### 自行编译

- 安装 [.NET SDK 10](https://dotnet.microsoft.com/zh-cn/download/dotnet/10.0)、[git](https://git-scm.com/)。
- 在终端运行以下命令。

```
git clone https://github.com/ajtn123/BangumiNet.git
cd ./BangumiNet/BangumiNet
dotnet publish --configuration Release --runtime <RID>
```

> 参见 [RID](https://learn.microsoft.com/zh-cn/dotnet/core/rid-catalog)、[dotnet publish](https://learn.microsoft.com/zh-cn/dotnet/core/tools/dotnet-publish)。

## BangumiNet.Api

本项目使用由 [Kiota](https://learn.microsoft.com/zh-cn/openapi/kiota/overview) 根据 [Bangumi](https://bgm.tv) 提供的 [OpenAPI](https://www.openapis.org/) 定义生成的 API 客户端。

- [//api.bgm.tv/](https://bangumi.github.io/api/#/)
- [//api.bgm.tv/v0/](https://bangumi.github.io/api/#/)
- [//next.bgm.tv/p1/](https://next.bgm.tv/p1/#/)

## 鸣谢

- [Bangumi](https://bgm.tv)
- [Bangumi 开源项目](https://github.com/bangumi)
- [.NET](https://dotnet.microsoft.com/zh-cn/)
- [Avalonia](https://avaloniaui.net/)
- [Kiota](https://learn.microsoft.com/zh-cn/openapi/kiota/overview)
- 以及所有引用的依赖、资产与服务。

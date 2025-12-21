# BangumiNet

[![license](https://img.shields.io/github/license/ajtn123/BangumiNet)](https://github.com/ajtn123/BangumiNet/blob/master/LICENSE.txt)
[![release](https://img.shields.io/github/release/ajtn123/BangumiNet)](https://github.com/ajtn123/BangumiNet/releases)
![code size](https://img.shields.io/github/languages/code-size/ajtn123/BangumiNet)
[![framework](https://img.shields.io/badge/.NET-10-blue)](https://dotnet.microsoft.com/zh-cn/)
[![framework](https://img.shields.io/badge/Avalonia-11-blue)](https://avaloniaui.net/)
[![ci](https://github.com/ajtn123/BangumiNet/actions/workflows/build.yml/badge.svg)](https://github.com/ajtn123/BangumiNet/actions/workflows/build.yml)

BangumiNet 是一个基于 .NET 与 Avalonia 开发的第三方 [Bangumi 番组计划](https://bgm.tv/) 桌面客户端，支持 Windows、Linux 和 MacOS。

软件使用 MIT 开源协议，所有源代码均可在此仓库获取。如果在其他位置查看本 README，[点击这里](https://github.com/ajtn123/BangumiNet)前往 GitHub 仓库。

## 预览

<img width="3828" height="2035" alt="屏幕截图" src="https://github.com/user-attachments/assets/006bc927-8708-491a-bf80-205a745a1a74" />

## 安装

### 发布版

- 安装 [.NET 运行时 10](https://dotnet.microsoft.com/zh-cn/download/dotnet/10.0)。

> 如已有 .NET 桌面运行时 / SDK 则无需额外安装。

- 下载 [Release](https://github.com/ajtn123/BangumiNet/releases) 并解压到任何位置。
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

## NuGet

BangumiNet 将部分工具项目发布在 [NuGet](https://www.nuget.org/packages?q=BangumiNet)。

[![BangumiNet.Api 版本](https://img.shields.io/nuget/v/BangumiNet.Api?label=BangumiNet.Api)](https://www.nuget.org/packages/BangumiNet.Api)
[![BangumiNet.Archive 版本](https://img.shields.io/nuget/v/BangumiNet.Archive?label=BangumiNet.Archive)](https://www.nuget.org/packages/BangumiNet.Archive)
[![BangumiNet.BangumiData 版本](https://img.shields.io/nuget/v/BangumiNet.BangumiData?label=BangumiNet.BangumiData)](https://www.nuget.org/packages/BangumiNet.BangumiData)
[![BangumiNet.Common 版本](https://img.shields.io/nuget/v/BangumiNet.Common?label=BangumiNet.Common)](https://www.nuget.org/packages/BangumiNet.Common)

## 鸣谢

- [Bangumi](https://bgm.tv)
- [Bangumi 开源项目](https://github.com/bangumi)
- [Bangumi Data](https://github.com/bangumi-data/bangumi-data)
- [.NET](https://dotnet.microsoft.com/zh-cn/)
- [Avalonia](https://avaloniaui.net/)
- [Kiota](https://learn.microsoft.com/zh-cn/openapi/kiota/overview)
- 以及所有引用的依赖、资产与服务。

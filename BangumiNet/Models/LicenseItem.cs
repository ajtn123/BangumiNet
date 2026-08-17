namespace BangumiNet.Models;

/// <summary>许可证信息，从 NuGet 包（nupkg）的 nuspec 元数据中读取。</summary>
public sealed record LicenseItem(
    string Name,
    string? Version,
    string? License,
    string? LicenseUrl,
    string? RepositoryUrl,
    string? Authors,
    string? LicenseText);

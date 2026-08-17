param(
    # Path to obj/project.assets.json (lists all referenced packages with versions)
    [Parameter(Mandatory = $true)]
    [string]$AssetsJson,

    # NuGet global packages folder (optional; defaults to NUGET_PACKAGES or ~/.nuget/packages)
    [Parameter(Mandatory = $true)]
    [string]$NuGetRoot,

    # Output JSON file path (embedded into the app at build time)
    [Parameter(Mandatory = $true)]
    [string]$Output
)

# 从 project.assets.json + NuGet 缓存(.nuspec)中提取所有引用包的许可证信息，
# 生成 JSON 嵌入程序集，运行时不依赖用户机器上的 NuGet 缓存。

if (-not (Test-Path $AssetsJson)) {
    Write-Output "GenerateLicenses: assets.json not found: $AssetsJson"
    exit 0
}

if (-not (Test-Path $NuGetRoot)) {
    Write-Output "GenerateLicenses: NuGet root not found: $NuGetRoot"
    exit 0
}

$assets = Get-Content $AssetsJson -Raw | ConvertFrom-Json
$items = @()

foreach ($prop in $assets.libraries.PSObject.Properties) {
    $lib = $prop.Name
    $info = $prop.Value
    if ($info.type -ne 'package') { continue }

    $parts = $lib -split '/'
    if ($parts.Count -lt 2) { continue }
    $id = $parts[0]
    $ver = $parts[1]

    $dir = Join-Path $NuGetRoot ($id.ToLowerInvariant())
    $verDir = Join-Path $dir $ver
    if (-not (Test-Path $verDir)) {
        $latest = Get-ChildItem $dir -Directory -ErrorAction SilentlyContinue |
            Sort-Object Name | Select-Object -Last 1
        if ($latest) { $verDir = $latest.FullName } else { continue }
    }

    $nuspec = Get-ChildItem $verDir -Filter *.nuspec -ErrorAction SilentlyContinue | Select-Object -First 1
    if (-not $nuspec) { continue }

    $item = [ordered]@{
        Name          = $id
        Type          = '依赖'
        Version       = $ver
        License       = $null
        LicenseUrl    = $null
        RepositoryUrl = $null
        Authors       = $null
        LicenseText   = $null
    }

    try {
        [xml]$x = Get-Content $nuspec.FullName -Raw
        $md = $x.package.metadata

        $lic = $md.license
        $type = if ($lic) { $lic.type } else { '' }
        $expr = if ($lic) { ([string]$lic.'#text').Trim() } else { '' }

        if ($type -eq 'file' -and $expr) {
            $licPath = Join-Path $verDir $expr
            if (Test-Path $licPath) {
                $item.LicenseText = [System.IO.File]::ReadAllText($licPath)
                $item.License = (Split-Path $expr -Leaf)
            }
        }
        elseif ($expr) {
            $item.License = $expr
        }

        if ($expr -and $type -eq 'expression') {
            $item.LicenseUrl = "https://licenses.nuget.org/$expr"
        }
        elseif (-not $expr -and $md.licenseUrl -and $md.licenseUrl -ne 'https://aka.ms/deprecateLicenseUrl') {
            $item.LicenseUrl = $md.licenseUrl
        }
        if ($md.repository.url) { 
            $item.RepositoryUrl = $md.repository.url 
        }
        if ($md.authors) { 
            $item.Authors = ([string]$md.authors).Trim() 
        }
    }
    catch { }

    $items += [pscustomobject]$item
}

$json = $items | Sort-Object Name | ConvertTo-Json -Depth 4
New-Item -Path $Output -ItemType File -Force -Value $json | Out-Null

Write-Output "GenerateLicenses: wrote $($items.Count) packages -> $Output"

param (
    # Path to directory where kiota generated C# code.
    [Parameter(Mandatory=$true)]
    [string]$apiDescriptionFile
)

$api = [System.IO.Path]::GetFileNameWithoutExtension($apiDescriptionFile).ToUpper()
$content = Get-Content $apiDescriptionFile -Raw

# mtigate kiota error
$content = ($content -replace 'exclusiveMinimum\s*:\s*(\d+)', 'minimum: $1')
$content = ($content -replace '"exclusiveMinimum"\s*:\s*(\d+)', '"minimum": $1')

# get version string
if ($api -eq 'P1') {
    $obj = $content | ConvertFrom-Json
    $version = $obj.info.version
} elseif ($api -eq 'V0') {
    $url = 'https://api.github.com/repos/bangumi/api/commits?path=open-api/v0.yaml'
    $response = Invoke-RestMethod -Uri $url -Method Get -Headers @{ 'User-Agent' = 'pwsh' }
    if ($response) {
        $commit = $response[0]
        $date = $commit.commit.author.date.ToString('yyyy-MM-dd')
        $sha = $commit.sha.Substring(0, 7)
        $version = "$date-$sha"
    }
}

if ($version) {
    "API_VERSION=${version}" >> $env:GITHUB_ENV

    $client_extension_path = 'BangumiNet.Api/Extensions/ClientExtensions.cs'
    $client_extension = Get-Content $client_extension_path -Raw
    $client_extension = ($client_extension -replace "public const string Version = `".+`"; // $api", "public const string Version = `"$version`"; // $api")
    Set-Content -Path $client_extension_path -Value $client_extension -NoNewline
}

Set-Content -Path $apiDescriptionFile -Value $content -NoNewline

param (
    # Path to directory where kiota generated C# code.
    [Parameter(Mandatory=$true)]
    [string]$apiDescriptionFile
)

$content = Get-Content $apiDescriptionFile -Raw

# mtigate kiota error
$content = ($content -replace 'exclusiveMinimum', 'minimum')

# get version string
if ($content -match 'version:\s*(.+)') {
    $version = $Matches[1]
    $api = [System.IO.Path]::GetFileNameWithoutExtension($apiDescriptionFile).ToUpper()
    "${api}_VERSION=${version}" >> $env:GITHUB_ENV

    $client_extension_path = 'BangumiNet.Api/Extensions/ClientExtensions.cs'
    $client_extension = Get-Content $client_extension_path -Raw
    if ($api -eq 'PRIVATE') {
        $client_extension = ($client_extension -replace 'public const string Version = ".+"; // P1', "public const string Version = `"$version`"; // P1")
        Set-Content -Path $client_extension_path -Value $client_extension -NoNewline
    }
}

Set-Content -Path $apiDescriptionFile -Value $content -NoNewline

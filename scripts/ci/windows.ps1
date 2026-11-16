[CmdletBinding()]
param(
    [string]$CoreArchive = '',

    [string]$WorkspaceConfig = '',

    [string]$OutputDir = ''
)

$ErrorActionPreference = 'Stop'
$scriptsRoot = Split-Path -Parent $PSScriptRoot
. (Join-Path $scriptsRoot 'Workspace.ps1')

if ($CoreArchive) {
    & (Join-Path $PSScriptRoot 'fetch-core.ps1') `
        -Platform windows `
        -Archive $CoreArchive `
        -WorkspaceConfig $WorkspaceConfig | Out-Host
}

# The agent has neither the generated inputs nor a certificate. Both are
# supplied by the caller: inputs by the core archive above, signing by the
# release host after this job hands over its output.
$buildArgs = @{
    Configuration        = 'Release'
    SkipStaleInputChecks = $true
    SkipSigning          = $true
}
if ($WorkspaceConfig) { $buildArgs['WorkspaceConfig'] = $WorkspaceConfig }
& (Join-Path $scriptsRoot 'BuildBetterEndfieldNext.ps1') @buildArgs

$installerArgs = @{
    Configuration = 'Release'
    SkipSigning   = $true
}
if ($WorkspaceConfig) { $installerArgs['WorkspaceConfig'] = $WorkspaceConfig }
if ($OutputDir) { $installerArgs['OutputDir'] = $OutputDir }
& (Join-Path $scriptsRoot 'BuildInstaller.ps1') @installerArgs

$ws = Get-BEWorkspace -Config $WorkspaceConfig
$version = ([xml](Get-Content -LiteralPath (Join-Path $ws.repo_root 'Directory.Build.props') -Raw)).
    SelectSingleNode('//Version').InnerText.Trim()
$releaseDir = if ($OutputDir) { [System.IO.Path]::GetFullPath($OutputDir) } else {
    Get-BEReleaseDirectory -Workspace $ws -Version $version
}
$installer = Join-Path $releaseDir "BetterEndfieldNext-$version-Setup.exe"
if (-not (Test-Path -LiteralPath $installer -PathType Leaf)) {
    throw "Installer was not produced: $installer"
}

[ordered]@{
    version   = $version
    installer = $installer
    bytes     = (Get-Item -LiteralPath $installer).Length
    signed    = $false
} | ConvertTo-Json -Depth 4

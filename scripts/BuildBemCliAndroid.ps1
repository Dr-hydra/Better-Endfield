[CmdletBinding()]
param(
    [string]$Destination = "",
    [string]$WorkspaceConfig = "",
    [string]$Wheels = "",
    [switch]$SkipVerify
)
$ErrorActionPreference = "Stop"
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
Set-BEWorkspaceEnvironment $ws
$repo = $ws.repo_root
$python = $ws.tools.python
if (-not (Get-Command $python -ErrorAction SilentlyContinue)) {
    throw "Configured Python was not found: $python"
}
if ([string]::IsNullOrWhiteSpace($Destination)) {
    $Destination = Join-Path $ws.paths.build "tools\bem-android"
}
$Destination = [System.IO.Path]::GetFullPath($Destination)
$staging = Join-Path $Destination 'staging'
$archive = Join-Path $Destination 'dist\BEM-Tools-android-arm64.tar.gz'

# Prebuilt wheels ride along so `install.sh` can drop zstandard into site-packages
# without a compiler. Rebuild them with tools/CustomModel/android/build_zstandard_wheel.py.
if ([string]::IsNullOrWhiteSpace($Wheels)) {
    $defaultWheels = Join-Path $repo 'tools\CustomModel\android\wheels'
    if (Test-Path -LiteralPath $defaultWheels -PathType Container) {
        $Wheels = $defaultWheels
    }
}

# The Android build ships the same Python sources as the Windows converter minus
# source_archive.py (no 7-Zip backend on the device) and package_toolchain.py.
# Only zip and already-unpacked directories are supported sources.
$packageArgs = @(
    (Join-Path $repo 'tools/CustomModel/android/package_android.py'),
    '--repo', $repo,
    '--staging', $staging,
    '--output', $archive,
    '--python', $python
)
if ($Wheels) {
    $Wheels = [System.IO.Path]::GetFullPath($Wheels)
    if (-not (Test-Path -LiteralPath $Wheels -PathType Container)) {
        throw "Wheel directory not found: $Wheels"
    }
    $packageArgs += @('--wheels', $Wheels)
}
if ($SkipVerify) { $packageArgs += '--skip-verify' }

$packageOutput = & $python @packageArgs
if ($LASTEXITCODE -ne 0) { throw 'Android CLI packaging failed.' }
$info = ($packageOutput -join "`n") | ConvertFrom-Json

$releaseDir = Get-BEReleaseDirectory -Workspace $ws
New-Item -ItemType Directory -Path $releaseDir -Force | Out-Null
$releaseArchive = Join-Path $releaseDir "BEM-Tools-$($info.version)-android-arm64.tar.gz"
if ([System.IO.Path]::GetFullPath($archive) -ne [System.IO.Path]::GetFullPath($releaseArchive)) {
    Copy-Item -LiteralPath $archive -Destination $releaseArchive -Force
}

$sizeMb = [math]::Round($info.package_bytes / 1MB, 2)
$rawMb = [math]::Round($info.raw_bytes / 1MB, 2)
Write-Host "BEM Android CLI: $releaseArchive"
Write-Host ("  version {0} / {1} modules / {2} files" -f $info.version, $info.modules, $info.files)
Write-Host ("  packed {0} MB, unpacked {1} MB" -f $sizeMb, $rawMb)
Write-Host ("  sha256 {0}" -f $info.sha256)
Write-Host "  install on device: tar -xzf <archive> && sh BEM-Tools-$($info.version)-android-arm64/install.sh"

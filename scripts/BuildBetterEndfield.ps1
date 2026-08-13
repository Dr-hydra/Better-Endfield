[CmdletBinding()]
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release",

    [string]$PublishDir = ""
)

$ErrorActionPreference = "Stop"
$repoRoot = Split-Path -Parent $PSScriptRoot
$nativeRoot = Join-Path $repoRoot "tools\IL2CPP-Dumper-src"
$uiProject = Join-Path $repoRoot "src\BetterEndfield.UI\BetterEndfield.UI.csproj"
$publishDir = if ([string]::IsNullOrWhiteSpace($PublishDir)) {
    Join-Path $repoRoot "artifacts\BetterEndfield-win-x64"
}
else {
    [System.IO.Path]::GetFullPath($PublishDir)
}
$vswhere = Join-Path ${env:ProgramFiles(x86)} `
    "Microsoft Visual Studio\Installer\vswhere.exe"
$runtimeMapCompiler = Join-Path $repoRoot "scripts\CompileVoiceRuntimeMap.py"
$runtimeMapManifest = Join-Path $repoRoot "manifests\voice-event-media-manifest.json"

if (-not (Test-Path -LiteralPath $runtimeMapManifest)) {
    throw "Voice manifest was not found. Run scripts\UpdateResourceManifests.ps1 first."
}

& py -3 $runtimeMapCompiler --input $runtimeMapManifest
if ($LASTEXITCODE -ne 0) {
    throw "Voice runtime map compilation failed with exit code $LASTEXITCODE."
}

if (-not (Test-Path -LiteralPath $vswhere)) {
    throw "vswhere.exe was not found. Install Visual Studio 2022 Build Tools with C++."
}

$msbuild = & $vswhere -latest -products * `
    -requires Microsoft.Component.MSBuild `
    -find "MSBuild\**\Bin\MSBuild.exe" |
    Select-Object -First 1
if (-not $msbuild) {
    throw "MSBuild was not found."
}

& $msbuild (Join-Path $nativeRoot "Dump.sln") /m /t:Build `
    "/p:Configuration=$Configuration" /p:Platform=x64 /v:minimal
if ($LASTEXITCODE -ne 0) {
    throw "Native build failed with exit code $LASTEXITCODE."
}

dotnet publish $uiProject -c $Configuration -r win-x64 `
    --self-contained true -p:Platform=x64 -p:PublishSingleFile=true `
    -p:DebugType=None -p:DebugSymbols=false -p:PublishDir="$publishDir\"
if ($LASTEXITCODE -ne 0) {
    throw "WinUI publish failed with exit code $LASTEXITCODE."
}

$mapperPath = Join-Path $nativeRoot "x64\$Configuration\Il2cppDumper.exe"
Copy-Item -LiteralPath $mapperPath -Destination $publishDir -Force

Write-Host ""
Write-Host "Build complete: $publishDir"
Write-Host "Run BetterEndfield.exe, select Endfield.exe, then save and launch."

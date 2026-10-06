[CmdletBinding()]
param(
    [string]$OutputDirectory,
    [switch]$Build,
    [string]$WindowsLibrary,
    [string]$AndroidLibrary,
    [string]$CMake,
    [string]$AndroidNdk,
    [string]$Ninja,
    [string]$WorkspaceConfig = ''
)

$ErrorActionPreference = 'Stop'
$repoRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
. (Join-Path $repoRoot 'scripts/Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
Set-BEWorkspaceEnvironment $ws
$sdkVersion = '1.0.0'
$propsPath = Join-Path $ws.repo_root 'Directory.Build.props'
if (!(Test-Path -LiteralPath $propsPath -PathType Leaf)) {
    throw "Directory.Build.props not found: $propsPath"
}
$props = [xml](Get-Content -LiteralPath $propsPath -Raw)
$applicationVersionNode = $props.SelectSingleNode('//Version')
if ($null -eq $applicationVersionNode -or [string]::IsNullOrWhiteSpace($applicationVersionNode.InnerText)) {
    throw 'Directory.Build.props does not define <Version>.'
}
$applicationVersion = $applicationVersionNode.InnerText.Trim()
if ($applicationVersion -notmatch '^\d+\.\d+\.\d+(?:[-+][A-Za-z0-9.-]+)?$') {
    throw "Invalid application version in Directory.Build.props: $applicationVersion"
}
$echoRoot = Join-Path $PSScriptRoot 'echo'
if (!$OutputDirectory) { $OutputDirectory = Get-BEReleaseDirectory -Workspace $ws }
$outputRoot = [System.IO.Path]::GetFullPath($OutputDirectory)
if (!$CMake) { $CMake = $ws.tools.cmake }
if (!$WindowsLibrary) { $WindowsLibrary = Join-Path $ws.paths.build 'third-party/echo/windows/package/native/windows-x64/example.echo.dll' }
if (!$AndroidLibrary) { $AndroidLibrary = Join-Path $ws.paths.build 'third-party/echo/android/package/native/android-arm64/libexample.echo.so' }
if (!$AndroidNdk) { $AndroidNdk = Join-Path $ws.tools.android_sdk 'ndk/27.2.12479018' }
if (!$Ninja) { $Ninja = Join-Path $ws.tools.android_sdk 'cmake/3.22.1/bin/ninja.exe' }

function Invoke-CMake([string[]]$Arguments) {
    & $CMake @Arguments
    if ($LASTEXITCODE -ne 0) { throw "CMake failed with exit code $LASTEXITCODE" }
}
if ($Build) {
    $winBuild = Join-Path $ws.paths.build 'third-party/echo/windows'
    $androidBuild = Join-Path $ws.paths.build 'third-party/echo/android'
    $toolchain = Join-Path $AndroidNdk 'build/cmake/android.toolchain.cmake'
    if (!(Test-Path -LiteralPath $toolchain -PathType Leaf) -or !(Test-Path -LiteralPath $Ninja -PathType Leaf)) {
        throw 'Android toolchain missing. Set -AndroidNdk and -Ninja to installed NDK/Ninja paths.'
    }
    Invoke-CMake -Arguments @('-S', $echoRoot, '-B', $winBuild, '-A', 'x64')
    Invoke-CMake -Arguments @('--build', $winBuild, '--config', 'Release')
    Invoke-CMake -Arguments @('-S', $echoRoot, '-B', $androidBuild, '-G', 'Ninja',
        '-DANDROID_ABI=arm64-v8a', '-DANDROID_PLATFORM=android-29', '-DANDROID_STL=c++_static',
        '-DCMAKE_BUILD_TYPE=Release', "-DCMAKE_TOOLCHAIN_FILE=$toolchain", "-DCMAKE_MAKE_PROGRAM=$Ninja")
    Invoke-CMake -Arguments @('--build', $androidBuild, '--config', 'Release')
}
foreach ($library in @($WindowsLibrary, $AndroidLibrary)) {
    if (!(Test-Path -LiteralPath $library -PathType Leaf)) { throw "Native library missing: $library. Run this script with -Build or supply prebuilt library paths." }
}

# Explicit allowlist avoids copying build caches, developer configuration or private indices.
function Copy-File([string]$Source, [string]$Destination) {
    if (!(Test-Path -LiteralPath $Source -PathType Leaf)) { throw "Required SDK file missing: $Source" }
    [System.IO.Directory]::CreateDirectory([System.IO.Path]::GetDirectoryName($Destination)) | Out-Null
    [System.IO.File]::Copy([System.IO.Path]::GetFullPath($Source), $Destination, $true)
}
function Write-Utf8([string]$Destination, [string]$Value) {
    [System.IO.File]::WriteAllText($Destination, $Value, [System.Text.UTF8Encoding]::new($false))
}
Add-Type -AssemblyName System.IO.Compression
Add-Type -AssemblyName System.IO.Compression.FileSystem
function Zip-Directory([string]$Source, [string]$Destination) {
    $stream = [System.IO.File]::Open($Destination, [System.IO.FileMode]::Create)
    $archive = [System.IO.Compression.ZipArchive]::new($stream, [System.IO.Compression.ZipArchiveMode]::Create, $false)
    try {
        $prefix = [System.IO.Path]::GetFullPath($Source).TrimEnd('\', '/') + [System.IO.Path]::DirectorySeparatorChar
        $files = @(Get-ChildItem -LiteralPath $Source -Recurse -File | Sort-Object FullName)
        foreach ($file in $files) {
            $relative = $file.FullName.Substring($prefix.Length).Replace('\', '/')
            $entry = $archive.CreateEntry($relative, [System.IO.Compression.CompressionLevel]::Optimal)
            $entry.LastWriteTime = [System.DateTimeOffset]::new(2000, 1, 1, 0, 0, 0, [TimeSpan]::Zero)
            $inputStream = [System.IO.File]::OpenRead($file.FullName)
            $entryStream = $entry.Open()
            try { $inputStream.CopyTo($entryStream) } finally { $entryStream.Dispose(); $inputStream.Dispose() }
        }
    } finally { $archive.Dispose(); $stream.Dispose() }
}
function Test-Package([string]$Directory) {
    $manifest = Get-Content -LiteralPath (Join-Path $Directory 'module.json') -Raw | ConvertFrom-Json
    if ($manifest.format -ne 1 -or $manifest.abi -ne 1 -or $manifest.id -ne 'example.echo') { throw 'Unexpected Echo manifest identity/format.' }
    $paths = @($manifest.libraries.'windows-x64', $manifest.libraries.'android-arm64', $manifest.ui)
    foreach ($relative in $paths) {
        if (!$relative -or $relative.StartsWith('/') -or $relative.Contains('\') -or $relative.Contains(':') -or $relative.Split('/') -contains '..') { throw "Invalid package path: $relative" }
        if (!(Test-Path -LiteralPath (Join-Path $Directory $relative) -PathType Leaf)) { throw "Package entry missing: $relative" }
    }
    $winBytes = [System.IO.File]::ReadAllBytes((Join-Path $Directory $manifest.libraries.'windows-x64'))
    if ($winBytes.Length -lt 64 -or $winBytes[0] -ne 0x4d -or $winBytes[1] -ne 0x5a) { throw 'Windows library is not a PE image.' }
    $peOffset = [BitConverter]::ToInt32($winBytes, 0x3c)
    if ($peOffset -lt 0 -or $peOffset + 6 -gt $winBytes.Length -or [BitConverter]::ToUInt32($winBytes, $peOffset) -ne 0x4550 -or [BitConverter]::ToUInt16($winBytes, $peOffset + 4) -ne 0x8664) { throw 'Windows library is not an x64 PE image.' }
    $androidBytes = [System.IO.File]::ReadAllBytes((Join-Path $Directory $manifest.libraries.'android-arm64'))
    if ($androidBytes.Length -lt 64 -or $androidBytes[0] -ne 0x7f -or $androidBytes[1] -ne 0x45 -or $androidBytes[2] -ne 0x4c -or $androidBytes[3] -ne 0x46 -or $androidBytes[4] -ne 2 -or $androidBytes[5] -ne 1 -or [BitConverter]::ToUInt16($androidBytes, 18) -ne 183) { throw 'Android library is not a little-endian arm64 ELF64 image.' }
    foreach ($bytes in @($winBytes, $androidBytes)) {
        if (![System.Text.Encoding]::ASCII.GetString($bytes).Contains('BetterEndfield_GetThirdPartyModuleV1')) { throw 'Required native entry symbol is missing.' }
    }
    return $manifest
}

[System.IO.Directory]::CreateDirectory($outputRoot) | Out-Null
$stageRoot = Join-Path $outputRoot ('.stage-' + [Guid]::NewGuid().ToString('N'))
[System.IO.Directory]::CreateDirectory($stageRoot) | Out-Null
try {
    $packageRoot = Join-Path $stageRoot 'echo-package'
    [System.IO.Directory]::CreateDirectory($packageRoot) | Out-Null
    foreach ($relative in @('module.json', 'ui/index.html', 'ui/style.css', 'ui/app.js')) {
        Copy-File (Join-Path $echoRoot $relative) (Join-Path $packageRoot $relative)
    }
    Copy-File $WindowsLibrary (Join-Path $packageRoot 'native/windows-x64/example.echo.dll')
    Copy-File $AndroidLibrary (Join-Path $packageRoot 'native/android-arm64/libexample.echo.so')
    $manifest = Test-Package $packageRoot
    $echoName = "BetterEndfield-Echo-$($manifest.version)-Dual.zip"
    $echoZip = Join-Path $outputRoot $echoName
    Zip-Directory $packageRoot $echoZip

    $sdkRoot = Join-Path $stageRoot 'sdk'
    foreach ($header in @('ModuleApi.h', 'HookChain.h', 'ThirdPartyModule.h')) {
        Copy-File (Join-Path $repoRoot "native/shared/include/BetterEndfield/$header") (Join-Path $sdkRoot "include/BetterEndfield/$header")
    }
    Copy-File (Join-Path $repoRoot 'docs/host/THIRD_PARTY_MODULE_CREATOR_GUIDE.md') (Join-Path $sdkRoot 'docs/host/THIRD_PARTY_MODULE_CREATOR_GUIDE.md')
    foreach ($relative in @('CMakeLists.txt', 'module.json', 'native/echo.cpp', 'ui/index.html', 'ui/style.css', 'ui/app.js')) {
        Copy-File (Join-Path $echoRoot $relative) (Join-Path $sdkRoot "examples/echo/$relative")
    }
    Copy-File $WindowsLibrary (Join-Path $sdkRoot 'examples/echo/native/windows-x64/example.echo.dll')
    Copy-File $AndroidLibrary (Join-Path $sdkRoot 'examples/echo/native/android-arm64/libexample.echo.so')
    Copy-File $echoZip (Join-Path $sdkRoot "packages/$echoName")
    Write-Utf8 (Join-Path $sdkRoot 'README.md') @'
# Better Endfield Third-Party Module SDK 1.0.0

Target: Better Endfield __APPLICATION_VERSION__, package format 1, native ABI 1.

- `docs/host/THIRD_PARTY_MODULE_CREATOR_GUIDE.md`: package, lifecycle, configuration, UI bridge, shared Hook contract and build guide.
- `include/BetterEndfield/`: all three public headers required by `ThirdPartyModule.h`.
- `examples/echo/`: complete portable CMake/C++20 source and static HTML UI, plus both prebuilt native libraries.
- `packages/BetterEndfield-Echo-1.0.0-Dual.zip`: import this ZIP into the application's Third-Party Modules page on either platform, then enable it.

From this SDK directory on Windows with CMake and Visual Studio C++ Build Tools:

```powershell
cmake -S examples/echo -B build/echo-windows -A x64
cmake --build build/echo-windows --config Release
```

Android arm64: configure the same project with the NDK CMake toolchain, Ninja, `ANDROID_ABI=arm64-v8a`, `ANDROID_PLATFORM=android-29`, `ANDROID_STL=c++_static`, `CMAKE_BUILD_TYPE=Release`. See the guide for exact commands.

Each CMake build emits a **single-platform** `build/echo-*/package/`. A dual-platform ZIP must use the original dual `examples/echo/module.json` and include both native libraries and current UI files. ZIP the contents, with `module.json` at the archive root.

Native callbacks execute on a Host worker, not the game main thread. Game symbols and version adaptation belong to the author. Shared Hook `next` forwards to the next registered node; it is not a guarantee to bypass all other modules. Legacy builtin hooks remain exclusive unless migrated explicitly. Loaded libraries and old generations remain resident; binary upgrades need a game restart.
'@
    $sdkReadme = Join-Path $sdkRoot 'README.md'
    $sdkReadmeText = (Get-Content -LiteralPath $sdkReadme -Raw).Replace('__APPLICATION_VERSION__', $applicationVersion)
    Write-Utf8 $sdkReadme $sdkReadmeText
    $sdkZip = Join-Path $outputRoot "BetterEndfield-ThirdPartySDK-$sdkVersion.zip"
    Zip-Directory $sdkRoot $sdkZip
    $metadata = [ordered]@{
        sdk_version = $sdkVersion
        application_version = $applicationVersion
        package_format = 1
        native_abi = 1
        module_id = $manifest.id
        module_version = $manifest.version
        artifacts = @(
            [ordered]@{ file = $echoName; bytes = (Get-Item -LiteralPath $echoZip).Length },
            [ordered]@{ file = [System.IO.Path]::GetFileName($sdkZip); bytes = (Get-Item -LiteralPath $sdkZip).Length }
        )
    }
    Write-Utf8 (Join-Path $outputRoot 'BUILD.json') (($metadata | ConvertTo-Json -Depth 6) + "`n")
    Write-Output "Dual module: $echoZip"
    Write-Output "SDK: $sdkZip"
    Write-Output 'Validated dual manifest paths, Windows x64 PE, Android arm64 ELF64 and entry symbol presence.'
} finally {
    # Delete only the exact fresh stage resolved inside this script's explicit output directory.
    $resolvedStage = [System.IO.Path]::GetFullPath($stageRoot)
    $outputPrefix = $outputRoot.TrimEnd('\', '/') + [System.IO.Path]::DirectorySeparatorChar
    if (!$resolvedStage.StartsWith($outputPrefix, [StringComparison]::OrdinalIgnoreCase) -or
        [System.IO.Path]::GetFileName($resolvedStage) -notmatch '^\.stage-[0-9a-f]{32}$') {
        throw "Refusing stage cleanup outside output directory: $resolvedStage"
    }
    Remove-Item -LiteralPath $resolvedStage -Recurse -Force
}

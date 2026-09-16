[CmdletBinding()]
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release",

    [string]$PublishDir = "",

    [switch]$Strict
)

$ErrorActionPreference = "Stop"
$repoRoot = Split-Path -Parent $PSScriptRoot
$nativeRoot = Join-Path $repoRoot "native"
$nativeBuild = Join-Path $repoRoot "artifacts\betterendfield-native-build"
$uiProject = Join-Path $repoRoot "ui\BetterEndfield.UI\BetterEndfield.UI.csproj"
$voiceManifest = Join-Path $repoRoot "manifests\voice\voice-event-media-manifest.json"
$voiceCatalogIndex = Join-Path $repoRoot `
    "ui\BetterEndfield.UI\Assets\voice\voice-catalog-index.json"
$combatSemantics = Join-Path $repoRoot "manifests\combat\combat-semantics.besem"
$buffSourceMap = Join-Path $repoRoot "manifests\combat\buff-sources.bemap"
$combatSemanticsReport = Join-Path $repoRoot `
    "manifests\combat\combat-semantics-report.json"
$publishDir = if ([string]::IsNullOrWhiteSpace($PublishDir)) {
    Join-Path $repoRoot "artifacts\BetterEndfield-win-x64"
}
else {
    [System.IO.Path]::GetFullPath($PublishDir)
}
$nativeStage = Join-Path $nativeBuild "stage\$Configuration"

if (-not (Get-Command cmake.exe -ErrorAction SilentlyContinue)) {
    throw "cmake.exe was not found. Install CMake and the Visual Studio C++ workload."
}

if (-not (Test-Path -LiteralPath $voiceCatalogIndex -PathType Leaf)) {
    throw "Embedded voice catalog index is missing: $voiceCatalogIndex. Run scripts\UpdateResourceManifests.ps1."
}
if (Test-Path -LiteralPath $voiceManifest -PathType Leaf) {
    $catalogIndexMetadata = Get-Content -LiteralPath $voiceCatalogIndex -Raw |
        ConvertFrom-Json
    $voiceManifestSha256 = (Get-FileHash -LiteralPath $voiceManifest -Algorithm SHA256).Hash
    if ($catalogIndexMetadata.kind -ne 'betterendfield-voice-catalog-index' -or
        $catalogIndexMetadata.sourceManifestSha256 -ne $voiceManifestSha256) {
        $msg = "The embedded voice catalog index hash differs from manifest (possibly due to line endings or manifest updates)."
        if ($Strict) {
            throw "$msg Run scripts\UpdateResourceManifests.ps1."
        } else {
            Write-Warning "$msg Continuing build..."
        }
    }
}

if (-not (Test-Path -LiteralPath $combatSemantics -PathType Leaf)) {
    throw "Combat semantics catalogue is missing: $combatSemantics. Run scripts\BuildCombatSemantics.py."
}
if (-not (Test-Path -LiteralPath $buffSourceMap -PathType Leaf)) {
    throw "Buff source map is missing: $buffSourceMap. Run tools\CombatDataExporter\export_combat_data.py."
}
$buffSourceHeader = ((Get-Content -LiteralPath $buffSourceMap -TotalCount 1) -replace "`r", "")
if ($buffSourceHeader -notin @("BESOURCE`t1", "BESOURCE`t2")) {
    throw "Buff source map header is invalid: '$buffSourceHeader'. Run tools\CombatDataExporter\export_combat_data.py."
}

if (Test-Path -LiteralPath $combatSemanticsReport -PathType Leaf) {
    $combatReportMetadata = Get-Content -LiteralPath $combatSemanticsReport -Raw |
        ConvertFrom-Json
    $combatSemanticsSha256 = (Get-FileHash -LiteralPath $combatSemantics `
        -Algorithm SHA256).Hash
    if ($combatReportMetadata.kind -ne 'betterendfield-combat-semantics-build-report' -or
        $combatReportMetadata.schemaVersion -ne 1 -or
        $combatReportMetadata.catalogueSha256 -ne $combatSemanticsSha256) {
        $msg = "The bundled combat semantics catalogue hash differs from build report (possibly due to line endings or updates)."
        if ($Strict) {
            throw "$msg Run scripts\BuildCombatSemantics.py."
        } else {
            Write-Warning "$msg Continuing build..."
        }
    }
}

if (Test-Path -LiteralPath $publishDir) {
    Remove-Item -LiteralPath $publishDir -Recurse -Force
}
New-Item -ItemType Directory -Force -Path $publishDir | Out-Null

# The CMake stage is shared by successive builds in this working tree. Clear it
# so targets removed from the current branch cannot leak into a release package.
if (Test-Path -LiteralPath $nativeStage) {
    Remove-Item -LiteralPath $nativeStage -Recurse -Force
}

& cmake -S $nativeRoot -B $nativeBuild -G "Visual Studio 17 2022" -A x64
if ($LASTEXITCODE -ne 0) {
    throw "Better Endfield native configuration failed with exit code $LASTEXITCODE."
}

& cmake --build $nativeBuild --config $Configuration --target BetterEndfield.Layout --parallel
if ($LASTEXITCODE -ne 0) {
    throw "Better Endfield native build failed with exit code $LASTEXITCODE."
}

dotnet publish $uiProject -c $Configuration -r win-x64 `
    --self-contained true -p:Platform=x64 -p:PublishSingleFile=true `
    -p:DebugType=None -p:DebugSymbols=false -p:PublishDir="$publishDir\"
if ($LASTEXITCODE -ne 0) {
    throw "Better Endfield UI publish failed with exit code $LASTEXITCODE."
}

if (-not (Test-Path -LiteralPath $nativeStage)) {
    throw "Native stage directory was not produced: $nativeStage"
}
$forbiddenMedia = Get-ChildItem -LiteralPath $nativeStage -Recurse -File |
    Where-Object { $_.Extension -in @(".becat", ".wem", ".pck", ".bnk") }
if ($forbiddenMedia) {
    $paths = $forbiddenMedia.FullName -join [Environment]::NewLine
    throw ("Native stage contains local game-media payloads:" +
        [Environment]::NewLine + $paths)
}
$payloadFiles = Get-ChildItem -LiteralPath (Join-Path $nativeStage "payloads") -File
$unexpectedPayloads = $payloadFiles |
    Where-Object { $_.Name -ne "xinput1_4.dll" }
if ($unexpectedPayloads -or
    -not ($payloadFiles | Where-Object { $_.Name -eq "xinput1_4.dll" })) {
    $names = ($payloadFiles.Name | Sort-Object) -join ", "
    throw "Native payload layout must contain only xinput1_4.dll; found: $names"
}
Copy-Item -LiteralPath (Join-Path $nativeStage "runtime") -Destination $publishDir -Recurse -Force
Copy-Item -LiteralPath (Join-Path $nativeStage "modules") -Destination $publishDir -Recurse -Force
Copy-Item -LiteralPath (Join-Path $nativeStage "loaders") -Destination $publishDir -Recurse -Force
Copy-Item -LiteralPath (Join-Path $nativeStage "payloads") -Destination $publishDir -Recurse -Force

$forbiddenReleaseMedia = Get-ChildItem -LiteralPath $publishDir -Recurse -File |
    Where-Object { $_.Extension -in @(".becat", ".wem", ".pck", ".bnk") }
if ($forbiddenReleaseMedia) {
    $paths = $forbiddenReleaseMedia.FullName -join [Environment]::NewLine
    throw ("Final release contains local game-media payloads:" +
        [Environment]::NewLine + $paths)
}
$runtimeMarkers = Get-ChildItem -LiteralPath $publishDir -Recurse -File |
    Where-Object {
        $_.Name -in @(
            "BetterEndfield-bootstrap.loaded",
            "BetterEndfield-bootstrap-host.status")
    }
if ($runtimeMarkers) {
    $paths = $runtimeMarkers.FullName -join [Environment]::NewLine
    throw ("Final release contains runtime marker files:" +
        [Environment]::NewLine + $paths)
}
$requiredReleaseFiles = @(
    "BetterEndfield.exe",
    "runtime\BetterEndfield.Host.dll",
    "modules\BetterEndfield.Model.dll",
    "modules\BetterEndfield.Voice.dll",
    "modules\BetterEndfield.Music.dll",
    "modules\BetterEndfield.UiModule.dll",
    "modules\BetterEndfield.Camera.dll",
    "modules\BetterEndfield.Actions.dll",
    "modules\BetterEndfield.Gacha.dll",
    "modules\BetterEndfield.CombatStats.dll",
    "modules\BetterEndfield.CombatOverlay.exe",
    "modules\combat-semantics.besem",
    "modules\buff-sources.bemap",
    "modules\betterendfield.ui.module.ini",
    "modules\betterendfield.camera.module.ini",
    "modules\betterendfield.actions.module.ini",
    "modules\actions\pose_aglina.bin",
    "modules\actions\pose_liino.bin",
    "modules\betterendfield.gacha.module.ini",
    "modules\betterendfield.music.module.ini",
    "modules\betterendfield.combat_stats.module.ini",
    "loaders\BetterEndfield.Injector.exe",
    "payloads\xinput1_4.dll"
)
$missingReleaseFiles = $requiredReleaseFiles |
    Where-Object { -not (Test-Path -LiteralPath (Join-Path $publishDir $_) -PathType Leaf) }
if ($missingReleaseFiles) {
    throw "Final release is incomplete: $($missingReleaseFiles -join ', ')"
}

Write-Host ""
Write-Host "Better Endfield build complete: $publishDir"
Write-Host "Run BetterEndfield.exe, verify the detected paths, then choose Injector or XInput."

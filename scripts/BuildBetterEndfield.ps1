[CmdletBinding()]
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release",

    [string]$PublishDir = "",

    [string]$WorkspaceConfig = "",

    [ValidateRange(0, 64)]
    [int]$Parallel = 0
)

$ErrorActionPreference = "Stop"
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
Set-BEWorkspaceEnvironment $ws
$repoRoot = $ws.repo_root
$cmake = $ws.tools.cmake
$dotnet = $ws.tools.dotnet
$python = $ws.tools.python
$nativeRoot = Join-Path $repoRoot "native"
$platformBuild = Join-Path $ws.paths.build "windows\win-x64\$Configuration"
$nativeBuild = Join-Path $platformBuild "native"
$uiProject = Join-Path $repoRoot "ui\BetterEndfield.UI\BetterEndfield.UI.csproj"
$manifestRoot = $ws.resource_update.outputs.manifests
$voiceManifest = Join-Path $manifestRoot "voice\voice-event-media-manifest.json"
$voiceCatalogIndex = $ws.resource_update.outputs.voice_index
$combatSemantics = Join-Path $manifestRoot "combat\combat-semantics.besem"
$buffSourceMap = Join-Path $manifestRoot "combat\buff-sources.bemap"
$combatSemanticsReport = Join-Path $manifestRoot "combat\combat-semantics-report.json"
$publishDir = if ([string]::IsNullOrWhiteSpace($PublishDir)) {
    Join-Path $platformBuild "publish"
}
else {
    [System.IO.Path]::GetFullPath($PublishDir)
}

foreach ($command in @($cmake, $dotnet, $python)) {
    if (-not (Get-Command $command -ErrorAction SilentlyContinue)) {
        throw "Configured build command was not found: $command"
    }
}

if (-not (Test-Path -LiteralPath $voiceManifest -PathType Leaf) -or
    -not (Test-Path -LiteralPath $voiceCatalogIndex -PathType Leaf)) {
    throw "Voice manifest or embedded catalog index is missing. Run scripts\UpdateResourceManifests.ps1."
}
$catalogIndexMetadata = Get-Content -LiteralPath $voiceCatalogIndex -Raw |
    ConvertFrom-Json
$voiceManifestSha256 = (Get-FileHash -LiteralPath $voiceManifest -Algorithm SHA256).Hash
if ($catalogIndexMetadata.kind -ne 'betterendfield-voice-catalog-index' -or
    $catalogIndexMetadata.sourceManifestSha256 -ne $voiceManifestSha256) {
    throw "The embedded voice catalog index is stale. Run scripts\UpdateResourceManifests.ps1."
}
if (-not (Test-Path -LiteralPath $combatSemantics -PathType Leaf) -or
    -not (Test-Path -LiteralPath $combatSemanticsReport -PathType Leaf)) {
    throw "Combat semantics catalogue or build report is missing. Run scripts\BuildCombatSemantics.py."
}
if (-not (Test-Path -LiteralPath $buffSourceMap -PathType Leaf) -or
    (Get-Content -LiteralPath $buffSourceMap -TotalCount 1) -notin @("BESOURCE`t1", "BESOURCE`t2")) {
    throw "Buff source map is missing or invalid. Run tools\CombatDataExporter\export_combat_data.py."
}
$combatReportMetadata = Get-Content -LiteralPath $combatSemanticsReport -Raw |
    ConvertFrom-Json
$combatSemanticsSha256 = (Get-FileHash -LiteralPath $combatSemantics `
    -Algorithm SHA256).Hash
if ($combatReportMetadata.kind -ne 'betterendfield-combat-semantics-build-report' -or
    $combatReportMetadata.schemaVersion -ne 1 -or
    $combatReportMetadata.catalogueSha256 -ne $combatSemanticsSha256) {
    throw "The bundled combat semantics catalogue is stale or invalid. Run scripts\BuildCombatSemantics.py."
}

# Keep explicit publish directories, but never reset retained data or installs.
$publishDir = [System.IO.Path]::GetFullPath($publishDir)
$publishIdentity = $publishDir.TrimEnd('\', '/')
$protectedRoots = @($repoRoot, $ws.paths.build, $ws.paths.releases,
    [System.IO.Path]::GetPathRoot($publishDir))
foreach ($area in $protectedRoots) {
    $identity = [System.IO.Path]::GetFullPath($area).TrimEnd('\', '/')
    if ($publishIdentity -eq $identity -or
        $identity.StartsWith($publishIdentity + '\', [StringComparison]::OrdinalIgnoreCase)) {
        throw "Refusing to reset a workspace/output root: $publishDir"
    }
}
$retainedAreas = @($ws.legacy.root, $ws.game.install_dir, $ws.test.be_install_dir,
    $ws.paths.inputs, $ws.paths.research, $ws.paths.toolchains, $ws.paths.generated)
foreach ($sourceDirectory in @('native', 'ui', 'android', 'web', 'tools', 'scripts',
    'docs', 'config', 'resources', 'manifests', 'legacy', '.git')) {
    $retainedAreas += Join-Path $repoRoot $sourceDirectory
}
foreach ($area in $retainedAreas) {
    if (-not $area) { continue }
    $identity = [System.IO.Path]::GetFullPath($area).TrimEnd('\', '/')
    if ($publishIdentity -eq $identity -or
        $identity.StartsWith($publishIdentity + '\', [StringComparison]::OrdinalIgnoreCase) -or
        $publishIdentity.StartsWith($identity + '\', [StringComparison]::OrdinalIgnoreCase)) {
        throw "Publish directory overlaps retained data: $publishDir"
    }
}
$ancestor = $publishDir
while ($ancestor) {
    if (Test-Path -LiteralPath $ancestor) {
        $entry = Get-Item -LiteralPath $ancestor -Force
        if ($entry.Attributes -band [System.IO.FileAttributes]::ReparsePoint) {
            throw "Publish directory traverses a reparse point: $ancestor"
        }
    }
    $ancestor = Split-Path -Parent $ancestor
}
if (Test-Path -LiteralPath $publishDir) {
    Remove-Item -LiteralPath $publishDir -Recurse -Force
}
New-Item -ItemType Directory -Force -Path $publishDir | Out-Null

& $cmake -S $nativeRoot -B $nativeBuild -G "Visual Studio 17 2022" -A x64
if ($LASTEXITCODE -ne 0) {
    throw "Better Endfield native configuration failed with exit code $LASTEXITCODE."
}

$nativeBuildArgs = @('--build', $nativeBuild, '--config', $Configuration, '--target', 'BetterEndfield.Layout', '--parallel')
if ($Parallel -gt 0) { $nativeBuildArgs += $Parallel }
& $cmake @nativeBuildArgs
if ($LASTEXITCODE -ne 0) {
    throw "Better Endfield native build failed with exit code $LASTEXITCODE."
}

& $dotnet publish $uiProject -c $Configuration -r win-x64 `
    --self-contained true -p:Platform=x64 -p:PublishSingleFile=true `
    -p:DebugType=None -p:DebugSymbols=false -p:PublishDir="$publishDir\" `
    "-p:BEWorkspaceBuildRoot=$($ws.paths.build)" `
    "-p:BECharacterPresetsPath=$($ws.resource_update.outputs.character_presets)" `
    "-p:BEVoiceCatalogIndexPath=$voiceCatalogIndex" `
    "-p:BECombatDictionaryPath=$($ws.resource_update.outputs.combat_dictionary)"
if ($LASTEXITCODE -ne 0) {
    throw "Better Endfield UI publish failed with exit code $LASTEXITCODE."
}

& (Join-Path $PSScriptRoot "BuildBemTools.ps1") -WorkspaceConfig $WorkspaceConfig
$bemTools = Join-Path $ws.paths.build "tools\bem\dist\BetterEndfield.BemConverter"
New-Item -ItemType Directory -Force -Path (Join-Path $publishDir "tools") | Out-Null
Copy-Item -LiteralPath $bemTools -Destination (Join-Path $publishDir "tools\BemConverter") -Recurse -Force
New-Item -ItemType Directory -Force -Path (Join-Path $publishDir "docs") | Out-Null
$documentNames = @("BEM_CREATOR_GUIDE.md", "BEM_FORMAT_SPEC.md", "BEM_RUNTIME_COMPATIBILITY.md", "BEM_SOURCE_MOD_CONVERSION.md", "THIRD_PARTY_MODULE_CREATOR_GUIDE.md")
$resolveDocuments = @'
import json, sys
from pathlib import Path
sys.path.insert(0, str(Path(sys.argv[1]) / 'scripts'))
from workspace_config import load_workspace
ws = load_workspace(repo_root=sys.argv[1])
print(json.dumps([{'source': str(ws.document(name)), 'basename': name} for name in sys.argv[2:]]))
'@
$documentJson = & $python -c $resolveDocuments $repoRoot @documentNames
if ($LASTEXITCODE -ne 0) { throw "Release document paths could not be resolved." }
foreach ($document in (($documentJson -join "`n") | ConvertFrom-Json)) {
    Copy-Item -LiteralPath $document.source -Destination (Join-Path $publishDir "docs\$($document.basename)") -Force
}

$nativeStage = Join-Path $nativeBuild "stage\$Configuration"
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
    "tools\BemConverter\BetterEndfield.BemConverter.exe",
    "docs\BEM_CREATOR_GUIDE.md",
    "modules\BetterEndfield.CustomModel.dll",
    "modules\betterendfield.custom_model.module.ini",
    "runtime\BetterEndfield.Host.dll",
    "modules\BetterEndfield.Model.dll",
    "modules\BetterEndfield.Voice.dll",
    "modules\BetterEndfield.Music.dll",
    "modules\BetterEndfield.UiModule.dll",
    "modules\BetterEndfield.Camera.dll",
    "modules\BetterEndfield.Gacha.dll",
    "modules\BetterEndfield.CombatStats.dll",
    "modules\BetterEndfield.CombatOverlay.exe",
    "modules\BetterEndfield.MmdOverlay.exe",
    "modules\BetterEndfield.ModelOverlay.exe",
    "modules\model-character-names.json",
    "modules\model-character-names-en.json",
    "modules\combat-semantics.besem",
    "modules\buff-sources.bemap",
    "modules\betterendfield.ui.module.ini",
    "modules\betterendfield.camera.module.ini",
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

# Persist a complete distributable outside the cleanable publish directory.
$version = ([xml](Get-Content -LiteralPath (Join-Path $repoRoot 'Directory.Build.props') -Raw)).SelectSingleNode('//Version').InnerText.Trim()
$releaseDir = Join-Path $ws.paths.releases "windows\win-x64\$Configuration"
New-Item -ItemType Directory -Force -Path $releaseDir | Out-Null
$releaseArchive = Join-Path $releaseDir "BetterEndfield-$version-win-x64.zip"
$temporaryArchive = Join-Path $ws.paths.temp ("BetterEndfield-release-" + [Guid]::NewGuid().ToString('N') + '.zip')
Add-Type -AssemblyName System.IO.Compression.FileSystem
try {
    [System.IO.Compression.ZipFile]::CreateFromDirectory($publishDir, $temporaryArchive)
    Move-Item -LiteralPath $temporaryArchive -Destination $releaseArchive -Force
} finally {
    if (Test-Path -LiteralPath $temporaryArchive) {
        Remove-Item -LiteralPath $temporaryArchive -Force
    }
}

Write-Host ""
Write-Host "Better Endfield build complete: $publishDir"
Write-Host "Release archive: $releaseArchive"
Write-Host "Run BetterEndfield.exe, verify the detected paths, then choose Injector or XInput."

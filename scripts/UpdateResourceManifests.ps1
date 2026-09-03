[CmdletBinding()]
param(
    [string]$GamePath = 'E:\Endfield Game',
    [string]$Workspace = (Split-Path -Parent $PSScriptRoot),
    [switch]$SkipInputRefresh,
    [switch]$SkipPckDiscovery
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$Workspace = [IO.Path]::GetFullPath($Workspace).TrimEnd('\')
$GamePath = [IO.Path]::GetFullPath($GamePath).TrimEnd('\')
$python = Get-Command py -ErrorAction Stop

if (-not $SkipInputRefresh) {
    & $python.Source -3 (Join-Path $Workspace 'scripts\RefreshEndfieldResourceInputs.py') `
        '--game-path' $GamePath `
        '--output' (Join-Path $Workspace 'research\current-inputs')
    if ($LASTEXITCODE -ne 0) { throw 'Current VFS input refresh failed.' }
}

$currentInputs = Join-Path $Workspace 'research\current-inputs'
$currentCatalog = Join-Path $Workspace 'research\character-catalog-current'
& $python.Source -3 (Join-Path $Workspace 'scripts\ScanCharacterAssets.py') `
    '--manifest' (Join-Path $currentInputs 'Bundles\Windows\manifest.json') `
    '--prefab-info' (Join-Path $currentInputs 'Json_decrypted\NPC\PrefabInfo') `
    '--clip-json' (Join-Path $Workspace 'research\character-catalog\walk-clip-metadata.json') `
    '--out' $currentCatalog
if ($LASTEXITCODE -ne 0) { throw 'Character asset scan failed.' }

& $python.Source -3 (Join-Path $Workspace 'scripts\GenerateModCharacterPresets.py') `
    '--manifest' (Join-Path $currentInputs 'Bundles\Windows\manifest.json') `
    '--catalog' (Join-Path $currentCatalog 'characters.json') `
    '--output' (Join-Path $currentInputs 'character-presets.json')
if ($LASTEXITCODE -ne 0) { throw 'Character preset generation failed.' }

$embeddedCharacterPresets = Join-Path $Workspace `
    'ui\BetterEndfield.UI\Assets\model\character-presets.json'
Copy-Item -LiteralPath (Join-Path $currentInputs 'character-presets.json') `
    -Destination $embeddedCharacterPresets -Force

$arguments = @(
    '-3',
    (Join-Path $Workspace 'scripts\GenerateResourceManifests.py'),
    '--game-path', $GamePath,
    '--output-dir', (Join-Path $Workspace 'manifests')
)
if ($SkipPckDiscovery) { $arguments += '--no-pck-discovery' }

& $python.Source @arguments
if ($LASTEXITCODE -ne 0) { throw 'Resource manifest generation failed.' }

$manifestArtifacts = @(
    'manifests\model\action-manifest.json',
    'manifests\voice\voice-event-media-manifest.json',
    'manifests\shared\resource-manifest-report.md'
)
foreach ($relativePath in $manifestArtifacts) {
    $artifact = Join-Path $Workspace $relativePath
    if (-not (Test-Path -LiteralPath $artifact -PathType Leaf)) {
        throw "Expected manifest output was not generated: $artifact"
    }
}

# Catalogs intentionally remain local, selected-language runtime inputs.
# They are built after a user selects the target speaker and language.
foreach ($relativePath in @(
    'manifests\model\action-manifest.json',
    'manifests\voice\voice-event-media-manifest.json',
    'ui\BetterEndfield.UI\Assets\model\character-presets.json')) {
    $artifact = Join-Path $Workspace $relativePath
    try {
        $null = Get-Content -LiteralPath $artifact -Raw | ConvertFrom-Json
    }
    catch {
        throw "Generated manifest is not valid JSON: $artifact"
    }
}

& $python.Source -3 (Join-Path $Workspace 'scripts\GenerateVoiceCatalogIndex.py') `
    '--manifest' (Join-Path $Workspace 'manifests\voice\voice-event-media-manifest.json') `
    '--output' (Join-Path $Workspace 'ui\BetterEndfield.UI\Assets\voice\voice-catalog-index.json')
if ($LASTEXITCODE -ne 0) { throw 'Voice catalog runtime index generation failed.' }

# Combat Data Dictionary Export (Characters, Weapons, Suits, Dungeons, Skills)
$tableDir = Join-Path $Workspace 'research\table-dump\Table'
$combatSemantics = Join-Path $Workspace 'manifests\combat\combat-semantics.besem'
if (Test-Path -LiteralPath $tableDir -PathType Container) {
    & $python.Source -3 (Join-Path $Workspace 'tools\CombatDataExporter\export_combat_data.py') `
        '--table-dir' $tableDir `
        '--game-path' $GamePath `
        '--refresh-tables' `
        '--besem' $combatSemantics `
        '--output' (Join-Path $Workspace 'manifests\combat\combat-dictionary.json') `
        '--ui-output' (Join-Path $Workspace 'ui\BetterEndfield.UI\Assets\combat\combat-dictionary.json') `
        '--min-output' (Join-Path $Workspace 'web\src\data\combat-dict.min.json') `
        '--verify'
    if ($LASTEXITCODE -ne 0) { throw 'Combat data dictionary export failed.' }
}

Write-Host "Resource manifests updated successfully: $(Join-Path $Workspace 'manifests')" -ForegroundColor Green

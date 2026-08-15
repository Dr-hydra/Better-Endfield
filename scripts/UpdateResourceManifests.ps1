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

$arguments = @(
    '-3',
    (Join-Path $Workspace 'scripts\GenerateResourceManifests.py'),
    '--game-path', $GamePath,
    '--output-dir', (Join-Path $Workspace 'manifests')
)
if ($SkipPckDiscovery) { $arguments += '--no-pck-discovery' }

& $python.Source @arguments
if ($LASTEXITCODE -ne 0) { throw 'Resource manifest generation failed.' }

@('action-manifest.json', 'voice-event-media-manifest.json',
    'resource-manifest-report.md') | ForEach-Object {
    $artifact = Join-Path $Workspace "manifests\$_"
    if (-not (Test-Path -LiteralPath $artifact -PathType Leaf)) {
        throw "Expected manifest output was not generated: $artifact"
    }
}

# Catalogs intentionally remain local, selected-language runtime inputs.
# They are built after a user selects the target speaker and language.
foreach ($jsonArtifact in @('action-manifest.json', 'voice-event-media-manifest.json')) {
    $artifact = Join-Path $Workspace "manifests\$jsonArtifact"
    try {
        $null = Get-Content -LiteralPath $artifact -Raw | ConvertFrom-Json
    }
    catch {
        throw "Generated manifest is not valid JSON: $artifact"
    }
}

& $python.Source -3 (Join-Path $Workspace 'scripts\GenerateVoiceCatalogIndex.py') `
    '--manifest' (Join-Path $Workspace 'manifests\voice-event-media-manifest.json') `
    '--output' (Join-Path $Workspace 'src\BetterEndfield.UI\Assets\voice-catalog-index.json')
if ($LASTEXITCODE -ne 0) { throw 'Voice catalog runtime index generation failed.' }

Write-Host "Resource manifests: $(Join-Path $Workspace 'manifests')"

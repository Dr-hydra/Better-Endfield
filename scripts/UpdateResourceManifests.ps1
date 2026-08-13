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

& $python.Source -3 (Join-Path $Workspace 'scripts\CompileVoiceRuntimeMap.py')
if ($LASTEXITCODE -ne 0) { throw 'Voice runtime map compilation failed.' }

Write-Host "Resource manifests: $(Join-Path $Workspace 'manifests')"

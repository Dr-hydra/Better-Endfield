[CmdletBinding()]
param(
    [string]$GamePath = '',
    [string]$Workspace = '',
    [Alias('workspace-config')][string]$WorkspaceConfig = '',
    [switch]$SkipInputRefresh,
    [switch]$SkipPckDiscovery,
    [switch]$Plan
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
if (-not $Workspace) { $Workspace = Split-Path -Parent $PSScriptRoot }
$Workspace = [IO.Path]::GetFullPath($Workspace).TrimEnd('\')
. (Join-Path $Workspace 'scripts/Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
if (-not $GamePath) { $GamePath = $ws.game.install_dir }
if ($GamePath) { $GamePath = [IO.Path]::GetFullPath($GamePath).TrimEnd('\') }
$python = $ws.tools.python
$currentInputs = $ws.resource_update.input_root
$currentCatalog = $ws.resource_update.catalog_root
$outputs = $ws.resource_update.outputs
$manifestRoot = $outputs.manifests
$configArgs = @()
if ($ws.config_file) { $configArgs = @('--workspace-config', $ws.config_file) }
$gameArgs = @()
if ($GamePath) { $gameArgs = @('--game-path', $GamePath) }

if ($Plan) {
    [ordered]@{
        workspace = $ws.repo_root
        config = $ws.config_file
        python = $python
        gamePath = $GamePath
        inputRoot = $currentInputs
        catalogRoot = $currentCatalog
        outputs = $outputs
        temp = $ws.paths.temp
        refreshInputs = (-not $SkipInputRefresh)
        discoverPck = (-not $SkipPckDiscovery)
    } | ConvertTo-Json -Depth 8
    return
}
if (-not $SkipInputRefresh -and -not $GamePath) {
    throw 'Configure game.install_dir or pass -GamePath to refresh game inputs.'
}
Set-BEWorkspaceEnvironment $ws

function Invoke-ResourceStep {
    param([string]$Script, [string[]]$Arguments, [string]$Failure)
    & $python (Join-Path $Workspace $Script) @configArgs @Arguments
    if ($LASTEXITCODE -ne 0) { throw $Failure }
}

if (-not $SkipInputRefresh) {
    Invoke-ResourceStep 'scripts/RefreshEndfieldResourceInputs.py' `
        (@($gameArgs) + @('--output', $currentInputs)) 'Current VFS input refresh failed.'
}
$platform = $ws.game.platform
Invoke-ResourceStep 'scripts/ScanCharacterAssets.py' @(
    '--manifest', (Join-Path $currentInputs "Bundles/$platform/manifest.json"),
    '--prefab-info', (Join-Path $currentInputs 'Json_decrypted/NPC/PrefabInfo'),
    '--clip-json', $ws.resource_update.walk_clip_metadata,
    '--out', $currentCatalog
) 'Character asset scan failed.'
Invoke-ResourceStep 'scripts/GenerateModCharacterPresets.py' @(
    '--manifest', (Join-Path $currentInputs "Bundles/$platform/manifest.json"),
    '--catalog', (Join-Path $currentCatalog 'characters.json'),
    '--output', $outputs.character_presets
) 'Character preset generation failed.'
$manifestArgs = @($gameArgs) + @('--output-dir', $manifestRoot)
if ($SkipPckDiscovery) { $manifestArgs += '--no-pck-discovery' }
Invoke-ResourceStep 'scripts/GenerateResourceManifests.py' $manifestArgs 'Resource manifest generation failed.'

foreach ($artifact in @(
    (Join-Path $manifestRoot 'model/action-manifest.json'),
    (Join-Path $manifestRoot 'voice/voice-event-media-manifest.json'),
    $outputs.character_presets
)) {
    if (-not (Test-Path -LiteralPath $artifact -PathType Leaf)) {
        throw "Expected output was not generated: $artifact"
    }
    try { $null = Get-Content -LiteralPath $artifact -Raw | ConvertFrom-Json }
    catch { throw "Generated output is not valid JSON: $artifact" }
}
$report = Join-Path $manifestRoot 'shared/resource-manifest-report.md'
if (-not (Test-Path -LiteralPath $report -PathType Leaf)) { throw "Expected report was not generated: $report" }

Invoke-ResourceStep 'scripts/GenerateVoiceCatalogIndex.py' @(
    '--manifest', (Join-Path $manifestRoot 'voice/voice-event-media-manifest.json'),
    '--output', $outputs.voice_index
) 'Voice catalog runtime index generation failed.'

$tableDir = $ws.resource_update.table_root
if ((Test-Path -LiteralPath $tableDir -PathType Container) -or -not $SkipInputRefresh) {
    $combatArgs = @($gameArgs) + @(
        '--table-dir', $tableDir,
        '--besem', (Join-Path $manifestRoot 'combat/combat-semantics.besem'),
        '--output', $outputs.combat_dictionary,
        '--min-output', $outputs.web_combat_dictionary,
        '--verify'
    )
    if ($SkipInputRefresh) { $combatArgs += @('--no-refresh-tables', '--no-refresh-json-data') }
    Invoke-ResourceStep 'tools/CombatDataExporter/export_combat_data.py' $combatArgs 'Combat data dictionary export failed.'
}
Write-Host "Resource manifests updated successfully: $manifestRoot" -ForegroundColor Green

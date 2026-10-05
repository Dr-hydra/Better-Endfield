[CmdletBinding()]
param(
    [ValidateSet('arm','status','stop')][string]$Action = 'status',
    [string]$Manifest = '',
    [string[]]$Characters = @(),
    [string]$Run = '',
    [string]$Catalog = '',
    [switch]$SingleSession,
    [string]$WorkspaceConfig = ''
)
$ErrorActionPreference = 'Stop'
$repo = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..')).Path
. (Join-Path $repo 'scripts\Workspace.ps1')
$workspace = Get-BEWorkspace -Config $WorkspaceConfig
Set-BEWorkspaceEnvironment $workspace
$entry = Join-Path (Split-Path -Parent $PSScriptRoot) 'runtime_sweep.py'
$arguments = @($entry, $Action)
if ($Manifest) { $arguments += @('--manifest', $Manifest) }
if ($Characters.Count) { $arguments += @('--characters') + $Characters }
if ($Run) { $arguments += @('--run', $Run) }
if ($Catalog) { $arguments += @('--catalog', $Catalog) }
if ($Action -eq 'arm' -and -not $SingleSession) { $arguments += '--persistent' }
if ($workspace.config_file) { $arguments += @('--workspace-config',$workspace.config_file) }
& $workspace.tools.python @arguments
if ($LASTEXITCODE -ne 0) { throw "Probe command failed: $LASTEXITCODE" }

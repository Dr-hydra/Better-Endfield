[CmdletBinding()]
param(
    [ValidateSet('arm','status','stop')][string]$Action = 'status',
    [string]$Manifest = '',
    [string[]]$Characters = @(),
    [string]$Run = '',
    [string]$Catalog = '',
    [switch]$SingleSession
)
$ErrorActionPreference = 'Stop'
$entry = Join-Path (Split-Path -Parent $PSScriptRoot) 'runtime_sweep.py'
$arguments = @($entry, $Action)
if ($Manifest) { $arguments += @('--manifest', $Manifest) }
if ($Characters.Count) { $arguments += @('--characters') + $Characters }
if ($Run) { $arguments += @('--run', $Run) }
if ($Catalog) { $arguments += @('--catalog', $Catalog) }
if ($Action -eq 'arm' -and -not $SingleSession) { $arguments += '--persistent' }
& python @arguments
if ($LASTEXITCODE -ne 0) { throw "Probe command failed: $LASTEXITCODE" }

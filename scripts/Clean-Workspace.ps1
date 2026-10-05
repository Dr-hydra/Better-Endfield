[CmdletBinding(SupportsShouldProcess=$true)]
param([string]$WorkspaceConfig = '', [switch]$Apply)
$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
$cleanupArgs = @((Join-Path $PSScriptRoot 'workspace_config.py'),'clean-plan')
if ($WorkspaceConfig) { $cleanupArgs += @('--config',$WorkspaceConfig) }
$cleanupText = & $ws.tools.python @cleanupArgs
if ($LASTEXITCODE -ne 0) { throw 'Cleanup boundary validation failed.' }
$cleanupPlan = ($cleanupText -join "`n") | ConvertFrom-Json
foreach ($cleanupTarget in $cleanupPlan.targets) {
    $cleanupAbsolute = [IO.Path]::GetFullPath($cleanupTarget)
    if (-not $cleanupAbsolute.StartsWith($ws.repo_root.TrimEnd('\')+'\',[StringComparison]::OrdinalIgnoreCase)) { throw 'Cleanup target outside active workspace.' }
    Write-Output $cleanupAbsolute
    if (-not $Apply) { continue }
    if (Test-Path -LiteralPath $cleanupAbsolute) {
        $cleanupItem = Get-Item -LiteralPath $cleanupAbsolute -Force
        if (-not $cleanupItem.PSIsContainer -or ($cleanupItem.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0) { throw 'Cleanup target is not an ordinary directory.' }
        if ($PSCmdlet.ShouldProcess($cleanupAbsolute,'Remove reproducible workspace output')) {
            Remove-Item -LiteralPath $cleanupAbsolute -Recurse -Force
        }
    }
}
if (-not $Apply) { Write-Output 'Plan only. Specify -Apply to clean these reproducible directories.' }

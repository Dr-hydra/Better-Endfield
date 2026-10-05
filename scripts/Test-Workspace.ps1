[CmdletBinding()]
param(
    [string]$WorkspaceConfig,
    [switch]$List,
    [switch]$Plan,
    [string[]]$Module,
    [string[]]$Platform,
    [string[]]$Kind,
    [string[]]$Suite,
    [string[]]$GameVersion,
    [switch]$AllowGame,
    [string]$Output,
    [string]$Registry
)
$ErrorActionPreference = 'Stop'
if ($List -and $Plan) { throw '-List and -Plan are mutually exclusive.' }
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$testWorkspace = Get-BEWorkspace -Config $WorkspaceConfig
# The Python runner sets ws.env() only before an actual suite launch; list/plan
# must not create temp directories or change the caller's process environment.
$testArguments = @('-B', (Join-Path $PSScriptRoot 'run_workspace_tests.py'))
if ($WorkspaceConfig) { $testArguments += @('--workspace-config', $WorkspaceConfig) }
if ($List) { $testArguments += '--list' }
if ($Plan) { $testArguments += '--plan' }
if ($AllowGame) { $testArguments += '--allow-game' }
foreach ($testFilter in @(
    @{ Name = 'module'; Values = $Module },
    @{ Name = 'platform'; Values = $Platform },
    @{ Name = 'kind'; Values = $Kind },
    @{ Name = 'suite'; Values = $Suite },
    @{ Name = 'game-version'; Values = $GameVersion }
)) {
    foreach ($testValue in $testFilter.Values) { $testArguments += @(('--' + $testFilter.Name), [string]$testValue) }
}
if ($Output) { $testArguments += @('--output', $Output) }
if ($Registry) { $testArguments += @('--registry', $Registry) }
& $testWorkspace.tools.python @testArguments
exit $LASTEXITCODE

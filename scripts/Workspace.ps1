function Get-BEWorkspace {
    param([string]$Config = '')
    $workspaceScript = Join-Path $PSScriptRoot 'workspace_config.py'
    $env:PYTHONDONTWRITEBYTECODE = '1'
    $workspaceBootstrap = Get-Command python -ErrorAction SilentlyContinue
    if (-not $workspaceBootstrap) { throw 'Python is required to resolve workspace configuration.' }
    $workspaceArgs = @($workspaceScript, 'resolve')
    if ($Config) { $workspaceArgs += @('--config', $Config) }
    $workspaceText = & $workspaceBootstrap.Source @workspaceArgs
    if ($LASTEXITCODE -ne 0) { throw 'Workspace configuration could not be resolved.' }
    return (($workspaceText -join "`n") | ConvertFrom-Json)
}

function Set-BEWorkspaceEnvironment {
    param($Workspace)
    New-Item -ItemType Directory -Path $Workspace.paths.temp -Force | Out-Null
    $env:TEMP = $Workspace.paths.temp
    $env:TMP = $Workspace.paths.temp
    $env:TMPDIR = $Workspace.paths.temp
    $env:PYTHONDONTWRITEBYTECODE = '1'
    $env:BE_WORKSPACE_ROOT = $Workspace.repo_root
    $env:BE_WORKSPACE_BUILD_ROOT = $Workspace.paths.build
    $env:BE_WORKSPACE_TEMP_ROOT = $Workspace.paths.temp
    $env:BE_WORKSPACE_ANDROID_SDK = $Workspace.tools.android_sdk
    $env:BE_WORKSPACE_DOBBY_ROOT = $Workspace.tools.android_dobby
    if ($Workspace.config_file) { $env:BE_WORKSPACE_CONFIG = $Workspace.config_file }
}

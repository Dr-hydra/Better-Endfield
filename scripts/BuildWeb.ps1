[CmdletBinding()]
param(
    [string]$WorkspaceConfig = "",
    [switch]$SkipRestore
)

$ErrorActionPreference = "Stop"
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
Set-BEWorkspaceEnvironment $ws
$webRoot = Join-Path $ws.repo_root 'web'
$buildRoot = Join-Path $ws.paths.build 'web'
$configRoot = Join-Path $buildRoot 'tsconfig'
$distRoot = Join-Path $buildRoot 'dist'
$node = $ws.tools.node
$npm = $ws.tools.npm
foreach ($command in @($node, $npm)) {
    if (-not (Get-Command $command -ErrorAction SilentlyContinue)) {
        throw "Configured web build command was not found: $command"
    }
}
New-Item -ItemType Directory -Force -Path $configRoot | Out-Null
$configs = @()
foreach ($name in @('app', 'node')) {
    $target = Join-Path $configRoot "tsconfig.$name.json"
    $config = @{
        extends = Join-Path $webRoot "tsconfig.$name.json"
        compilerOptions = @{
            tsBuildInfoFile = Join-Path $buildRoot "tsconfig.$name.tsbuildinfo"
        }
    }
    $config | ConvertTo-Json -Depth 4 | Set-Content -LiteralPath $target -Encoding UTF8
    $configs += $target
}

Push-Location $webRoot
try {
    if (-not $SkipRestore) {
        & $npm ci --cache (Join-Path $ws.paths.cache 'npm') --no-audit --no-fund
        if ($LASTEXITCODE -ne 0) { throw "Web dependency restore failed: $LASTEXITCODE" }
    }
    & $node (Join-Path $webRoot 'node_modules/typescript/bin/tsc') -b @configs
    if ($LASTEXITCODE -ne 0) { throw "Web TypeScript build failed: $LASTEXITCODE" }
    & $node (Join-Path $webRoot 'node_modules/vite/bin/vite.js') build `
        --config (Join-Path $webRoot 'vite.config.ts') --outDir $distRoot --emptyOutDir
    if ($LASTEXITCODE -ne 0) { throw "Web asset build failed: $LASTEXITCODE" }
} finally {
    Pop-Location
}
Write-Host "Web build complete: $distRoot"

[CmdletBinding()]
param([string]$WorkspaceConfig = '')
$ErrorActionPreference = 'Stop'
$repoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../../..'))
. (Join-Path $repoRoot 'scripts/Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
$testRoot = Join-Path $ws.paths.build 'desktop-launch-tests'
$project = Join-Path $PSScriptRoot 'DesktopLaunch.csproj'
& $ws.tools.dotnet build $project -c Release "-p:BEWorkspaceBuildRoot=$testRoot" --nologo
if ($LASTEXITCODE -ne 0) { throw 'Desktop launch test build failed.' }
$bin = Join-Path $testRoot 'dotnet/DesktopLaunch/AnyCPU/bin/Release/net9.0'
& $ws.tools.dotnet (Join-Path $bin 'DesktopLaunch.dll') $testRoot
if ($LASTEXITCODE -ne 0) { throw 'Desktop launch regression failed.' }
$obfuscated = Join-Path $testRoot 'obfuscated'
& (Join-Path $repoRoot 'scripts/ObfuscateNext.ps1') -AssemblyPath (Join-Path $bin 'DesktopLaunch.dll') `
    -OutputDirectory $obfuscated -WorkspaceConfig $WorkspaceConfig
Copy-Item -LiteralPath (Join-Path $bin 'DesktopLaunch.deps.json'),(Join-Path $bin 'DesktopLaunch.runtimeconfig.json') `
    -Destination $obfuscated
& $ws.tools.dotnet (Join-Path $obfuscated 'DesktopLaunch.dll') $testRoot
if ($LASTEXITCODE -ne 0) { throw 'Obfuscated desktop launch regression failed.' }

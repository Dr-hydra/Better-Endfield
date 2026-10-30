[CmdletBinding()]
param([string]$WorkspaceConfig = '')
$ErrorActionPreference = 'Stop'
$repoRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../../..'))
. (Join-Path $repoRoot 'scripts/Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
$testRoot = Join-Path $ws.paths.build 'gacha-record-store-tests'
$project = Join-Path $PSScriptRoot 'GachaRecordStoreTests.csproj'
& $ws.tools.dotnet build $project -c Release "-p:BEWorkspaceBuildRoot=$testRoot" --nologo
if ($LASTEXITCODE -ne 0) { throw 'Gacha record store test build failed.' }
$bin = Join-Path $testRoot 'dotnet/GachaRecordStoreTests/AnyCPU/bin/Release/net9.0'
& $ws.tools.dotnet (Join-Path $bin 'GachaRecordStoreTests.dll')
if ($LASTEXITCODE -ne 0) { throw 'Gacha record store regression failed.' }
$obfuscated = Join-Path $testRoot 'obfuscated'
& (Join-Path $repoRoot 'scripts/ObfuscateNext.ps1') -AssemblyPath (Join-Path $bin 'GachaRecordStoreTests.dll') `
    -OutputDirectory $obfuscated -WorkspaceConfig $WorkspaceConfig
Copy-Item -LiteralPath (Join-Path $bin 'GachaRecordStoreTests.deps.json'),(Join-Path $bin 'GachaRecordStoreTests.runtimeconfig.json') `
    -Destination $obfuscated
& $ws.tools.dotnet (Join-Path $obfuscated 'GachaRecordStoreTests.dll') --expect-anonymous-failure
if ($LASTEXITCODE -ne 0) { throw 'Obfuscated gacha record store regression failed.' }

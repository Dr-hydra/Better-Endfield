[CmdletBinding()]
param([string]$BuildDirectory = '', [string]$DeployTo = '')
$ErrorActionPreference = 'Stop'
$repo = (Resolve-Path (Join-Path $PSScriptRoot '../../..')).Path
if (-not $BuildDirectory) { $BuildDirectory = Join-Path $repo 'build' }
& cmake --build $BuildDirectory --config Release --target BetterEndfield.CustomModel BetterEndfield.CustomModelBindingTests
if ($LASTEXITCODE -ne 0) { throw 'Probe build failed' }
& (Join-Path $BuildDirectory 'Release/BetterEndfield.CustomModelBindingTests.exe') --probe (Join-Path $BuildDirectory 'probe-checks')
if ($LASTEXITCODE -ne 0) { throw 'Probe checks failed' }
if ($DeployTo) {
    $dll = Join-Path $BuildDirectory 'stage/Release/modules/BetterEndfield.CustomModel.dll'
    if (-not (Test-Path -LiteralPath $dll)) { throw 'Check the configured CMake stage directory for the built DLL.' }
    Copy-Item -LiteralPath $dll -Destination (Join-Path $DeployTo 'modules/BetterEndfield.CustomModel.dll') -Force
}

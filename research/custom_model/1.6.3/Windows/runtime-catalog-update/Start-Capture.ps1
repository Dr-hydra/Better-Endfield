[CmdletBinding()]
param([string]$Run = '')

$ErrorActionPreference = 'Stop'
$repo = (Resolve-Path (Join-Path $PSScriptRoot '../../../../..')).Path
. (Join-Path $repo 'scripts/Workspace.ps1')
$workspace = Get-BEWorkspace
$controller = Join-Path $PSScriptRoot 'capture_control.py'
$injector = Join-Path $repo 'build/bem-capture-1.6.3/loaders/BetterEndfieldNext.Injector.exe'
$game = Join-Path $workspace.game.install_dir 'Endfield.exe'
$runtime = Join-Path $repo 'build/bem-capture-1.6.3'
$payload = Join-Path $runtime 'payloads/xinput1_4.dll'
$proxy = Join-Path $workspace.game.install_dir 'xinput1_4.dll'
$manifest = Join-Path $workspace.game.install_dir 'BetterEndfieldNext.xinput.install.json'
if (Get-Process Endfield -ErrorAction SilentlyContinue) {
    throw 'Endfield is still running. Finish shader compilation and exit normally first; this script never terminates it.'
}
foreach ($file in @($controller, $payload, $game)) {
    if (-not (Test-Path -LiteralPath $file -PathType Leaf)) { throw "Missing capture input: $file" }
}
if ((Test-Path -LiteralPath $proxy) -or (Test-Path -LiteralPath $manifest)) {
    throw 'An XInput proxy or ownership marker already exists. Inspect and clean up the previous capture before starting another.'
}
$armArgs = @($controller, 'arm')
if ($Run) { $armArgs += @('--run', $Run) }
& $workspace.tools.python @armArgs
if ($LASTEXITCODE -ne 0) { throw 'Capture could not be armed; game was not started.' }
try {
    # The current client requires its official launcher. This proxy is the
    # existing BE auto-load payload, not a replacement game launcher.
    [IO.File]::Copy($payload, $proxy, $false)
    # Keep the ownership record expected by XInputDeploymentService.UninstallAsync.
    $digest = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData([IO.File]::ReadAllBytes($payload)))
    [ordered]@{
        Product = 'BetterEndfieldNext.XInputProxy'
        ProxyFile = 'xinput1_4.dll'
        Sha256 = $digest
        InstallRoot = $runtime
        InstalledUtc = [DateTimeOffset]::UtcNow.ToString('o')
    } | ConvertTo-Json | Set-Content -LiteralPath $manifest -Encoding utf8
    Write-Output 'Capture is armed. Use the official Hypergryph launcher Start Game button; do not launch Endfield.exe directly.'
} catch {
    & $workspace.tools.python $controller stop
    & $workspace.tools.python $controller restore
    throw
}

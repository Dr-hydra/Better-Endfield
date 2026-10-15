$ErrorActionPreference = 'Stop'
$workspace = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '../../../../..')).Path
$live = Join-Path $workspace 'build\bem-chain-audit-4.0.3\live'
$injector = Join-Path $live 'runtime\loaders\BetterEndfieldNext.Injector.exe'
$game = 'G:\Arknights Endfield\Endfield.exe'
if (Get-Process -Name Endfield -ErrorAction SilentlyContinue) { throw 'The game is already running; no second process will be started.' }
foreach ($path in @($injector, $game, (Join-Path $live 'local\BetterEndfieldNext\catalog\custom-model\runtime.ini'))) {
    if (!(Test-Path -LiteralPath $path -PathType Leaf)) { throw "Missing prepared input: $path" }
}
if (Test-Path -LiteralPath 'G:\Arknights Endfield\xinput1_4.dll') { throw 'Existing XInput proxy conflicts with Injector; it will not be changed.' }
$principal = [Security.Principal.WindowsPrincipal]::new([Security.Principal.WindowsIdentity]::GetCurrent())
if (!$principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    $process = Start-Process -FilePath (Get-Process -Id $PID).Path -ArgumentList ('-NoProfile -File "' + $PSCommandPath + '"') -Verb RunAs -WindowStyle Hidden -PassThru
    [pscustomobject]@{ElevatedProcessId=$process.Id; LiveDirectory=$live} | ConvertTo-Json -Compress
    return
}
# Process-local override only. User's real BE settings and installed model library are untouched.
$savedLocal = $env:LOCALAPPDATA
try {
    $env:LOCALAPPDATA = Join-Path $live 'local'
    @{Phase='running'; Started=(Get-Date).ToString('o'); Injector=$injector} |
        ConvertTo-Json | Set-Content -LiteralPath (Join-Path $live 'status.json') -Encoding utf8
    & $injector --game $game *> (Join-Path $live 'injector.log')
    @{Phase='finished'; Finished=(Get-Date).ToString('o'); ExitCode=$LASTEXITCODE} |
        ConvertTo-Json | Set-Content -LiteralPath (Join-Path $live 'status.json') -Encoding utf8
} catch {
    @{Phase='failed'; Error=$_.Exception.Message} | ConvertTo-Json |
        Set-Content -LiteralPath (Join-Path $live 'status.json') -Encoding utf8
    throw
} finally { $env:LOCALAPPDATA = $savedLocal }

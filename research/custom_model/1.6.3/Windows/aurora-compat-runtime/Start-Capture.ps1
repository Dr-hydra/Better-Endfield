$ErrorActionPreference = 'Stop'
$repo = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '../../../../..')).Path
$auroraLive = Join-Path $repo 'build\aurora-compat-1.6.3\live'
$injector = Join-Path $auroraLive 'runtime\loaders\BetterEndfieldNext.Injector.exe'
$game = 'G:\Arknights Endfield\Endfield.exe'
if (Get-Process -Name Endfield -ErrorAction SilentlyContinue) { throw 'Game already running; no capture started.' }
if (Test-Path -LiteralPath 'G:\Arknights Endfield\xinput1_4.dll') { throw 'Existing XInput proxy conflicts with capture Injector; no files changed.' }
foreach ($path in @($injector, $game, (Join-Path $auroraLive 'local\BetterEndfieldNext\BetterEndfieldNext.ini'))) {
    if (!(Test-Path -LiteralPath $path -PathType Leaf)) { throw "Missing capture input: $path" }
}
$principal = [Security.Principal.WindowsPrincipal]::new([Security.Principal.WindowsIdentity]::GetCurrent())
if (!$principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    $process = Start-Process -FilePath (Get-Process -Id $PID).Path -ArgumentList ('-NoProfile -File "' + $PSCommandPath + '"') -Verb RunAs -WindowStyle Hidden -PassThru
    [pscustomobject]@{ElevatedProcessId=$process.Id; LiveDirectory=$auroraLive} | ConvertTo-Json -Compress
    return
}
$savedLocal = $env:LOCALAPPDATA
try {
    $env:LOCALAPPDATA = Join-Path $auroraLive 'local'
    @{Phase='running'; Started=(Get-Date).ToString('o'); Injector=$injector} |
        ConvertTo-Json | Set-Content -LiteralPath (Join-Path $auroraLive 'status.json') -Encoding utf8
    & $injector --game $game *> (Join-Path $auroraLive 'injector.log')
    @{Phase='finished'; Finished=(Get-Date).ToString('o'); ExitCode=$LASTEXITCODE} |
        ConvertTo-Json | Set-Content -LiteralPath (Join-Path $auroraLive 'status.json') -Encoding utf8
} catch {
    @{Phase='failed'; Error=$_.Exception.Message} | ConvertTo-Json |
        Set-Content -LiteralPath (Join-Path $auroraLive 'status.json') -Encoding utf8
    throw
} finally { $env:LOCALAPPDATA = $savedLocal }

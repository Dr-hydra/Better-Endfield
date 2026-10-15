param(
    [ValidateSet('Probe', 'Game')][string]$Mode = 'Game',
    [string]$ParentPath = 'D:\Hypergryph Launcher\1.6.0\Games.exe',
    [string]$GamePath = 'G:\Arknights Endfield\Endfield.exe',
    [string]$RunDirectory = ''
)
$ErrorActionPreference = 'Stop'
$workspace = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..\..\..\..\..')).Path
$bridge = Join-Path $workspace 'build\launcher-helper-1.6.3\Release\LaunchBridge.exe'
if (!(Test-Path -LiteralPath $bridge -PathType Leaf)) { throw "Build LaunchBridge first: $bridge" }
if (!(Test-Path -LiteralPath $ParentPath -PathType Leaf)) { throw "Official launcher missing: $ParentPath" }
if ($Mode -eq 'Game' -and (Get-Process -Name Endfield -ErrorAction SilentlyContinue)) {
    throw 'Endfield is already running. Close it normally before using this entry.'
}
if (!$RunDirectory) {
    $RunDirectory = Join-Path $PSScriptRoot ('runs\' + $Mode.ToLowerInvariant() + '-' + (Get-Date -Format 'yyyyMMdd-HHmmss'))
}
$RunDirectory = [IO.Path]::GetFullPath($RunDirectory)
New-Item -ItemType Directory -Force -Path $RunDirectory | Out-Null
$principal = [Security.Principal.WindowsPrincipal]::new([Security.Principal.WindowsIdentity]::GetCurrent())
if (!$principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    $forwarded = @('-NoProfile', '-File', $PSCommandPath, '-Mode', $Mode,
        '-ParentPath', $ParentPath, '-GamePath', $GamePath, '-RunDirectory', $RunDirectory)
    # File-system paths cannot contain quotes; pass every argument literally.
    $quoted = foreach ($item in $forwarded) {
        if ($item.Contains('"')) { throw 'Invalid quote in launch argument.' }
        '"' + $item + '"'
    }
    $elevated = Start-Process -FilePath (Get-Process -Id $PID).Path -ArgumentList ($quoted -join ' ') -Verb RunAs -WindowStyle Hidden -PassThru
    [pscustomobject]@{ ElevatedProcessId = $elevated.Id; Mode = $Mode; RunDirectory = $RunDirectory } | ConvertTo-Json -Compress
    return
}

$statePath = Join-Path $RunDirectory 'status.json'
try {
    $signature = Get-AuthenticodeSignature -LiteralPath $ParentPath
    if ($signature.Status -ne 'Valid') { throw "Official parent signature is not valid: $($signature.Status)" }
    $arguments = @('--parent', $ParentPath)
    if ($Mode -eq 'Probe') {
        $fixturePath = Join-Path $RunDirectory 'fixture.json'
        @{ checkItems = @(@{ name = 'Launcher'; parentNames = @([IO.Path]::GetFileName($ParentPath));
            fingerprints = @($signature.SignerCertificate.Thumbprint) }) } |
            ConvertTo-Json -Depth 5 | Set-Content -LiteralPath $fixturePath -Encoding utf8
        $python = (Get-Command python.exe -CommandType Application | Select-Object -First 1).Source
        $arguments += @('--game', $python, '--', (Join-Path $PSScriptRoot 'probe_checker.py'),
            '--dll', (Join-Path (Split-Path -Parent $GamePath) 'OneChannelLC.dll'),
            '--config', $fixturePath, '--output', (Join-Path $RunDirectory 'checker-result.json'))
    } else {
        if (!(Test-Path -LiteralPath $GamePath -PathType Leaf)) { throw "Game missing: $GamePath" }
        $arguments += @('--game', $GamePath)
    }
    @{ Phase = 'running'; Mode = $Mode; WrapperPid = $PID; Started = (Get-Date).ToString('o') } |
        ConvertTo-Json | Set-Content -LiteralPath $statePath -Encoding utf8
    & $bridge @arguments *> (Join-Path $RunDirectory 'bridge.log')
    $result = $LASTEXITCODE
    @{ Phase = 'finished'; Mode = $Mode; ExitCode = $result; Finished = (Get-Date).ToString('o') } |
        ConvertTo-Json | Set-Content -LiteralPath $statePath -Encoding utf8
    exit $result
} catch {
    @{ Phase = 'failed'; Mode = $Mode; Error = $_.Exception.Message } |
        ConvertTo-Json | Set-Content -LiteralPath $statePath -Encoding utf8
    throw
}

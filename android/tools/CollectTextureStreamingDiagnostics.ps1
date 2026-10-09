#requires -Version 7.0
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [ValidateNotNullOrEmpty()]
    [string]$Serial,

    [Parameter(Mandatory = $true)]
    [ValidateSet('baseline', 'models-enabled', 'models-disabled')]
    [string]$Stage,

    [string]$Package = 'com.hypergryph.endfield',
    [string]$OutputDirectory = '',
    [string]$Adb = '',
    [switch]$IncludePrivateRuntimeLog
)

$ErrorActionPreference = 'Stop'
$taskRepo = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
if (-not $Adb) {
    $Adb = Join-Path $taskRepo 'toolchains/android/sdk/platform-tools/adb.exe'
}
if (-not (Test-Path -LiteralPath $Adb -PathType Leaf)) {
    throw "ADB not found: $Adb"
}
if ($Package -notmatch '^[A-Za-z][A-Za-z0-9_]*(\.[A-Za-z][A-Za-z0-9_]*)+$') {
    throw 'Package must be an Android application package name.'
}
$deviceState = & $Adb -s $Serial get-state 2>&1
if ($LASTEXITCODE -ne 0 -or ($deviceState -join '').Trim() -ne 'device') {
    throw "Device $Serial is not connected and authorized."
}
if (-not $OutputDirectory) {
    $taskTimestamp = [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss-fff')
    $OutputDirectory = Join-Path $taskRepo "build/android-issues-25-26/device/$taskTimestamp-$Stage"
}
$OutputDirectory = [System.IO.Path]::GetFullPath($OutputDirectory)
New-Item -ItemType Directory -Force -Path $OutputDirectory | Out-Null

function Save-TaskAdbOutput {
    param([string[]]$Arguments, [string]$Path)
    $taskProcessInfo = [System.Diagnostics.ProcessStartInfo]::new()
    $taskProcessInfo.FileName = (Resolve-Path -LiteralPath $Adb).Path
    $taskProcessInfo.UseShellExecute = $false
    $taskProcessInfo.CreateNoWindow = $true
    $taskProcessInfo.RedirectStandardOutput = $true
    $taskProcessInfo.RedirectStandardError = $true
    foreach ($taskArgument in $Arguments) { $taskProcessInfo.ArgumentList.Add($taskArgument) }
    $taskProcess = [System.Diagnostics.Process]::new()
    $taskProcess.StartInfo = $taskProcessInfo
    $taskOutputStream = $null
    try {
        if (-not $taskProcess.Start()) { throw 'Unable to start ADB.' }
        $taskErrorRead = $taskProcess.StandardError.ReadToEndAsync()
        $taskOutputStream = [System.IO.File]::Create($Path)
        # Preserve UTF-8 bytes from Android; Windows native-output decoding can
        # corrupt Chinese log/model names before a PowerShell redirection.
        $taskProcess.StandardOutput.BaseStream.CopyTo($taskOutputStream)
        $taskOutputStream.Dispose()
        $taskOutputStream = $null
        $taskProcess.WaitForExit()
        $taskErrorText = $taskErrorRead.GetAwaiter().GetResult()
        if ($taskErrorText) {
            [System.IO.File]::WriteAllText($Path + '.stderr.txt', $taskErrorText, [System.Text.UTF8Encoding]::new($false))
        }
        return $taskProcess.ExitCode
    } finally {
        if ($taskOutputStream) { $taskOutputStream.Dispose() }
        $taskProcess.Dispose()
    }
}

# Read-only snapshots. Do not clear logcat, launch/stop the game, install an APK,
# or toggle models. Private cache access via su is explicitly opt-in.
$logPath = Join-Path $OutputDirectory 'betterendfield-logcat.txt'
$logExit = Save-TaskAdbOutput -Arguments @('-s', $Serial, 'logcat', '-d', '-v', 'threadtime', '-t', '3000', 'BetterEndfieldNext:V', '*:S') -Path $logPath
$memoryPath = Join-Path $OutputDirectory 'game-meminfo.txt'
$memoryExit = Save-TaskAdbOutput -Arguments @('-s', $Serial, 'shell', 'dumpsys', 'meminfo', $Package) -Path $memoryPath
$processPath = Join-Path $OutputDirectory 'game-pid.txt'
$processExit = Save-TaskAdbOutput -Arguments @('-s', $Serial, 'shell', 'pidof', $Package) -Path $processPath
$privateExit = $null
$gameOverlayExit = $null
$ownerOverlayExit = $null
if ($IncludePrivateRuntimeLog) {
    $taskPrivateCommand = 'su -c "cat /data/user/0/' + $Package + '/cache/betterendfield-diagnostics.log"'
    $privateExit = Save-TaskAdbOutput -Arguments @('-s', $Serial, 'shell', $taskPrivateCommand) -Path (Join-Path $OutputDirectory 'game-native-diagnostics.log')
    $taskOverlayCommand = 'su -c "cat /data/user/0/' + $Package + '/cache/betterendfield-overlay-settings.log"'
    $gameOverlayExit = Save-TaskAdbOutput -Arguments @('-s', $Serial, 'shell', $taskOverlayCommand) -Path (Join-Path $OutputDirectory 'game-overlay-settings.log')
    $ownerOverlayExit = Save-TaskAdbOutput -Arguments @('-s', $Serial, 'shell', 'su -c "cat /data/user/0/dev.betterendfield.next/cache/betterendfield-overlay-settings.log"') -Path (Join-Path $OutputDirectory 'owner-overlay-settings.log')
}

[ordered]@{
    schema_version = 1
    collected_at_utc = [DateTime]::UtcNow.ToString('o')
    serial = $Serial
    stage = $Stage
    package = $Package
    logcat_exit_code = $logExit
    meminfo_exit_code = $memoryExit
    pidof_exit_code = $processExit
    private_runtime_log_requested = [bool]$IncludePrivateRuntimeLog
    private_runtime_log_exit_code = $privateExit
    game_overlay_log_exit_code = $gameOverlayExit
    owner_overlay_log_exit_code = $ownerOverlayExit
    note = 'Stage is supplied by the operator; no model state or GPU peak is inferred from this snapshot.'
} | ConvertTo-Json | Set-Content -LiteralPath (Join-Path $OutputDirectory 'collection.json') -Encoding utf8

Write-Output "Diagnostics saved: $OutputDirectory"
if ($processExit -ne 0) {
    Write-Warning 'The game process was not found; meminfo is not a measurement of a running game.'
}
if ($logExit -ne 0 -or $memoryExit -ne 0) {
    throw 'A diagnostic command failed. See collection.json and the captured command output.'
}
if ($IncludePrivateRuntimeLog -and $privateExit -ne 0) {
    Write-Warning 'Private runtime log access failed; see the captured su output. No settings were changed.'
}

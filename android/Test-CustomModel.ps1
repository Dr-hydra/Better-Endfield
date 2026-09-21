param(
    [Parameter(Mandatory=$true)][string]$Serial,
    [ValidateSet('MeshData','Rollback','Replace')][string]$Mode = 'MeshData',
    [ValidatePattern('^/data/local/tmp/[a-zA-Z0-9_.-]+\.bem$')][string]$DevicePackage = '/data/local/tmp/e.bem',
    [string]$DevicePackages,
    [string]$Adb = 'adb',
    [ValidateRange(30,300)][int]$TimeoutSeconds = 180,
    [switch]$SkipInstall,
    [switch]$PipelineLod,
    [switch]$NpcParameters,
    [switch]$Inspect
)
$ErrorActionPreference = 'Stop'
$taskPackageList = if ($DevicePackages) { @($DevicePackages -split ',' | ForEach-Object { $_.Trim() } | Where-Object { $_ }) } else { @() }
$taskRepo = Split-Path $PSScriptRoot -Parent
$taskOutput = Join-Path $taskRepo ('artifacts/android-refactor/model-' + $Mode.ToLowerInvariant() + '-' + (Get-Date -Format 'yyyyMMdd-HHmmss'))
New-Item -ItemType Directory -Force $taskOutput | Out-Null
$taskApk = Join-Path $PSScriptRoot 'app/build/outputs/apk/modern/debug/app-modern-debug.apk'
function Invoke-TaskAdb {
    # Some emulator commands (notably `monkey`) write harmless status text to
    # stderr even when they succeed. Keep the explicit exit-code check without
    # letting PowerShell's Stop preference turn that text into a test failure.
    $taskOldErrorAction = $ErrorActionPreference
    $ErrorActionPreference = 'Continue'
    try { $taskResult = & $Adb -s $Serial @args 2>&1 }
    finally { $ErrorActionPreference = $taskOldErrorAction }
    if ($LASTEXITCODE -ne 0) { throw "ADB failed: $taskResult" }
    return $taskResult
}
if ((Invoke-TaskAdb shell id -u).Trim() -ne '0') { throw 'Run adb root on this emulator first.' }
if (-not $taskPackageList) { Invoke-TaskAdb shell test -r $DevicePackage | Out-Null }
if ($taskPackageList) {
    foreach ($taskPackage in $taskPackageList) { Invoke-TaskAdb shell test -r $taskPackage | Out-Null }
}
if (-not $SkipInstall) { Invoke-TaskAdb install -r $taskApk | Out-Null }
$taskPrevious = [string](Invoke-TaskAdb shell getprop debug.betterendfield.custom_model_config)
$taskConfig = if ($taskPackageList) {
    'resource=auto;packages=' + (($taskPackageList | ForEach-Object { Split-Path $_ -Leaf }) -join ',')
} else { 'resource=auto;package=' + $DevicePackage }
if ($PipelineLod) { $taskConfig += ';lod_pipeline=1' }
if ($NpcParameters) {
    if (-not $PipelineLod) { throw 'NpcParameters requires PipelineLod.' }
    $taskConfig += ';lod_npc=1'
}
if ($Inspect) { $taskConfig += ';inspect=1' }
$taskExpected = switch ($Mode) {
    'MeshData' { $taskConfig += ';mesh_data_probe=2'; 'Android MeshData scratch builder PASS' }
    'Rollback' { $taskConfig += ';replace=1;rollback=1'; 'Android paired world/UI validation PASS' }
    'Replace' { $taskConfig += ';replace=1'; 'Android paired world/UI publication PASS' }
}
if ($taskConfig.Length -gt 91) { throw 'Android debug property exceeds its 91-byte limit; use a shorter device BEM filename.' }
# Do not pass untrusted prior property text through a shell when restoring it.
if ($taskPrevious -match '[^a-zA-Z0-9_./,;=:+-]') { throw 'Existing property needs manual preservation before this test.' }
$taskPassed = $false
Write-Output "Starting $Mode test; enter the game to trigger character resource delivery."
if ($Mode -eq 'Rollback') {
    Write-Output 'Rollback mode restores original models immediately; it must not be used for visual replacement validation.'
}
try {
    Invoke-TaskAdb shell "setprop debug.betterendfield.custom_model_config '$taskConfig'" | Out-Null
    Invoke-TaskAdb shell am force-stop com.hypergryph.endfield | Out-Null
    Invoke-TaskAdb shell monkey -p com.hypergryph.endfield 1 | Out-Null
    $taskDeadline = (Get-Date).AddSeconds($TimeoutSeconds)
    $taskPid = ''
    do {
        Start-Sleep -Seconds 3
        $taskFoundPid = [string](& $Adb -s $Serial shell pidof com.hypergryph.endfield)
        if ($LASTEXITCODE -ne 0 -or $taskFoundPid -notmatch '^\d+$') { throw 'Game process exited.' }
        if ($taskPid -and $taskPid -ne $taskFoundPid) { throw 'Game process restarted during test.' }
        $taskPid = $taskFoundPid
        $taskLines = Invoke-TaskAdb logcat -d "--pid=$taskPid" -s BetterEndfield:V AndroidRuntime:E libc:F
        $taskLines | Set-Content -LiteralPath (Join-Path $taskOutput 'runtime.log') -Encoding utf8
        $taskText = $taskLines -join "`n"
        if ($taskText -match 'Fatal signal|FATAL EXCEPTION|commit/restore FAIL|scratch builder FAIL|CRITICAL:') {
            throw "Runtime failure; see $taskOutput"
        }
        if ($taskText.Contains($taskExpected) -and
                ($Mode -eq 'MeshData' -or
                    ($NpcParameters -and $taskText.Contains('Android pipeline LOD bias + NPC parameters applied/readback PASS; QualitySettings unchanged')) -or
                    ($PipelineLod -and -not $NpcParameters -and $taskText.Contains('Android pipeline LOD bias applied; quality/NPC/camera culling unchanged')) -or
                    (-not $PipelineLod -and $taskText.Contains('global LOD/culling overrides disabled')))) {
            $taskPassed = $true; break
        }
    } while ((Get-Date) -lt $taskDeadline)
    if (-not $taskPassed) { throw "No '$taskExpected' evidence before timeout. See $taskOutput" }
    # Confirm the same process survives after the passing marker.
    Start-Sleep -Seconds 10
    if (([string](Invoke-TaskAdb shell pidof com.hypergryph.endfield)) -ne $taskPid) {
        $taskPassed = $false; throw 'Game did not survive the post-test observation.'
    }
    $taskFinal = Invoke-TaskAdb logcat -d "--pid=$taskPid" -s BetterEndfield:V AndroidRuntime:E libc:F
    $taskFinal | Set-Content -LiteralPath (Join-Path $taskOutput 'runtime.log') -Encoding utf8
    if (($taskFinal -join "`n") -match 'Fatal signal|FATAL EXCEPTION|commit/restore FAIL|CRITICAL:') {
        $taskPassed = $false; throw 'Runtime failure after the passing marker.'
    }
    Write-Output "PASS $Mode pid=$taskPid evidence=$taskOutput"
    Write-Output 'This is a data/binding test. Visual verification remains separate.'
    if ($Mode -eq 'Replace') { Write-Output 'Replacement remains enabled for visual validation. Only Android world LOD1 is currently bound.' }
} finally {
    if ($Mode -ne 'Replace' -or -not $taskPassed) {
        & $Adb -s $Serial shell am force-stop com.hypergryph.endfield | Out-Null
        & $Adb -s $Serial shell "setprop debug.betterendfield.custom_model_config '$taskPrevious'" | Out-Null
    }
}

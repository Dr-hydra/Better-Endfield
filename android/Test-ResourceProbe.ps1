param(
    [Parameter(Mandatory=$true)][string]$Serial,
    [string]$Adb = 'adb',
    [ValidatePattern('^[a-zA-Z][a-zA-Z0-9_.]+$')][string]$Package = 'com.hypergryph.endfield',
    [ValidateRange(10,60)][int]$Seconds = 30
)
$ErrorActionPreference = 'Stop'
$taskRepo = Split-Path $PSScriptRoot -Parent
$taskOutput = Join-Path $taskRepo ('artifacts/android-refactor/probe-' + (Get-Date -Format 'yyyyMMdd-HHmmss'))
New-Item -ItemType Directory -Force $taskOutput | Out-Null
$taskApk = Join-Path $PSScriptRoot 'app/build/outputs/apk/modern/debug/app-modern-debug.apk'
if (-not (Test-Path -LiteralPath $taskApk)) { throw 'Build :app:assembleModernDebug first.' }
& $Adb -s $Serial get-state
if ($LASTEXITCODE -ne 0) { throw 'ADB target unavailable.' }
$taskUid = & $Adb -s $Serial shell id -u
if ($LASTEXITCODE -ne 0 -or "$taskUid".Trim() -ne '0') {
    throw 'This diagnostic capture needs root ADB. Run adb -s <serial> root first.'
}
# Installation is deliberately outside the launch block: no game is started if it fails.
& $Adb -s $Serial install -r $taskApk
if ($LASTEXITCODE -ne 0) { throw 'APK installation failed.' }
try {
    & $Adb -s $Serial shell "am force-stop $Package"
    & $Adb -s $Serial shell setprop debug.betterendfield.resource_probe 1
    if ($LASTEXITCODE -ne 0) { throw 'Unable to enable the debug probe.' }
    & $Adb -s $Serial shell "monkey -p $Package 1" > (Join-Path $taskOutput 'launch.log') 2>&1
    if ($LASTEXITCODE -ne 0) { throw 'Unable to launch game.' }
    Start-Sleep -Seconds $Seconds
    & $Adb -s $Serial logcat -d -t 4000 > (Join-Path $taskOutput 'logcat.log')
    & $Adb -s $Serial shell "cat /data/user/0/$Package/cache/betterendfield-diagnostics.log" > (Join-Path $taskOutput 'runtime.log') 2>&1
    & $Adb -s $Serial shell 'cat /data/adb/lspd/log/modules*' > (Join-Path $taskOutput 'framework.log') 2>&1
} finally {
    & $Adb -s $Serial shell "am force-stop $Package"
    if ($LASTEXITCODE -ne 0) { Write-Warning 'Game stop failed; check the ADB connection and stop it manually.' }
    & $Adb -s $Serial shell setprop debug.betterendfield.resource_probe 0
}
Write-Output "Probe evidence: $taskOutput"
Write-Output 'Inspect current process timestamps: logs may also contain earlier runs. Log capture alone is not a passing test.'

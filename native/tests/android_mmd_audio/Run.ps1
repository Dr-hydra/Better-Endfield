param(
    [string]$JavaCompiler = 'javac',
    [string]$Java = 'java',
    [string[]]$Cases = @()
)
$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '../../..')).Path
$productionAudio = Join-Path $repoRoot 'android/app/src/main/java/dev/betterendfield/android/MmdAudio.java'
$testSources = @(Get-ChildItem -LiteralPath (Join-Path $PSScriptRoot 'stubs'), (Join-Path $PSScriptRoot 'src') -Filter '*.java' -Recurse | ForEach-Object { $_.FullName })
$audioTestOutput = Join-Path ([IO.Path]::GetTempPath()) ('be-mmd-audio-jvm-' + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $audioTestOutput | Out-Null
$audioSnapshot = Join-Path $audioTestOutput 'MmdAudio.java'
Copy-Item -LiteralPath $productionAudio -Destination $audioSnapshot
Write-Output ('Production source SHA256: ' + (Get-FileHash -LiteralPath $audioSnapshot -Algorithm SHA256).Hash)
& $JavaCompiler -encoding UTF-8 --release 17 -d $audioTestOutput $audioSnapshot @testSources
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
if ($Cases.Count -eq 0) {
    $Cases = @(& $Java -cp $audioTestOutput dev.betterendfield.android.MmdAudioJvmTest --list)
    if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
}
$failed = @()
foreach ($case in $Cases) {
    & $Java -cp $audioTestOutput dev.betterendfield.android.MmdAudioJvmTest $case
    if ($LASTEXITCODE -ne 0) { $failed += $case }
}
Write-Output ('Audio JVM cases: ' + ($Cases.Count - $failed.Count) + '/' + $Cases.Count + ' passed')
Write-Output ('Compiled test classes: ' + $audioTestOutput)
if ($failed.Count -gt 0) {
    Write-Output ('Failing cases: ' + ($failed -join ', '))
    exit 1
}

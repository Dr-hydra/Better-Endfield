param(
    [string]$JavaCompiler = 'javac',
    [string]$Java = 'java'
)
$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '../../..')).Path
$mediaOutput = Join-Path $repoRoot 'build/next/media-screen/android-tests'
New-Item -ItemType Directory -Path $mediaOutput -Force | Out-Null
$mediaJar = Join-Path $repoRoot 'toolchains/testing/android/json-20250517.jar'
$mediaSources = @(Get-ChildItem -LiteralPath (Join-Path $PSScriptRoot 'stubs'), (Join-Path $PSScriptRoot 'src') -Filter '*.java' -Recurse | ForEach-Object FullName)
$mediaProduction = Join-Path $repoRoot 'android/app/src/main/java/dev/betterendfield/next/MediaScreenFiles.java'
& $JavaCompiler -encoding UTF-8 --release 17 -cp $mediaJar -d $mediaOutput $mediaProduction @mediaSources
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
& $Java -cp ($mediaOutput + ';' + $mediaJar) dev.betterendfield.next.MediaScreenFilesTest (Join-Path $mediaOutput 'fixtures')
exit $LASTEXITCODE

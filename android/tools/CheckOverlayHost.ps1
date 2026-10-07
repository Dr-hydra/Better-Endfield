param(
    [Parameter(Mandatory=$true)][string]$MainClasses,
    [Parameter(Mandatory=$true)][string]$AndroidJar,
    [Parameter(Mandatory=$true)][string]$ServiceJar,
    [Parameter(Mandatory=$true)][string]$JsonJar,
    [string]$WorkspaceConfig = ''
)
$ErrorActionPreference = 'Stop'
$overlayAndroid = Split-Path -Parent $PSScriptRoot
. (Join-Path (Split-Path -Parent $overlayAndroid) 'scripts/Workspace.ps1')
$overlayWorkspace = Get-BEWorkspace -Config $WorkspaceConfig
Set-BEWorkspaceEnvironment $overlayWorkspace
$overlayMain = Join-Path $overlayAndroid 'app/src/main/java/dev/betterendfield/android'
$overlayTests = Join-Path $overlayAndroid 'app/src/test/java/dev/betterendfield/android'
$overlayHostTests = Join-Path $overlayAndroid 'app/src/testHost/java/dev/betterendfield/android'
$overlayOutput = Join-Path $overlayWorkspace.paths.build 'tests/android/overlay-host'
New-Item -ItemType Directory -Force -Path $overlayOutput | Out-Null
$overlayClasspath = @($overlayOutput, $JsonJar, $MainClasses, $ServiceJar, $AndroidJar) -join ';'
$overlaySources = @(
    (Join-Path $overlayAndroid 'app/src/testHost/java/android/content/Context.java'),
    (Join-Path $overlayMain 'OverlayWritePolicy.java'),
    (Join-Path $overlayMain 'OverlayModelCatalogState.java'),
    (Join-Path $overlayMain 'OverlayReconnectPolicy.java'),
    (Join-Path $overlayMain 'BemOptions.java'),
    (Join-Path $overlayMain 'BemParameters.java'),
    (Join-Path $overlayMain 'BemHotSwitchUpdate.java'),
    (Join-Path $overlayTests 'OverlayWritePolicyTest.java'),
    (Join-Path $overlayHostTests 'ModuleSettingsFovTest.java'),
    (Join-Path $overlayHostTests 'BemOverlayPreparationTest.java'),
    (Join-Path $overlayHostTests 'OverlayModelCatalogStateTest.java'),
    (Join-Path $overlayHostTests 'OverlayReconnectPolicyTest.java'),
    (Join-Path $overlayTests 'BemHotSwitchUpdateTest.java')
)
& javac -encoding UTF-8 --release 17 -proc:none -cp $overlayClasspath -d $overlayOutput @overlaySources
if ($LASTEXITCODE -ne 0) { throw 'Overlay host regression compilation failed' }
foreach ($overlayTest in @('OverlayWritePolicyTest', 'ModuleSettingsFovTest', 'BemOverlayPreparationTest', 'BemHotSwitchUpdateTest')) {
    & java -cp $overlayClasspath "dev.betterendfield.android.$overlayTest" $overlayOutput
    if ($LASTEXITCODE -ne 0) { throw "Overlay regression failed: $overlayTest" }
}
# This test has an optional saved-index file argument, not an output directory.
# Its normal regression coverage uses an independent synthetic catalog.
& java -cp $overlayClasspath 'dev.betterendfield.android.OverlayModelCatalogStateTest'
if ($LASTEXITCODE -ne 0) { throw 'Overlay regression failed: OverlayModelCatalogStateTest' }
& java -cp $overlayClasspath 'dev.betterendfield.android.OverlayReconnectPolicyTest'
if ($LASTEXITCODE -ne 0) { throw 'Overlay regression failed: OverlayReconnectPolicyTest' }

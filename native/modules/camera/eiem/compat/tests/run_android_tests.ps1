param(
    [string]$HostCxx = (Get-Command g++ -ErrorAction Stop).Source,
    [string]$NdkClang = ""
)
$ErrorActionPreference = 'Stop'
$taskRoot = (Resolve-Path (Join-Path $PSScriptRoot '../../../../../..')).Path
$previousLocation = Get-Location
try {
    Set-Location -LiteralPath $taskRoot
    if (!$NdkClang) {
        $NdkClang = (Get-ChildItem -LiteralPath 'tools/android-toolchain/sdk/ndk' -Filter clang++.exe -Recurse | Select-Object -First 1).FullName
    }
    if (!$NdkClang) { throw 'Android NDK clang++ missing' }
    $includes = @('-Inative/shared/include','-Inative/modules/camera/eiem','-Inative/modules/camera/eiem/upstream','-Inative/modules/camera/eiem/compat')
    $base = 'native/modules/camera/eiem'
    foreach ($name in @('eiem_slot0.cpp','eiem_slot1.cpp','eiem_slot2.cpp','eiem_slot3.cpp','eiem_body.cpp')) {
        & $NdkClang '--target=aarch64-linux-android24' '-std=c++20' '-fno-char8_t' '-fsyntax-only' '-Wall' '-Wextra' '-Werror' '-Wno-unused-function' @includes "$base/$name"
        if ($LASTEXITCODE) { throw "NDK syntax failed: $name" }
    }
    $buildDir = Join-Path $PSScriptRoot '.build'
    New-Item -ItemType Directory -Force -Path $buildDir | Out-Null
    $testFlags = @('-std=c++17','-D__ANDROID__','-DBE_EIEM_ANDROID_OFFLINE_TEST','-static') + $includes
    $single = Join-Path $buildDir 'android_slot_tests.exe'
    & $HostCxx @testFlags "$base/compat/tests/android_slot_tests.cpp" '-o' $single
    if ($LASTEXITCODE) { throw 'Single-slot fixture build failed' }
    & $single
    if ($LASTEXITCODE) { throw 'Single-slot fixture failed' }
    $multi = Join-Path $buildDir 'android_multislot_tests.exe'
    & $HostCxx @testFlags "$base/compat/tests/android_multislot_tests.cpp" "$base/eiem_slot1.cpp" "$base/eiem_slot2.cpp" "$base/eiem_slot3.cpp" "$base/eiem_body.cpp" '-o' $multi
    if ($LASTEXITCODE) { throw 'Four-slot fixture build failed' }
    & $multi
    if ($LASTEXITCODE) { throw 'Four-slot fixture failed' }
} finally {
    Set-Location -LiteralPath $previousLocation.Path
}

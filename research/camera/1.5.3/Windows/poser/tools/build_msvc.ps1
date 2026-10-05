param([switch]$RunTests, [switch]$EnableXxmiBridge)

$ErrorActionPreference = 'Stop'

# Endfield Poser - cmake-free MSVC build.
#
# Why this exists:
#   * Uses installed MSVC and Windows SDK. An optional SDK fallback uses NuGet:
#       - deps\winsdk    (Microsoft.Windows.SDK.CPP  -> c\Include\<ver>\{um,shared,ucrt})
#       - deps\winsdkcpp (Microsoft.Windows.SDK.CPP.x64 -> c\um\x64, c\ucrt\x64)
#   * Initializes only the native x64 compiler environment, avoiding slow
#     cmd AutoRun hooks during the nested commands inside vcvars64.bat.
#
# Produces:
#   plugin\poser.dll            (main plugin, self-initializing)
#   plugin\d3dcompiler_47.dll   (DX proxy loader, forwards to System32)
#   plugin\vulkan-1.dll         (Vulkan proxy loader, forwards to System32)
# -EnableXxmiBridge writes the optional variant to build\xxmi\plugin instead.
# -RunTests also builds and runs private local tests, when available.

$root = Join-Path $PSScriptRoot '..'
Set-Location $root
$pluginDir = if ($EnableXxmiBridge) { 'build\xxmi\plugin' } else { 'plugin' }
$bridgeFlag = if ($EnableXxmiBridge) { 1 } else { 0 }
Write-Host "XXMI bridge: $EnableXxmiBridge; output: $pluginDir"
if ($RunTests -and -not (Test-Path -LiteralPath 'tests\test_quat.cpp')) {
  throw 'Local tests are unavailable. Omit -RunTests for a public source build.'
}

# ---- 1) Locate MSVC toolchain (vcvars64.bat) ----
# Probe the usual install roots first (any edition / any VS version folder,
# including Insiders + BuildTools), then fall back to vswhere.
$vcvars = $null
$vsRoots = @(
  'C:\Program Files\Microsoft Visual Studio',
  'C:\Program Files (x86)\Microsoft Visual Studio'
)
foreach ($vsRoot in $vsRoots) {
  if (-not (Test-Path $vsRoot)) { continue }
  $hit = Get-ChildItem -Path (Join-Path $vsRoot '*\*\VC\Auxiliary\Build\vcvars64.bat') -ErrorAction SilentlyContinue |
    Sort-Object FullName -Descending | Select-Object -First 1
  if ($hit) { $vcvars = $hit.FullName; break }
}
if (-not $vcvars) {
  $vswhere = 'C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe'
  if (Test-Path $vswhere) {
    $vsDir = & $vswhere -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath
    if ($vsDir) {
      $candidate = Join-Path $vsDir 'VC\Auxiliary\Build\vcvars64.bat'
      if (Test-Path $candidate) { $vcvars = $candidate }
    }
  }
}
if (-not $vcvars) { throw "vcvars64.bat not found (Visual Studio C++ tools missing?)" }
Write-Host "Using $vcvars"
$vcRoot = Split-Path (Split-Path (Split-Path $vcvars -Parent) -Parent) -Parent
$vcVersion = (Get-Content (Join-Path $vcRoot 'Auxiliary\Build\Microsoft.VCToolsVersion.default.txt') -Raw).Trim()
$vcToolsDir = Join-Path $vcRoot ('Tools\MSVC\' + $vcVersion)
$compiler = Join-Path $vcToolsDir 'bin\Hostx64\x64\cl.exe'
if (-not (Test-Path -LiteralPath $compiler)) { throw "x64 compiler not found: $compiler" }


# ---- 2) Prefer installed Windows SDK; retain the NuGet fallback ----
$sdkRoot = 'C:\Program Files (x86)\Windows Kits\10'
$sdkVerDir = Get-ChildItem (Join-Path $sdkRoot 'Include') -Directory -ErrorAction SilentlyContinue |
  Where-Object { (Test-Path (Join-Path $_.FullName 'um\Windows.h')) -and (Test-Path (Join-Path $sdkRoot ('Lib\' + $_.Name + '\um\x64\kernel32.lib'))) } |
  Sort-Object Name -Descending | Select-Object -First 1
if ($sdkVerDir) {
  $sdkLibDirUm = Join-Path $sdkRoot ('Lib\' + $sdkVerDir.Name + '\um\x64')
  $sdkLibDirUcrt = Join-Path $sdkRoot ('Lib\' + $sdkVerDir.Name + '\ucrt\x64')
} else {
  $sdkVerDir = Get-ChildItem (Join-Path $root 'deps\winsdk\c\Include') -Directory -ErrorAction SilentlyContinue | Sort-Object Name -Descending | Select-Object -First 1
  $sdkLibDirUm = Join-Path $root 'deps\winsdkcpp\c\um\x64'
  $sdkLibDirUcrt = Join-Path $root 'deps\winsdkcpp\c\ucrt\x64'
}
if (-not $sdkVerDir) { throw 'Windows SDK not found; install the C++ Windows SDK component or run tools\setup_winsdk.ps1.' }
$sdkInc = @((Join-Path $sdkVerDir.FullName 'um'),(Join-Path $sdkVerDir.FullName 'shared'),(Join-Path $sdkVerDir.FullName 'ucrt'),(Join-Path $sdkVerDir.FullName 'winrt'))
foreach ($sdkPath in @($sdkInc) + @($sdkLibDirUm,$sdkLibDirUcrt)) { if (-not (Test-Path -LiteralPath $sdkPath)) { throw "Missing SDK path: $sdkPath" } }
$env:INCLUDE = (Join-Path $vcToolsDir 'include') + ';' + ($sdkInc -join ';')
$env:LIB = (Join-Path $vcToolsDir 'lib\x64') + ';' + $sdkLibDirUm + ';' + $sdkLibDirUcrt
$sdkBin = Join-Path $sdkRoot ('bin\' + $sdkVerDir.Name + '\x64')
if (-not (Test-Path (Join-Path $sdkBin 'rc.exe'))) {
  $sdkBin = Join-Path $root ('deps\winsdk\c\bin\' + $sdkVerDir.Name + '\x64')
}
$env:PATH = (Split-Path $compiler -Parent) + ';' + $sdkBin + ';' + $env:PATH

New-Item -ItemType Directory -Force -Path $pluginDir | Out-Null
New-Item -ItemType Directory -Force -Path 'build\tests' | Out-Null
New-Item -ItemType Directory -Force -Path 'build\obj' | Out-Null

$sdkIncFlags = ($sdkInc | ForEach-Object { '/I "' + $_ + '"' }) -join ' '
$sdkLibFlags = '/LIBPATH:"' + $sdkLibDirUm + '" /LIBPATH:"' + $sdkLibDirUcrt + '"'

$common = "/nologo /std:c++17 /O2 /bigobj /Gy /Gw /MD /EHa /utf-8 /Fo:build\obj\ /D_CRT_SECURE_NO_WARNINGS /DWIN32_LEAN_AND_MEAN /DIMGUI_DEFINE_MATH_OPERATORS /D_DISABLE_CONSTEXPR_MUTEX_CONSTRUCTOR $sdkIncFlags"
$inc    = '/DBROTLI_STATIC /I deps\brotli\c\include /I deps /I deps\imgui /I deps\imguizmo /I deps\minhook_lib\include /I deps\json /I src'

function Invoke-Cl([string]$CompileArgs) {
  # /d suppresses user cmd AutoRun scripts; compiler inherits the x64 environment.
  $cmdLine = 'cl ' + $CompileArgs
  cmd /d /c $cmdLine
  if ($LASTEXITCODE -ne 0) { throw "cl failed: $CompileArgs" }
}

Write-Host '=== Preparing embedded clothing resources ==='
Invoke-Cl "$common $inc src\build\cloth_resources.cpp /Fe:build\cloth_resources.exe /link $sdkLibFlags"
& '.\build\cloth_resources.exe' --repo $root --pack
if ($LASTEXITCODE -ne 0) { throw 'Clothing resource packing failed' }
New-Item -ItemType Directory -Force -Path 'build\obj\brotli' | Out-Null
$decoderSources = @(Get-ChildItem -LiteralPath 'deps\brotli\c\common','deps\brotli\c\dec' -Filter '*.c' -File)
foreach ($file in $decoderSources) {
  Invoke-Cl ('/nologo /O2 /MD /c /TC /DBROTLI_STATIC /I deps\brotli\c\include /Fo:build\obj\brotli\' + $file.BaseName + '.obj "' + $file.FullName + '"')
}
& lib /nologo /OUT:build\obj\cloth_decoder.lib (Get-ChildItem -LiteralPath 'build\obj\brotli' -Filter '*.obj' -File | ForEach-Object FullName)
if ($LASTEXITCODE -ne 0) { throw 'Clothing decoder library failed' }

Write-Host '=== Compiling version resource ==='
# cl 不处理 .rc；必须先用 rc.exe 编成 .res，再交给链接器
# （Applepie Manager 用 GetFileVersionInfoA 读它显示插件版本）
$rcCmdLine = "rc /nologo /D POSER_ENABLE_XXMI_BRIDGE=$bridgeFlag /I src /fo build\obj\poser.res src\poser.rc"
# rc.exe 会按"输出 vs .rc 文件"的时间戳做增量判断，而版本号在 version.h 里——
# 只改 version.h 时它不会重编，导致 DLL 版本号停在旧值。先删掉旧 .res 强制重编。
Remove-Item -LiteralPath 'build\obj\poser.res' -Force -ErrorAction SilentlyContinue
cmd /d /c $rcCmdLine
if ($LASTEXITCODE -ne 0) { throw "rc failed: src\poser.rc" }
if (-not (Test-Path 'build\obj\poser.res')) {
  throw "rc reported success but build\obj\poser.res is missing"
}

Write-Host '=== Building poser.dll ==='
$poserArgs = "$common /DPOSER_ENABLE_XXMI_BRIDGE=$bridgeFlag /DBROTLI_STATIC /DAPPLEPIE_PLUGIN_IMPL $inc /I deps\brotli\c\include /LD " +
  'src\poser.cpp ' +
  'build\obj\poser.res build\obj\cloth_decoder.lib ' +
  'deps\imgui\imgui.cpp deps\imgui\imgui_draw.cpp deps\imgui\imgui_tables.cpp deps\imgui\imgui_widgets.cpp ' +
  'deps\imgui\imgui_impl_dx11.cpp deps\imgui\imgui_impl_win32.cpp deps\imguizmo\ImGuizmo.cpp ' +
  "/Fe:$pluginDir\poser.dll " +
  "/link /NODEFAULTLIB:LIBCMT /MAP:$pluginDir\poser.map $sdkLibFlags d3d11.lib dxgi.lib d3dcompiler.lib dwmapi.lib ole32.lib deps\minhook_lib\lib\libMinHook.x64.lib"
Invoke-Cl $poserArgs
& '.\build\cloth_resources.exe' --repo $root --dll (Join-Path $root "$pluginDir\poser.dll")
if ($LASTEXITCODE -ne 0) { throw 'Embedded clothing resource verification failed' }

Write-Host ''
Write-Host '=== Building d3dcompiler_47.dll (proxy) ==='
$proxyArgs = "$common /LD src\core\proxy_d3dcompiler.cpp /Fe:$pluginDir\d3dcompiler_47.dll /link $sdkLibFlags"
Invoke-Cl $proxyArgs

Write-Host ''
Write-Host '=== Building vulkan-1.dll (proxy) ==='
$vulkanArgs = "$common /LD src\core\proxy_vulkan_full.cpp /Fe:$pluginDir\vulkan-1.dll /link $sdkLibFlags"
Invoke-Cl $vulkanArgs

Write-Host ''
if ($RunTests) {
Write-Host '=== Running local tests (MSVC) ==='
$tests = @(
  @{ Name = 'test_user_agreement'; Src = 'tests\test_user_agreement.cpp' },
  @{ Name = 'test_agreement_ui'; Src = 'tests\test_agreement_ui.cpp' },
  @{ Name = 'test_runtime_shutdown'; Src = 'tests\test_runtime_shutdown.cpp' },
  @{ Name = 'test_runtime_bootstrap'; Src = 'tests\test_runtime_bootstrap.cpp' },
  @{ Name = 'test_plugin_paths'; Src = 'tests\test_plugin_paths.cpp' },
  @{ Name = 'test_layered_readback'; Src = 'tests\test_layered_readback.cpp' },
  @{ Name = 'test_overlay_device'; Src = 'tests\test_overlay_device.cpp' },
  @{ Name = 'test_quat';      Src = 'tests\test_quat.cpp' },
  @{ Name = 'test_native_cloth'; Src = 'tests\test_native_cloth.cpp' },
  @{ Name = 'test_cloth_runtime'; Src = 'tests\test_cloth_runtime.cpp' },
  @{ Name = 'test_cloth_enhancement'; Src = 'tests\test_cloth_enhancement.cpp' },
  @{ Name = 'test_ik';        Src = 'tests\test_ik.cpp' },
  @{ Name = 'test_pose_file'; Src = 'tests\test_pose_file.cpp' },
  @{ Name = 'test_mmd'; Src = 'tests\test_mmd.cpp' },
  @{ Name = 'test_mmd_large'; Src = 'tests\test_mmd_large.cpp' },
  @{ Name = 'test_mmd_calibration'; Src = 'tests\test_mmd_calibration.cpp' },
  @{ Name = 'test_mmd_avatar'; Src = 'tests\test_mmd_avatar.cpp' },
  @{ Name = 'test_mmd_terrain'; Src = 'tests\test_mmd_terrain.cpp' },
  @{ Name = 'test_mmd_environment_runtime'; Src = 'tests\test_mmd_environment_runtime.cpp' },
  @{ Name = 'test_mmd_camera'; Src = 'tests\test_mmd_camera.cpp' },
  @{ Name = 'test_mmd_camera_runtime'; Src = 'tests\test_mmd_camera_runtime.cpp' },
  @{ Name = 'test_mmd_camera_settings'; Src = 'tests\test_mmd_camera_settings.cpp' },
  @{ Name = 'test_mmd_transport'; Src = 'tests\test_mmd_transport.cpp' },
  @{ Name = 'test_mod_bridge'; Src = 'tests\test_mod_bridge.cpp' },
  @{ Name = 'test_mod_bridge_runtime'; Src = 'tests\test_mod_bridge_runtime.cpp' },
  @{ Name = 'test_mmd_audio'; Src = 'tests\test_mmd_audio.cpp' },
  @{ Name = 'test_frame_driver'; Src = 'tests\test_frame_driver.cpp' },
  @{ Name = 'test_mmd_runtime'; Src = 'tests\test_mmd_runtime.cpp' },
  @{ Name = 'test_mmd_squad'; Src = 'tests\test_mmd_squad.cpp' },
  @{ Name = 'test_smc_squad'; Src = 'tests\test_smc_squad.cpp' },
  @{ Name = 'test_mmd_squad_runtime'; Src = 'tests\test_mmd_squad_runtime.cpp' },
  @{ Name = 'test_squad_api'; Src = 'tests\test_squad_api.cpp' },
  @{ Name = 'test_character_capture'; Src = 'tests\test_character_capture.cpp' }
  @{ Name = 'test_component_query'; Src = 'tests\test_component_query.cpp' }
  @{ Name = 'test_smc_switch'; Src = 'tests\test_smc_switch.cpp' }
  @{ Name = 'test_smc_ownership'; Src = 'tests\test_smc_ownership.cpp' }
  @{ Name = 'test_smc_automation'; Src = 'tests\test_smc_automation.cpp' }
  @{ Name = 'test_smc_abi'; Src = 'tests\test_smc_abi.cpp' }
  @{ Name = 'test_character_faces'; Src = 'tests\test_character_faces.cpp' }
  @{ Name = 'test_face_mixing'; Src = 'tests\test_face_mixing.cpp' }
  @{ Name = 'test_face_runtime'; Src = 'tests\test_face_runtime.cpp' }
  @{ Name = 'test_mmd_face_controls'; Src = 'tests\test_mmd_face_controls.cpp' }
  @{ Name = 'test_eye_gaze'; Src = 'tests\test_eye_gaze.cpp' }
  @{ Name = 'test_eye_gaze_runtime'; Src = 'tests\test_eye_gaze_runtime.cpp' }
)
foreach ($t in $tests) {
  if (-not (Test-Path -LiteralPath $t.Src)) { throw "Missing local test source: $($t.Src)" }
  $testFeature = if ($t.Name -eq 'test_mod_bridge_runtime') { '/DPOSER_ENABLE_XXMI_BRIDGE=1' } else { '' }
  Invoke-Cl "$common $testFeature $inc $($t.Src) /Fe:build\tests\$($t.Name).exe build\obj\cloth_decoder.lib /link $sdkLibFlags"
  & ".\build\tests\$($t.Name).exe"
  if ($LASTEXITCODE -ne 0) { throw "test $($t.Name) failed with exit $LASTEXITCODE" }
}

}

Write-Host ''
Write-Host '=== Build OK ==='
Write-Host "  $pluginDir\poser.dll"
Write-Host "  $pluginDir\d3dcompiler_47.dll"
Write-Host "  $pluginDir\vulkan-1.dll"
Get-ChildItem $pluginDir -Include *.lib,*.exp -Recurse -ErrorAction SilentlyContinue | Remove-Item -Force -ErrorAction SilentlyContinue
Write-Host ''
if ($EnableXxmiBridge) {
  Write-Host 'Optional build only; excluded from release packages. See docs/xxmi-bridge.md for installation.'
} else {
  Write-Host 'Deploy with the installation wizard at the repository root, or:'
  Write-Host '  powershell -NoProfile -ExecutionPolicy Bypass -File tools\deploy.ps1 -GameDir "<game directory>"'
}

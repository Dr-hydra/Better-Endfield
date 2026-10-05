param(
  [switch]$RunTests,
  [ValidateSet(0, 1)][int]$EnableLayeredOverlay = 0
)

$ErrorActionPreference = 'Stop'
Write-Host "Layered overlay: $EnableLayeredOverlay"

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
# -RunTests also builds and runs private local tests, when available.

$root = Join-Path $PSScriptRoot '..'
Set-Location $root
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

New-Item -ItemType Directory -Force -Path 'plugin' | Out-Null
New-Item -ItemType Directory -Force -Path 'build\tests' | Out-Null
New-Item -ItemType Directory -Force -Path 'build\obj' | Out-Null

$sdkIncFlags = ($sdkInc | ForEach-Object { '/I "' + $_ + '"' }) -join ' '
$sdkLibFlags = '/LIBPATH:"' + $sdkLibDirUm + '" /LIBPATH:"' + $sdkLibDirUcrt + '"'

$common = "/nologo /std:c++17 /O2 /bigobj /Gy /Gw /MD /EHa /utf-8 /Fo:build\obj\ /D_CRT_SECURE_NO_WARNINGS /DWIN32_LEAN_AND_MEAN /DIMGUI_DEFINE_MATH_OPERATORS /D_DISABLE_CONSTEXPR_MUTEX_CONSTRUCTOR $sdkIncFlags"
$inc    = '/DBROTLI_STATIC /I deps\brotli\c\include /I deps /I deps\imgui /I deps\imguizmo /I deps\minhook_lib\include /I deps\json /I src'

function Invoke-NativeTool([string]$Executable, [string]$Arguments) {
  $info = New-Object System.Diagnostics.ProcessStartInfo
  $info.FileName = $Executable
  $info.Arguments = $Arguments
  $info.WorkingDirectory = (Get-Location).Path
  $info.UseShellExecute = $false
  $process = [System.Diagnostics.Process]::Start($info)
  $process.WaitForExit()
  $exitCode = $process.ExitCode
  $process.Dispose()
  if ($exitCode -ne 0) { throw "$Executable failed ($exitCode)" }
}
function Invoke-Cl([string]$CompileArgs) {
  Invoke-NativeTool $compiler $CompileArgs
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
$decoderObjects = (Get-ChildItem -LiteralPath 'build\obj\brotli' -Filter '*.obj' -File | ForEach-Object { '"' + $_.FullName + '"' }) -join ' '
Invoke-NativeTool (Join-Path (Split-Path $compiler -Parent) 'lib.exe') ('/nologo /OUT:build\obj\cloth_decoder.lib ' + $decoderObjects)

Write-Host '=== Compiling version resource ==='
# cl 不处理 .rc；必须先用 rc.exe 编成 .res，再交给链接器
# （Applepie Manager 用 GetFileVersionInfoA 读它显示插件版本）
$rcCmdLine = "/nologo /DPOSER_ENABLE_LAYERED_OVERLAY=$EnableLayeredOverlay /I src /fo build\obj\poser.res src\poser.rc"
# rc.exe 会按"输出 vs .rc 文件"的时间戳做增量判断，而版本号在 version.h 里——
# 只改 version.h 时它不会重编，导致 DLL 版本号停在旧值。先删掉旧 .res 强制重编。
Remove-Item -LiteralPath 'build\obj\poser.res' -Force -ErrorAction SilentlyContinue
Invoke-NativeTool (Join-Path $sdkBin 'rc.exe') $rcCmdLine

if (-not (Test-Path 'build\obj\poser.res')) {
  throw "rc reported success but build\obj\poser.res is missing"
}

Write-Host '=== Building poser.dll ==='
$poserArgs = "$common /DAPPLEPIE_PLUGIN_IMPL /DPOSER_ENABLE_LAYERED_OVERLAY=$EnableLayeredOverlay $inc /LD " +
  'src\poser.cpp ' +
  'build\obj\poser.res build\obj\cloth_decoder.lib ' +
  'deps\imgui\imgui.cpp deps\imgui\imgui_draw.cpp deps\imgui\imgui_tables.cpp deps\imgui\imgui_widgets.cpp ' +
  'deps\imgui\imgui_impl_dx11.cpp deps\imgui\imgui_impl_win32.cpp deps\imguizmo\ImGuizmo.cpp ' +
  '/Fe:plugin\poser.dll ' +
  "/link /NODEFAULTLIB:LIBCMT /MAP:plugin\poser.map $sdkLibFlags d3d11.lib dxgi.lib d3dcompiler.lib dwmapi.lib ole32.lib deps\minhook_lib\lib\libMinHook.x64.lib"
Invoke-Cl $poserArgs
& '.\build\cloth_resources.exe' --repo $root --dll (Join-Path $root 'plugin\poser.dll')
if ($LASTEXITCODE -ne 0) { throw 'Embedded clothing resource verification failed' }

Write-Host ''
Write-Host '=== Building d3dcompiler_47.dll (proxy) ==='
$proxyArgs = "$common /LD src\core\proxy_d3dcompiler.cpp /Fe:plugin\d3dcompiler_47.dll /link $sdkLibFlags"
Invoke-Cl $proxyArgs

Write-Host ''
Write-Host '=== Building vulkan-1.dll (proxy) ==='
$vulkanArgs = "$common /LD src\core\proxy_vulkan_full.cpp /Fe:plugin\vulkan-1.dll /link $sdkLibFlags"
Invoke-Cl $vulkanArgs

Write-Host ''
if ($RunTests) {
Write-Host '=== Running local tests (MSVC) ==='
$tests = @(Get-ChildItem -LiteralPath 'tests' -Filter 'test_*.cpp' -File | Sort-Object Name | ForEach-Object {
  @{ Name = $_.BaseName; Src = $_.FullName }
})
foreach ($t in $tests) {
  if (-not (Test-Path -LiteralPath $t.Src)) { throw "Missing local test source: $($t.Src)" }
  Invoke-Cl "$common $inc `"$($t.Src)`" /Fe:build\tests\$($t.Name).exe build\obj\cloth_decoder.lib /link $sdkLibFlags"
  & ".\build\tests\$($t.Name).exe"
  if ($LASTEXITCODE -ne 0) { throw "test $($t.Name) failed with exit $LASTEXITCODE" }
}

}

Write-Host ''
& (Join-Path $PSScriptRoot 'copy_character_faces.ps1') -Destination (Join-Path $root 'plugin\mmd\character-faces')
Write-Host '=== Build OK ==='
Write-Host '  plugin\poser.dll'
Write-Host '  plugin\d3dcompiler_47.dll'
Write-Host '  plugin\vulkan-1.dll'
Get-ChildItem 'plugin' -Include *.lib,*.exp -Recurse -ErrorAction SilentlyContinue | Remove-Item -Force -ErrorAction SilentlyContinue
Write-Host ''
Write-Host 'Deploy with the installation wizard at the repository root, or:'
Write-Host '  powershell -NoProfile -ExecutionPolicy Bypass -File tools\deploy.ps1 -GameDir "<game directory>"'

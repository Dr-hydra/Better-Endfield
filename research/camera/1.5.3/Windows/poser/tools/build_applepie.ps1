# Build the Applepie Manager (Sasye/ApplepieManager, AGPL-3.0) into plugin\.
# The manager is the plugin host: it scans plugin\*.dll, manages hotkeys and
# shows a HOME-key GUI. Our poser.dll speaks the same AP_* protocol and will be
# auto-detected. This is optional: the proxy loads poser.dll even without it.
#
# Requires the ApplepieManager repo on disk (default D:\PROJECTS\ApplepieManager)
# and the Windows SDK NuGet packages in deps\ (see tools\setup_winsdk.ps1).

param(
  [string]$ManagerRepo = 'D:\PROJECTS\ApplepieManager'
)

$ErrorActionPreference = 'Stop'

$root = Join-Path $PSScriptRoot '..'
Set-Location $root

if (-not (Test-Path (Join-Path $ManagerRepo 'src\applepie_manager.cpp'))) {
  throw "ApplepieManager not found at $ManagerRepo (clone https://github.com/Sasye/ApplepieManager)"
}

# ---- vcvars ----
$vcvars = 'C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars64.bat'
if (-not (Test-Path $vcvars)) {
  $vswhere = 'C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe'
  if (Test-Path $vswhere) {
    $vsDir = & $vswhere -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath
    if ($vsDir) { $vcvars = Join-Path $vsDir 'VC\Auxiliary\Build\vcvars64.bat' }
  }
}
if (-not (Test-Path $vcvars)) { throw "vcvars64.bat not found" }

# ---- Windows SDK from deps ----
$winsdkHdr = Join-Path $root 'deps\winsdk\c\Include'
$sdkVerDir = Get-ChildItem $winsdkHdr -Directory -ErrorAction SilentlyContinue |
  Sort-Object Name -Descending | Select-Object -First 1
if (-not $sdkVerDir) { throw "Windows SDK headers not found under deps\winsdk" }
$sdkInc = @(
  (Join-Path $sdkVerDir.FullName 'um'),
  (Join-Path $sdkVerDir.FullName 'shared'),
  (Join-Path $sdkVerDir.FullName 'ucrt')
) | Where-Object { Test-Path $_ }
$sdkIncFlags = ($sdkInc | ForEach-Object { "/I $_" }) -join ' '
$sdkLibBase = Join-Path $root 'deps\winsdkcpp\c'
$sdkLibFlags = "/LIBPATH:$(Join-Path $sdkLibBase 'um\x64') /LIBPATH:$(Join-Path $sdkLibBase 'ucrt\x64')"

New-Item -ItemType Directory -Force -Path 'plugin' | Out-Null
New-Item -ItemType Directory -Force -Path 'build\obj' | Out-Null

function Invoke-Cl([string]$CompileArgs) {
  $cmdLine = 'call "' + $vcvars + '" >nul 2>&1 && cl ' + $CompileArgs
  cmd /d /c $cmdLine
  if ($LASTEXITCODE -ne 0) { throw "cl failed: $CompileArgs" }
}

$common = "/nologo /std:c++17 /O2 /MD /EHsc /utf-8 /Fo:build\obj\ /D_CRT_SECURE_NO_WARNINGS /DWIN32_LEAN_AND_MEAN /DIMGUI_DEFINE_MATH_OPERATORS $sdkIncFlags"
$mgr = $ManagerRepo.TrimEnd('\')
$inc = "/I $mgr\src /I $mgr\deps\imgui /I $mgr\deps\minhook_lib\include $sdkIncFlags"

Write-Host '=== Building applepie_manager.dll ==='
$args = "$common $inc /LD " +
  "$mgr\src\applepie_manager.cpp " +
  "$mgr\deps\imgui\imgui.cpp $mgr\deps\imgui\imgui_draw.cpp $mgr\deps\imgui\imgui_tables.cpp $mgr\deps\imgui\imgui_widgets.cpp " +
  "$mgr\deps\imgui\imgui_impl_dx11.cpp $mgr\deps\imgui\imgui_impl_win32.cpp " +
  "/Fe:plugin\applepie_manager.dll " +
  "/link /NODEFAULTLIB:LIBCMT $sdkLibFlags $mgr\deps\minhook_lib\lib\libMinHook.x64.lib user32.lib gdi32.lib winmm.lib comdlg32.lib d3d11.lib dxgi.lib dwmapi.lib ole32.lib winhttp.lib"
Invoke-Cl $args

# Default manager config: HOME toggles the manager panel; poser enabled.
$cfg = @'
# Endfield Plugin Manager Configuration
# 放在游戏根目录 plugin\ 子目录下

toggle_key=HOME
debug_log=1

[plugins]
poser.dll=1
'@
Set-Content -LiteralPath (Join-Path $root 'plugin\applepie_manager_config.txt') -Value $cfg -Encoding UTF8

Get-ChildItem 'plugin' -Include *.lib,*.exp -Recurse -ErrorAction SilentlyContinue | Remove-Item -Force -ErrorAction SilentlyContinue
Write-Host ''
Write-Host '=== OK ==='
Write-Host '  plugin\applepie_manager.dll'
Write-Host '  plugin\applepie_manager_config.txt'

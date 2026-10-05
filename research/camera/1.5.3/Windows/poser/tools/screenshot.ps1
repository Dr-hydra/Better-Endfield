# screenshot.ps1 — 截取游戏窗口画面，供 AI(Codex) 视觉反馈闭环使用。
# 输出到 debug/snap.png（默认），AI 自行读取图片判断游戏画面状态。
#
# 用法：
#   powershell -ExecutionPolicy Bypass -File tools\screenshot.ps1 -ProcessName GameExe
#   powershell -ExecutionPolicy Bypass -File tools\screenshot.ps1 --TitlePattern Endfield
#   powershell -ExecutionPolicy Bypass -File tools\screenshot.ps1            # 截前台窗口
#
# 说明：
#   - 优先按进程名(-ProcessName，即 exe 去掉扩展名的名字)找主窗口；
#     找不到再退回按窗口标题(-TitlePattern，模糊匹配)；
#     都未指定则截取当前前台窗口。
#   - 需要游戏窗口在前台且未被完全遮挡，否则可能截到空白/黑屏。
#     若黑屏，看 AGENT.md 6.4 的备选(PrintWindow / Windows.Graphics.Capture / OBS)。
param(
  [string]$ProcessName = "",
  [string]$TitlePattern = "",
  [string]$OutFile = "",
  [string]$OutDir = ""
)

$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Drawing

if ([string]::IsNullOrEmpty($OutFile)) {
  $DebugDir = if ([string]::IsNullOrEmpty($OutDir)) { Join-Path $PSScriptRoot '..\debug' } else { $OutDir }
  New-Item -ItemType Directory -Force -Path $DebugDir | Out-Null
  $OutFile = Join-Path $DebugDir 'snap.png'
} elseif (-not (Test-Path (Split-Path $OutFile -Parent))) {
  New-Item -ItemType Directory -Force -Path (Split-Path $OutFile -Parent) | Out-Null
}

# P/Invoke：找窗口 + 取矩形（DPI 感知保证坐标正确）
Add-Type @'
using System;
using System.Runtime.InteropServices;
public class Win32Snap {
  [DllImport("user32.dll")] public static extern IntPtr GetForegroundWindow();
  [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr h, out RECT r);
  [DllImport("user32.dll")] public static extern bool SetProcessDPIAware();
  [DllImport("user32.dll", CharSet=CharSet.Unicode)] public static extern bool IsWindow(IntPtr h);
  [StructLayout(LayoutKind.Sequential)] public struct RECT { public int L, T, R, B; }
}
'@
[Win32Snap]::SetProcessDPIAware() | Out-Null

# 1) 定位目标窗口句柄
$hwnd = [IntPtr]::Zero
if ($ProcessName) {
  $proc = Get-Process -Name $ProcessName -ErrorAction SilentlyContinue |
            Where-Object { $_.MainWindowHandle -ne 0 } | Select-Object -First 1
  if ($proc) { $hwnd = $proc.MainWindowHandle }
}
if ([IntPtr]::Zero -eq $hwnd -and $TitlePattern) {
  $closed = Get-Process -ErrorAction SilentlyContinue |
              Where-Object { $_.MainWindowTitle -match $TitlePattern -and $_.MainWindowHandle -ne 0 }
  if ($closed) { $hwnd = ($closed | Select-Object -First 1).MainWindowHandle }
}
if ([IntPtr]::Zero -eq $hwnd -and -not ($ProcessName -or $TitlePattern)) {
  $hwnd = [Win32Snap]::GetForegroundWindow()
}
if ([IntPtr]::Zero -eq $hwnd -or -not [Win32Snap]::IsWindow($hwnd)) {
  Write-Error "No window found (ProcessName='$ProcessName' TitlePattern='$TitlePattern')."
  exit 1
}

# 2) 截取窗口矩形
$r = New-Object Win32Snap+RECT
[Win32Snap]::GetWindowRect($hwnd, [ref]$r) | Out-Null
$w = $r.R - $r.L; $h = $r.B - $r.T
if ($w -le 0 -or $h -le 0) { Write-Error "Bad window rect ($w x $h)."; exit 1 }

$bmp = New-Object System.Drawing.Bitmap($w, $h)
$g = [System.Drawing.Graphics]::FromImage($bmp)
$g.SmoothingMode = 'None'
$g.CopyFromScreen($r.L, $r.T, 0, 0, (New-Object System.Drawing.Size($w, $h)))
$g.Dispose()
$bmp.Save($OutFile, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp.Dispose()

Write-Output "saved $OutFile  (${w}x${h})"
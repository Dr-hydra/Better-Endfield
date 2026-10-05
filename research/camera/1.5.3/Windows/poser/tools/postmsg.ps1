param(
  [int]$Msg = 1,   # WM_APP+n：1=切换面板, 90=业务指令
  [int]$Code = 0   # 业务指令 code（Msg=90 时）：0=切模式 1=冻结/解冻 2=T-pose
)

Add-Type @'
using System;
using System.Runtime.InteropServices;
using System.Text;
public class PM {
  public delegate bool EnumProc(IntPtr h, IntPtr l);
  [DllImport("user32.dll")] public static extern bool EnumWindows(EnumProc cb, IntPtr l);
  [DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr h, out uint p);
  [DllImport("user32.dll", CharSet=CharSet.Unicode)] public static extern int GetClassName(IntPtr h, StringBuilder s, int n);
  [DllImport("user32.dll")] public static extern bool PostMessage(IntPtr h, uint m, IntPtr w, IntPtr l);
}
'@

$game = Get-Process -Name 'Endfield*' -ErrorAction SilentlyContinue | Select-Object -First 1
if (-not $game) { Write-Host 'game not running'; exit 1 }
$targetPid = $game.Id
$found = [IntPtr]::Zero
$cb = [PM+EnumProc]{
  param($h, $l)
  [uint32]$p = 0
  [PM]::GetWindowThreadProcessId($h, [ref]$p) | Out-Null
  if ($p -eq $targetPid) {
    $sb = New-Object System.Text.StringBuilder 128
    [PM]::GetClassName($h, $sb, 128) | Out-Null
    if ($sb.ToString() -eq 'EndfieldPoserOverlay') { $script:found = $h; return $false }
  }
  return $true
}
[PM]::EnumWindows($cb, [IntPtr]::Zero) | Out-Null
if ($found -eq [IntPtr]::Zero) { Write-Host 'overlay window not found'; exit 1 }

$wmApp = 0x8000
$ok = [PM]::PostMessage($found, ($wmApp + $Msg), [IntPtr]$Code, [IntPtr]::Zero)
Write-Host "posted WM_APP+$Msg code=$Code to hwnd=$found ok=$ok"

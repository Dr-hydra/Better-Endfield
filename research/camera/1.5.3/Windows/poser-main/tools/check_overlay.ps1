param([int]$TargetPid)
Add-Type @'
using System;
using System.Runtime.InteropServices;
using System.Text;
public class WndEnum2 {
  public delegate bool EnumProc(IntPtr h, IntPtr l);
  [DllImport("user32.dll")] public static extern bool EnumWindows(EnumProc cb, IntPtr l);
  [DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr h, out uint p);
  [DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr h);
  [DllImport("user32.dll", CharSet=CharSet.Unicode)] public static extern int GetClassName(IntPtr h, StringBuilder s, int n);
  [DllImport("user32.dll", CharSet=CharSet.Unicode)] public static extern int GetWindowText(IntPtr h, StringBuilder s, int n);
  [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr h, out RECT r);
  [StructLayout(LayoutKind.Sequential)] public struct RECT { public int L, T, R, B; }
}
'@
$results = New-Object System.Collections.Generic.List[string]
$cb = [WndEnum2+EnumProc]{
  param($h, $l)
  [uint32]$wpid = 0
  [WndEnum2]::GetWindowThreadProcessId($h, [ref]$wpid) | Out-Null
  if ($wpid -eq $TargetPid) {
    $sb = New-Object System.Text.StringBuilder 256
    [WndEnum2]::GetClassName($h, $sb, 256) | Out-Null
    $cls = $sb.ToString()
    $sb2 = New-Object System.Text.StringBuilder 256
    [WndEnum2]::GetWindowText($h, $sb2, 256) | Out-Null
    $vis = [WndEnum2]::IsWindowVisible($h)
    $r = New-Object WndEnum2+RECT
    [WndEnum2]::GetWindowRect($h, [ref]$r) | Out-Null
    $results.Add("hwnd=$h class=$cls title='$($sb2.ToString())' visible=$vis rect=($($r.L),$($r.T),$($r.R),$($r.B))")
  }
  return $true
}
[WndEnum2]::EnumWindows($cb, [IntPtr]::Zero) | Out-Null
$results | ForEach-Object { Write-Host $_ }
if ($results.Count -eq 0) { Write-Host '(no windows found for this pid)' }

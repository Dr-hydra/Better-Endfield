param(
  [string]$Key = "",      # 按键名：F12 / TAB / ESC / H / 1..9 / A..Z
  [int]$X = -1,           # 鼠标点击 X（-1 则不点击）
  [int]$Y = -1,           # 鼠标点击 Y
  [string]$Button = "left"
)

Add-Type @'
using System;
using System.Runtime.InteropServices;
public class Inp {
  [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr h);
  [DllImport("user32.dll")] public static extern bool SetCursorPos(int x, int y);
  [DllImport("user32.dll")] public static extern void mouse_event(uint f, uint dx, uint dy, uint d, UIntPtr e);
  [DllImport("user32.dll")] public static extern uint SendInput(uint n, INPUT[] p, int cb);
  [StructLayout(LayoutKind.Sequential)] public struct INPUT { public uint type; public InputUnion U; }
  [StructLayout(LayoutKind.Explicit)] public struct InputUnion {
    [FieldOffset(0)] public KEYBDINPUT ki;
  }
  [StructLayout(LayoutKind.Sequential)] public struct KEYBDINPUT {
    public ushort wVk; public ushort wScan; public uint dwFlags; public uint time; public UIntPtr dwExtraInfo;
  }
  public const uint INPUT_KEYBOARD = 1;
  public const uint KEYEVENTF_KEYUP = 0x0002;
  public const uint KEYEVENTF_SCANCODE = 0x0008;
}
'@

function VkOf([string]$name) {
  $n = $name.ToUpper()
  switch ($n) {
    'F12' { return 0x7B } 'F11' { return 0x7A } 'F10' { return 0x79 } 'F9' { return 0x78 }
    'F8'  { return 0x77 } 'F7'  { return 0x76 } 'F6'  { return 0x75 } 'F5'  { return 0x74 }
    'TAB' { return 0x09 } 'ESC' { return 0x1B } 'ENTER' { return 0x0D }
    'HOME' { return 0x24 } 'INSERT' { return 0x2D }
    default { if ($n.Length -eq 1) { return [int][char]$n } return 0 }
  }
}

$game = Get-Process -Name 'Endfield*' -ErrorAction SilentlyContinue | Select-Object -First 1
if ($game) {
  [Inp]::SetForegroundWindow($game.MainWindowHandle) | Out-Null
  Start-Sleep -Milliseconds 300
}

if ($Key) {
  $vk = VkOf $Key
  if ($vk -ne 0) {
    $in = New-Object Inp+INPUT
    $in.type = [Inp]::INPUT_KEYBOARD
    $in.U.ki.wVk = $vk
    $arr = @($in)
    [Inp]::SendInput(1, $arr, [Runtime.InteropServices.Marshal]::SizeOf([type][Inp+INPUT])) | Out-Null
    Start-Sleep -Milliseconds 50
    $in.U.ki.dwFlags = [Inp]::KEYEVENTF_KEYUP
    [Inp]::SendInput(1, $arr, [Runtime.InteropServices.Marshal]::SizeOf([type][Inp+INPUT])) | Out-Null
    Write-Host "sent key $Key (vk=0x$($vk.ToString('X')))"
  } else {
    Write-Host "unknown key: $Key"
  }
}

if ($X -ge 0 -and $Y -ge 0) {
  [Inp]::SetCursorPos($X, $Y) | Out-Null
  Start-Sleep -Milliseconds 100
  $down = 0x0002; $up = 0x0004
  if ($Button -eq 'right') { $down = 0x0008; $up = 0x0010 }
  [Inp]::mouse_event($down, 0, 0, 0, [UIntPtr]::Zero)
  Start-Sleep -Milliseconds 60
  [Inp]::mouse_event($up, 0, 0, 0, [UIntPtr]::Zero)
  Write-Host "clicked ($X,$Y) $Button"
}

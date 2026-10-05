# M1 单帧测试: 向 EndfieldCamLink 发送一帧相机位姿 (协议见 protocol.md)
# 用法: pwsh send_pose.ps1 -X 0 -Y 1 -Z 2 -Fov 60
param(
  [float]$X = 0, [float]$Y = 1, [float]$Z = 2,      # 位置 (Blender 空间)
  [float]$Qx = 0, [float]$Qy = 0, [float]$Qz = 0, [float]$Qw = 1,  # 四元数
  [float]$Fov = 60,
  [int]$Port = 9601,
  [int]$Repeat = 1,     # 重复发送次数 (M1 建议 >=20, 覆盖若干帧)
  [int]$IntervalMs = 50
)
$packet = New-Object byte[] 36
$seq = 1
[BitConverter]::GetBytes([uint32]$seq).CopyTo($packet, 0)
[BitConverter]::GetBytes($X).CopyTo($packet, 4)
[BitConverter]::GetBytes($Y).CopyTo($packet, 8)
[BitConverter]::GetBytes($Z).CopyTo($packet, 12)
[BitConverter]::GetBytes($Qx).CopyTo($packet, 16)
[BitConverter]::GetBytes($Qy).CopyTo($packet, 20)
[BitConverter]::GetBytes($Qz).CopyTo($packet, 24)
[BitConverter]::GetBytes($Qw).CopyTo($packet, 28)
[BitConverter]::GetBytes($Fov).CopyTo($packet, 32)

$udp = New-Object System.Net.Sockets.UdpClient
for ($i = 0; $i -lt $Repeat; $i++) {
    $udp.Send($packet, $packet.Length, '127.0.0.1', $Port) | Out-Null
    Start-Sleep -Milliseconds $IntervalMs
}
$udp.Close()
Write-Output "已发送 $Repeat 帧: pos=($X,$Y,$Z) quat=($Qx,$Qy,$Qz,$Qw) fov=$Fov -> 127.0.0.1:$Port"

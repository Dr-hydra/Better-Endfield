# 发送运行时指令到已注入的 EndfieldCamLink 模块 (12 字节 ECMD 包)
# 指令表: 1=置门控 2=打开营销相机 3=关闭营销相机 4=设置FOV(绝对,arg) 5=营销相机FOV增量(arg)
#         6=快照相机激活 7=快照相机光圈(arg) 8=快照相机对焦距离(arg) 9=打印契约状态
param(
  [int]$Id = 9,
  [float]$Arg = 0,
  [int]$Port = 9601,
  [int]$Repeat = 3,
  [int]$IntervalMs = 100
)
$packet = New-Object byte[] 12
[Text.Encoding]::ASCII.GetBytes('ECMD').CopyTo($packet, 0)
[BitConverter]::GetBytes([uint32]$Id).CopyTo($packet, 4)
[BitConverter]::GetBytes([float]$Arg).CopyTo($packet, 8)
$udp = New-Object System.Net.Sockets.UdpClient
for ($i = 0; $i -lt $Repeat; $i++) {
    $udp.Send($packet, $packet.Length, '127.0.0.1', $Port) | Out-Null
    Start-Sleep -Milliseconds $IntervalMs
}
$udp.Close()
Write-Output "已发送指令 id=$Id arg=$Arg (x$Repeat) -> 127.0.0.1:$Port"

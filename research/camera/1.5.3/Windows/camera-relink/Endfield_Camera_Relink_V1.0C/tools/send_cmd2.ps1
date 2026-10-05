param(
    [Parameter(Mandatory=$true)][int]$Id,
    [float]$A0 = 0,
    [float]$A1 = 0,
    [float]$A2 = 0,
    [float]$A3 = 0,
    [int]$Port = 9601,
    [int]$Repeat = 3,
    [int]$IntervalMs = 60
)
# 发送 v2 扩展指令包 ECM2: magic(4) + u32 id(4) + float a0,a1,a2,a3(16) = 24 字节
$buf = New-Object byte[] 24
[System.Text.Encoding]::ASCII.GetBytes('ECM2').CopyTo($buf, 0)
[BitConverter]::GetBytes([uint32]$Id).CopyTo($buf, 4)
[BitConverter]::GetBytes([single]$A0).CopyTo($buf, 8)
[BitConverter]::GetBytes([single]$A1).CopyTo($buf, 12)
[BitConverter]::GetBytes([single]$A2).CopyTo($buf, 16)
[BitConverter]::GetBytes([single]$A3).CopyTo($buf, 20)

$udp = New-Object System.Net.Sockets.UdpClient
$ep = New-Object System.Net.IPEndPoint([System.Net.IPAddress]::Loopback, $Port)
for ($i = 0; $i -lt $Repeat; $i++) {
    [void]$udp.Send($buf, $buf.Length, $ep)
    if ($IntervalMs -gt 0 -and $i -lt ($Repeat - 1)) { Start-Sleep -Milliseconds $IntervalMs }
}
$udp.Close()
Write-Output ("已发送 ECM2 指令 id={0} a=({1}, {2}, {3}, {4}) x{5} -> 127.0.0.1:{6}" -f `
    $Id, $A0, $A1, $A2, $A3, $Repeat, $Port)

param(
    [int]$Port = 9601,
    [string]$PackageRoot
)
# 一键校准位置锚点原点(模块指令 41)。
#
# 目的: 让 Blender 相机坐标为 (0,0,0) 时, 游戏相机正好落在**角色**身上
#       (默认原点取的是"武装那一刻相机在哪", 与角色总有少量偏差)。
#
# 标准操作流程:
#   1) 武装相机 (相机控制台.cmd 菜单 [3])
#   2) 把 Blender 相机的位置设为 (0,0,0)  —— 参考物件也应在原点(默认方块)
#   3) 用 Blender 把游戏相机挪到正好与角色重合
#   4) 运行本脚本 —— 模块把当时的位移吸收成"原点修正量"
#   5) 把 Blender 相机位置设回 (0,0,0) —— 现在它正好落在角色身上
#      (第 4 步瞬间画面会跳一下, 属于预期: 原点即刻生效)
#
# 持久化: 模块日志的 [origin] 行会打印修正量, 把数值写进
#         game_mod\EndfieldCamLink.ini 的 pose_origin_dx/dy/dz 即可跨会话保留。
$ErrorActionPreference = 'Continue'
if (-not $PackageRoot) {
    $dir = Split-Path -Parent $MyInvocation.MyCommand.Path
    $PackageRoot = Split-Path -Parent $dir
}
$logPath = Join-Path $PackageRoot 'game_mod\EndfieldCamLink.log'
$before = 0
if (Test-Path $logPath) { $before = (Get-Item $logPath).Length }

Write-Output "[1/3] 发送校准指令 (id=41)"
& (Join-Path $PackageRoot 'tools\send_cmd2.ps1') -Id 41 -A0 0 -A1 0 -A2 0 -A3 0 -Repeat 2 -Port $Port | Out-Null
Start-Sleep -Seconds 2

Write-Output "[2/3] 读取模块日志里的校准结果"
if (Test-Path $logPath) {
    $tail = Get-Content $logPath -Encoding UTF8 | Select-String -Pattern '\[origin\]' | Select-Object -Last 1
    if ($tail) { Write-Output ("  " + $tail.Line) }
    else { Write-Output "  [注意] 日志里没有 [origin] 行: 确认已武装且 Blender 正在发送。" }
} else {
    Write-Output "  [注意] 日志不存在 —— 本次游戏可能不是用本包启动的(模块未注入)。"
}

Write-Output "[3/3] 位置锚点诊断 (id=39)"
& (Join-Path $PackageRoot 'tools\send_cmd2.ps1') -Id 39 -A0 0 -Repeat 1 -Port $Port | Out-Null
Start-Sleep -Seconds 2
if (Test-Path $logPath) {
    Get-Content $logPath -Encoding UTF8 | Select-String -Pattern '\[anchor\]' | Select-Object -Last 4 |
        ForEach-Object { Write-Output ("  " + $_.Line) }
}
Write-Output ""
Write-Output "完成。别忘了把 Blender 相机位置放回 (0,0,0) 验证。"

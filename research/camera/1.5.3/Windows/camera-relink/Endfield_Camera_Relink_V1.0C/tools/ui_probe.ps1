param(
    [ValidateSet('audit', 'inv', 'hide', 'restore', 'main', 'layers', 'world', 'world2', 'worldkeep', 'worldoff', 'strict', 'loose', 'keep', 'keepoff', 'keeptest', 'camoff', 'camdepth', 'camon')][string]$Step = 'audit',
    [int]$Hold = 3
)
# 隐藏游戏 UI —— 一键执行(1.0.0ai 起, 1.0.0am/an 增补)。
#   audit     = 指令 64: UI 体检(只读)
#   inv       = 指令 63: 导出 UI 符号清单(只读元数据, 可能耗时 1~3 秒)
#   hide      = 指令 65: 隐藏 UI(默认 3 秒后模块自动还原; 一次性实验)
#   restore   = 指令 65 a0=0: 立即还原(手动兜底)
#   main      = 指令 65 a1=1: 对照实验, 对 Camera.main 下手(**整屏会黑**, 仅验证机制)
#   ---- v1.0.0am 新增(世界空间 UI: 怪物血条/状态条/角色体力条 = 层 16 WorldUI) ----
#   layers    = 指令 70: 图层名表 + 各相机遮罩(逐位带名字, 只读)
#   world     = 指令 71: 只清**位16 WorldUI**, 默认 15 秒后自动还原(先跑这个看效果)
#   world2    = 指令 71: 清**位16 WorldUI + 位15 UIInteract**(连"按F交互"提示一起)
#   worldkeep = 指令 71: 把"清 WorldUI"设为**持续**(勾选隐藏 UI 时一并生效)
#   worldoff  = 指令 71 a0=0: 取消世界清位, 主相机立刻恢复
#   ---- 隐藏策略 ----
#   strict    = 指令 66 a0=1: 严格保持(连 ESC 菜单也不显示)
#   loose     = 指令 66 a0=0: 尊重游戏(默认; ESC 界面可见, 关掉后自动重新隐藏)
#   ---- v1.0.0ar: 主相机保活(ESC/时停时不让游戏把世界相机彻底关掉) ----
#   keep      = 指令 73 a0=1: 开(持久) —— 游戏关掉世界相机时每帧按回
#   keepoff   = 指令 73 a0=0: 关(并让游戏自己重算遮罩)
#   keeptest  = 指令 73 a1=<秒>: 一次性测试, 到期自动还原
#   ---- v1.0.0ar: UI 相机"清屏/启用"写-测(ESC 黑屏的根因验证) ----
#   camoff    = 指令 74 a0=2: 禁用 UI 相机(遮罩=0 也照样清屏 -> 禁用它才不擦掉世界画面)
#   camdepth  = 指令 74 a0=1: 只写 clearFlags=Depth(本作自定义渲染管线不采纳 -> 预期仍全黑)
#   camon     = 指令 74 a0=0: 还原 UI 相机的 clearFlags/enabled
# 用法: powershell -ExecutionPolicy Bypass -File tools\ui_probe.ps1 -Step camoff -Hold 20
#
# 用法:
#   powershell -ExecutionPolicy Bypass -File tools\ui_probe.ps1 -Step layers
#   powershell -ExecutionPolicy Bypass -File tools\ui_probe.ps1 -Step world -Hold 15
$ErrorActionPreference = 'Stop'

$root = Split-Path -Parent $PSScriptRoot          # 包根目录
$log = Join-Path $root 'game_mod\EndfieldCamLink.log'
$send = Join-Path $PSScriptRoot 'send_cmd2.ps1'
if (-not (Test-Path $send)) { throw "找不到 $send" }

# 图层位掩码(来自指令 70 实测的图层名表)
$BIT_WORLDUI = 65536      # 位 16 WorldUI  —— 怪物血条/状态条/角色体力条
$BIT_UINTERACT = 32768    # 位 15 UIInteract —— 世界里的交互提示

$id = 64; $a0 = 0; $a1 = 0; $a2 = 0; $desc = ''
switch ($Step) {
    'audit'     { $id = 64; $desc = 'UI 体检(只读: 相机清单 + UI/图层成员签名 + getter 候选)' }
    'inv'       { $id = 63; $desc = "UI 符号清单(只读) -> game_mod\EndfieldCamLink_ui_inventory.log" }
    'hide'      { $id = 65; $a0 = 1; $a1 = 0; $a2 = $Hold; $desc = "隐藏 UI($Hold 秒后自动还原)" }
    'restore'   { $id = 65; $a0 = 0; $desc = '立即还原 UI 相机遮罩(手动兜底)' }
    'main'      { $id = 65; $a0 = 1; $a1 = 1; $a2 = $Hold; $desc = "对照实验: 对 Camera.main 写遮罩 0(整屏黑), $Hold 秒后自动还原" }
    'layers'    { $id = 70; $desc = '图层名表 + 各相机遮罩(只读, 逐位带名字)' }
    'world'     { $id = 71; $a0 = $BIT_WORLDUI; $a1 = $Hold; $desc = "只清 WorldUI(位16) —— 怪物血条/体力条应消失, $Hold 秒后自动还原" }
    'world2'    { $id = 71; $a0 = $BIT_WORLDUI + $BIT_UINTERACT; $a1 = $Hold; $desc = "清 WorldUI + UIInteract(位16+15, 连交互提示), $Hold 秒后自动还原" }
    'worldkeep' { $id = 71; $a0 = $BIT_WORLDUI; $a1 = 0; $desc = '把"清 WorldUI"设为持续(勾选隐藏 UI 时一并生效)' }
    'worldoff'  { $id = 71; $a0 = 0; $a1 = 0; $desc = '取消世界清位, 主相机立刻恢复' }
    'strict'    { $id = 66; $a0 = 1; $desc = '隐藏策略: 严格保持(连 ESC 菜单也不显示)' }
    'loose'     { $id = 66; $a0 = 0; $desc = '隐藏策略: 尊重游戏(默认; ESC 界面可见)' }
    # ---- v1.0.0ar: 主相机保活(不让游戏在 ESC/时停时把世界相机彻底关掉) ----
    'keep'      { $id = 73; $a0 = 1; $desc = '主相机保活: 开(持久) —— 游戏关掉世界相机时每帧按回' }
    'keepoff'   { $id = 73; $a0 = 0; $desc = '主相机保活: 关(并让游戏自己重算遮罩)' }
    'keeptest'  { $id = 73; $a1 = $Hold; $desc = "主相机保活测试($Hold 秒后自动还原) —— 按 ESC 看世界还在吗" }
    # ---- v1.0.0ar: UI 相机"清屏/启用"写-测(ESC 黑屏根因验证) ----
    'camoff'    { $id = 74; $a0 = 2; $a1 = $Hold; $desc = "禁用 UI 相机($Hold 秒后自动还原) —— 世界画面应露出来" }
    'camdepth'  { $id = 74; $a0 = 1; $a1 = $Hold; $a2 = 3; $desc = "只写 clearFlags=Depth($Hold 秒后自动还原) —— 预期仍全黑(管线不采纳)" }
    'camon'     { $id = 74; $a0 = 0; $desc = '还原 UI 相机的 clearFlags / enabled' }
}

Write-Host "[ui_probe] $desc" -ForegroundColor Cyan
if (-not (Get-Process Endfield -ErrorAction SilentlyContinue)) {
    Write-Host "[ui_probe] 警告: 没检测到 Endfield 进程 —— 指令发出去也没人接" -ForegroundColor Yellow
}
# 记下发送前的日志长度, 之后只读"新增的那一段"
# (日志里有低频 diag 行, 用 -Tail 取尾部会把 [ui] 行截断)
$startPos = 0
if (Test-Path $log) { $startPos = (Get-Item $log).Length }

& powershell -ExecutionPolicy Bypass -File $send -Id $id -A0 $a0 -A1 $a1 -A2 $a2 -A3 0 -Repeat 1 -IntervalMs 0

$waitMs = 1500
if ($Step -in @('hide', 'main', 'world', 'world2', 'keeptest', 'camoff', 'camdepth')) { $waitMs = ($Hold + 2) * 1000 }
if ($Step -eq 'inv') { $waitMs = 5000 }
Start-Sleep -Milliseconds $waitMs

if (Test-Path $log) {
    $fs = New-Object System.IO.FileStream($log, [System.IO.FileMode]::Open, [System.IO.FileAccess]::Read, [System.IO.FileShare]::ReadWrite)
    [void]$fs.Seek($startPos, [System.IO.SeekOrigin]::Begin)
    $sr = New-Object System.IO.StreamReader($fs, [System.Text.Encoding]::UTF8)
    $newText = $sr.ReadToEnd()
    $sr.Close(); $fs.Close()
    Write-Host "---- 本次新增日志里的 [ui] 行 ----" -ForegroundColor Cyan
    $lines = @($newText -split "`r?`n" | Where-Object { $_ -match '\[ui\]' })
    if ($lines.Count -gt 0) { $lines | Select-Object -Last 80 | ForEach-Object { Write-Host $_ } }
    else { Write-Host "(本次新增日志里没有 [ui] 行 —— 确认 game_mod\EndfieldCamLink.dll 是新版(1.0.0an = 13318F81…), 且游戏是本包注入启动的)" }
} else {
    Write-Host "没找到日志文件: $log" -ForegroundColor Yellow
}

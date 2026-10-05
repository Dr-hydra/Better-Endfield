param(
    [int]$Port = 9601,
    [int]$SampleFrames = 600,   # 指令 46 的采样帧数(120fps 下 600 帧 = 5 秒)
    [int]$Settle = 45,          # 指令 45 每步的稳定帧数(给 Cinemachine 阻尼留时间)
    [switch]$NoSetup,           # 跳过"取实例 + 开自由相机"(已经武装过时用)
    [switch]$NoFreeCam,         # 只进拍照模式, **不开**官方自由相机
                                # (验证"当前激活相机是不是快照/关卡相机": 若是, 偏移就能驱动画面)
    [switch]$NoOffset,          # 只做 44 + 46, 不做 45(45 会真的写相机偏移)
    [switch]$NoTail             # 不打印日志尾部
)
# 角色锚点探测(方案 B 前置): 用"写入-测量"反推角色位置, 目标是让位置映射的游戏侧原点
# **每次武装自动校准**, 不再靠肉眼对齐。
#
# 本脚本会依次发送:
#   27  取快照相机实例(会进入拍照模式) —— SetCameraOffset 挂在它身上
#   2   打开官方自由相机(与平时武装一致)
#   44  类型体检: 关键 getter 的"声明返回类型 vs 实际对象类(含父类链)"
#   46  锚点候选采样: 连续 N 帧采样所有候选 → 中位数 / 极差 / 距引擎相机距离
#   45  SetCameraOffset 写入-测量: 两种调用约定各测一遍
#       偏移 0 → 锚点(= 相机正好落在角色锚点上); 偏移 1 和 2 → 验证线性(应恰好 2 倍)
#       偏移 (0,1,0)/(0,0,1) → 看偏移的轴与世界轴的关系
#
# 探测期间位姿写入会自动关闭(否则我们的写入会干扰测量), 探测结束自动恢复。
# 45 会把游戏相机的偏移写掉, 结束时写回 (0,0,0)(0 = 相机正好在锚点上)。
#
# 前置条件: 已进入游戏世界, 且模块已注入(DLL 为 1.0.0h 或更新)。
$dir = Split-Path -Parent $MyInvocation.MyCommand.Path
$pkgRoot = Split-Path -Parent $dir
$logPath = Join-Path $pkgRoot 'game_mod\EndfieldCamLink.log'
$reportPath = Join-Path $pkgRoot 'game_mod\probe_b_report.txt'

function Send-C([int]$id, [double]$a0 = 0, [double]$a1 = 0) {
    & "$dir\send_cmd2.ps1" -Id $id -A0 $a0 -A1 $a1 -Repeat 1 -Port $Port | Out-Null
}

function LogMark([string]$text) { Write-Output $text }

if (-not $NoSetup) {
    LogMark "[1/3] 取快照相机实例(进入拍照模式)..."
    Send-C 27 1
    Start-Sleep -Seconds 2
    if ($NoFreeCam) {
        LogMark "[1/3] 关掉官方自由相机(指令 3) + 不开它: 让激活相机回到快照/关卡相机"
        Send-C 3
        Start-Sleep -Seconds 2
        LogMark "[1/3] 跳过打开官方自由相机 (-NoFreeCam): 当前激活相机应为快照/关卡相机"
    } else {
        LogMark "[1/3] 打开官方自由相机..."
        Send-C 2
        Start-Sleep -Seconds 2
    }
} else {
    LogMark "[1/3] 跳过实例获取(-NoSetup)"
}

LogMark "[2/3] 类型体检(指令 44)..."
Send-C 44
Start-Sleep -Seconds 2

$wait46 = [math]::Ceiling($SampleFrames / 60.0) + 4
LogMark ("[2/3] 锚点候选采样(指令 46, {0} 帧, 约等 {1} 秒)..." -f $SampleFrames, $wait46)
Send-C 46 $SampleFrames
Start-Sleep -Seconds $wait46

if (-not $NoOffset) {
    $each = (($Settle + 6) * 6) / 120.0
    $wait45 = [math]::Ceiling($each * 2) + 6
    LogMark ("[3/3] SetCameraOffset 写入-测量(指令 45, 每步稳定 {0} 帧, 约等 {1} 秒)..." -f $Settle, $wait45)
    Send-C 45 0 $Settle
    Start-Sleep -Seconds $wait45
} else {
    LogMark "[3/3] 跳过写入-测量(-NoOffset)"
}

# ---------- 汇总 ----------
Write-Output ""
Write-Output "================ 探测结果 ================"
if (-not (Test-Path $logPath)) {
    Write-Output "[!] 日志不存在 —— 游戏可能不是用本包启动的(模块未注入)。"
    exit 1
}
$lines = Get-Content $logPath -Encoding UTF8
$picked = $lines | Select-String -Pattern '^\[' | Where-Object {
    $_.Line -match '^\[(audit|probe45|probe46)\]'
} | Select-Object -Last 160
foreach ($l in $picked) { Write-Output $l.Line }

try {
    $picked | ForEach-Object { $_.Line } | Set-Content -Path $reportPath -Encoding UTF8
    Write-Output ""
    Write-Output ("完整片段已存: " + $reportPath)
} catch {
    Write-Output ("[!] 报告写入失败: " + $_.Exception.Message)
}

Write-Output ""
Write-Output "判读指南:"
Write-Output "  1) [audit] 看 get_curVirtualCam 的 声明= 与 实际= , 判断 vcam 候选(1/2) 能不能用。"
Write-Output "  2) [probe46] 极差小(<=0.02) 且 距引擎相机大(>0.1) 的候选 = 可用的静止锚点。"
Write-Output "  3) [probe45] 出现「结论: 约定...可用」= SetCameraOffset 这条路通,"
Write-Output "     那一行的 锚点(x,y,z) 就是「角色位置」; 再看 2X 是否恰好是 X 的两倍(线性)。"
Write-Output "  4) 若两种约定都「未生效」: 说明当前激活相机不是快照相机, 或偏移只在编辑态生效。"
Write-Output ""
Write-Output "把这些贴给我(或直接说一声, 我读 probe_b_report.txt)即可定方案 B 的落点:"
Write-Output "  指令 42 a0=<匹配的候选>  →  指令 41 a0=0 a1=1 (自动校准, 无需肉眼对齐)"

param(
    [int]$Port = 9601,
    [ValidateSet('world', 'character')][string]$Mode = 'world',
    [switch]$NoPose,     # 只跟焦距/光圈, 不做位姿跟随
    [switch]$NoReset,    # 跳过"复位自由相机位置"(指令 21)
    [switch]$NoZero,     # 跳过"通知插件把相机归零到 (0,0,0)"
    [switch]$NoFreeCam,  # 不开官方自由相机(偏移驱动模式需要"激活相机=快照/关卡相机")
    [switch]$NoAutoCal   # 跳过"自动校准原点"(默认会做: 修正量 = 角色锚点 − 武装点)
)
# 一键把游戏相机置入"拍片工作状态", 并给位置基线一个**确定且干净**的原点。
#
# 两条链路(模式):
#   world     = 定机位:  posemode=3 —— 每帧直接写 Camera.main 的 位置+朝向(相机定点不动)
#   character = 随角色:  pose_mode=4 —— 每帧只发"相机偏移"(SetCameraOffset), 位置交给游戏;
#                        只要机架零点 P0 跟着角色走, 跟随就是游戏原生行为(我们不用追)。
#                        朝向仍由 Blender 写入。注意: 该模式要求"激活相机是快照/关卡相机"
#                        (即不要开自由相机), 所以本脚本会自动带上 -NoFreeCam 的行为。
#                        偏移坐标系由 ini 的 pose_offset_basis 描述(指令 45 实测, 绕世界 X ≈11°)。
#
# 指令顺序(定机位链路):
#   27 取快照相机实例(进拍照模式) -> 2 打开官方自由相机 -> 10 刷新实例
#   -> 等 1.5s(让拍照模式的相机混合/稳定, 早期偏差 2.2m 就是抓早了)
#   -> 21 复位自由相机位置(让"游戏自己的机位"确定; -NoReset 可跳过)
#   -> 22 a0=3 开写位姿(模块开始每帧缓存"引擎位姿"= 游戏自己的机位)
#   -> 等 1.0s -> 34 重取基线(此时缓存新鲜, 原点 = 游戏自己刚摆好的机位)
#   -> 9 打印状态 -> 令牌通知插件(水平归零 + 位置归零)
#
# 前置条件: 已进入游戏世界(标题画面下获取实例会返回 null)。
$dir = Split-Path -Parent $MyInvocation.MyCommand.Path
$pkgRoot = Split-Path -Parent $dir
$logPath = Join-Path $pkgRoot 'game_mod\EndfieldCamLink.log'

# v0.2.2: 镜头模式以插件面板为准 —— 插件会把面板选择写进下面这个临时文件。
# 本脚本没有被显式指定 -Mode 时就读它, 于是「相机控制台.cmd」菜单 [3] 武装相机也会尊重面板选择
# (随角色模式必须让快照/关卡相机保持激活, 也就是不能开自由相机, 否则偏移不生效)。
$modeFile = Join-Path $env:TEMP 'EndfieldCameraLink_mode.txt'
if (-not $PSBoundParameters.ContainsKey('Mode') -and (Test-Path $modeFile)) {
    $m = ''
    try { $m = (Get-Content $modeFile -Raw -ErrorAction Stop).Trim() } catch { }
    if ($m -eq 'follow') {
        $Mode = 'character'
        Write-Output "[i] 读插件面板设置: 随角色镜头 ($modeFile)"
    } elseif ($m -eq 'static') {
        $Mode = 'world'
        Write-Output "[i] 读插件面板设置: 定镜头 ($modeFile)"
    }
}

function Send-Cmd([int]$id, [double]$a0 = 0, [double]$a1 = 0) {
    # 需要携带 a0/a1 的指令一律走 24 字节 ECM2 包。
    # 【教训】原先这个名单漏了 41 —— 于是 `41 a0=0 a1=1` 被当成只带 a0 的包发出去,
    # 模块看到 a1=0 就走了"手动校准"分支, 把修正量写成了 (0,0,0)。
    # 凡是要用 a1 的指令都必须在这个名单里。
    $needA1 = @(22, 27, 34, 39, 40, 41, 42, 43, 45, 46, 47)
    if ($needA1 -contains $id) {
        & "$dir\send_cmd2.ps1" -Id $id -A0 $a0 -A1 $a1 -Repeat 1 -Port $Port | Out-Null
    } else {
        & "$dir\send_cmd.ps1" -Id $id -Repeat 1 -Port $Port | Out-Null
    }
}

if ($Mode -eq 'character') {
    Write-Output "[i] 随角色模式(character): 每帧读实时角色坐标 + 我们绝对写相机变换。"
    Write-Output "    仍使用 pose_mode=3(绝对写) —— 它对游戏自身相机状态与鼠标免疫;"
    Write-Output "    '发偏移让游戏自己摆'(pose_mode=4)已降级为实验路径(会被鼠标干涉), 不要用。"
    Write-Output "    需要: 不开自由相机(否则 vcamFollow 读不到, 实时角色坐标拿不到)。"
    if (-not $NoFreeCam) { $NoFreeCam = $true }
}
# v1.0.0n: 两种模式**都**用 pose_mode=3。定镜头/随角色的差别在模块内部(游戏侧原点取
# "冻结值"还是"实时角色坐标"), 由插件面板的 flags 位决定 —— 脚本不再切 pose_mode。
# 【教训】这里曾写成"character -> 4"; 设计改为"两者共用 3"之后漏改, 于是面板选随角色时
#         脚本又把模块切回 pose_mode=4(受鼠标干涉的那条路), 表现为"明明更新了却没生效"。
$poseMode = 3

Write-Output "[1/8] 获取快照相机实例 (ToggleSnapshotCamera)"
Send-Cmd 27 1
Start-Sleep -Seconds 2

# v1.5/1.0.0k: 趁自由相机**没开**的时候先采一次"角色锚点候选"并缓存。
# 原因: 自由相机一开, get_curVirtualCam 返回的是自由相机自己的 MarketingVirtualCamera,
#       它的 Follow 是 null → 之后每帧都读不到角色锚点(实测自动校准因此报"候选不可用")。
# 所以这里先显式关掉自由相机(指令 3)再采 —— 否则"上一次武装残留的自由相机还开着"时
# 这次采样会直接失败。这一步只读候选 + 缓存, 不改动相机位置/朝向。
Write-Output "[1.4/8] 先关掉自由相机(指令 3), 让激活相机回到快照/关卡相机"
Send-Cmd 3
Start-Sleep -Seconds 2
Write-Output "[1.5/8] 采样角色锚点候选(指令 43: 此时代码读得到 vcamFollow)"
Send-Cmd 43
Start-Sleep -Seconds 1
# v1.0.0m: 冻结锚点 —— 武装这一瞬间取一次角色坐标, 之后不再用实时角色坐标干预相机。
# 这是"定镜头"能真正定住的关键: 游戏一旦进入它自己的相机模式, vcamFollow 会变成
# 可读且随角色移动的值, 每帧实时读取会把"定机位"悄悄变成"跟拍"(实测踩到)。
Write-Output "[1.6/8] 冻结角色锚点(指令 49: 此刻自由相机是关的, 取到的是最新鲜的角色坐标)"
Send-Cmd 49
Start-Sleep -Seconds 1

if ($NoFreeCam) {
    Write-Output "[2/8] 跳过打开官方自由相机 (-NoFreeCam: 偏移驱动要求激活相机是快照/关卡相机)"
} else {
    Write-Output "[2/8] 打开官方自由相机 (OpenMarketingCamera)"
    Send-Cmd 2
    Start-Sleep -Seconds 1
}

Write-Output "[3/8] 刷新实例 (GetMainMarketingCameraController)"
Send-Cmd 10
Start-Sleep -Seconds 2

Write-Output "[4/8] 等待 1.5s 让拍照模式相机稳定"
Start-Sleep -Seconds 1.5

if (-not $NoReset) {
    Write-Output "[5/8] 复位自由相机位置 (指令 21, 让原点确定)"
    Send-Cmd 21
    Start-Sleep -Seconds 1
} else {
    Write-Output "[5/8] 跳过复位自由相机位置 (-NoReset)"
}

if (-not $NoPose) {
    Write-Output ("[6/8] 开写位姿 (pose_mode={0}{1})" -f $poseMode,
                  $(if ($poseMode -eq 4) { ", 偏移驱动: 位置交给游戏" } else { "" }))
    Send-Cmd 22 $poseMode
    Start-Sleep -Seconds 1

    Write-Output "[7/8] 重取基线 (指令 34, 原点=游戏自己刚摆好的机位)"
    Send-Cmd 34 3
    Start-Sleep -Seconds 1

    # v1.5: 自动校准 —— 原点修正量 = 角色锚点(用 [1.5/8] 缓存的值) − 武装点。
    # 只有把锚点来源切到角色相关候选(指令 42 a0=1..4)才有意义; source=0 时模块会提示并跳过。
    if (-not $NoAutoCal) {
        Write-Output "[7.5/8] 自动校准原点 (指令 41 a0=0 a1=1: 修正量 = 锚点 − 武装点, 不需要肉眼对齐)"
        Send-Cmd 41 0 1
        Start-Sleep -Seconds 1
    } else {
        Write-Output "[7.5/8] 跳过自动校准 (-NoAutoCal)"
    }
} else {
    Write-Output "[6/8] 跳过位姿写入 (-NoPose)"
    Write-Output "[7/8] 跳过重取基线"
    Write-Output "[7.5/8] 跳过自动校准"
}

Write-Output "[8/8] 打印契约状态"
Send-Cmd 9
Start-Sleep -Seconds 1

# 通知 Blender 插件: 本次武装 -> 水平归零(+ 位置归零到 (0,0,0))
# 插件每 0.25 秒轮询该令牌; 在插件面板里可分别关掉这两项。
$armToken = Join-Path $env:TEMP 'EndfieldCameraLink_arm.txt'
try {
    $enc = New-Object System.Text.UTF8Encoding($false)
    if ($NoZero) {
        [System.IO.File]::WriteAllText($armToken, (Get-Date -Format 'yyyy-MM-dd HH:mm:ss') + ' nozero', $enc)
    } else {
        [System.IO.File]::WriteAllText($armToken, (Get-Date -Format 'yyyy-MM-dd HH:mm:ss'), $enc)
    }
} catch {
    Write-Output ("[!] 无法写入武装令牌: " + $_.Exception.Message)
}

Write-Output ""
Write-Output "完成。核对三行日志:"
Write-Output ("  " + $logPath)
if (Test-Path $logPath) {
    $last = Get-Content $logPath -Encoding UTF8 | Select-String -Pattern '契约: openMarketing' | Select-Object -Last 1
    if ($last) { Write-Output ("  ① 实例   : " + $last.Line) }
    $map = Get-Content $logPath -Encoding UTF8 | Select-String -Pattern '\[map\]' | Select-Object -Last 1
    if ($map) { Write-Output ("  ② 原点   : " + $map.Line) }
    $bl = Get-Content $logPath -Encoding UTF8 | Select-String -Pattern '\[baseline\] 已建立' | Select-Object -Last 1
    if ($bl) { Write-Output ("  ③ 基线   : " + $bl.Line) }
} else {
    Write-Output ""
    Write-Output "  [注意] 日志不存在 —— 本次游戏可能不是用本包启动的(模块未注入)。" -ForegroundColor Yellow
    Write-Output "         请退出游戏后用 相机控制台.cmd 的菜单 [2] 启动游戏并注入 重新启动。" -ForegroundColor Yellow
}
Write-Output ""
Write-Output "模式与调试:"
Write-Output "  定机位(默认): 相机控制台.cmd 菜单 [3] 武装相机  (pose_mode=3, 每帧直接写 transform)"
Write-Output "  随角色:       本脚本 -Mode character    (pose_mode=4, 偏移驱动: 位置交给游戏跟随)"
Write-Output "  关闭位置跟随: send_cmd2.ps1 -Id 22 -A0 0"
Write-Output "  基线诊断:     send_cmd2.ps1 -Id 39   (锚点/参考物件/本次位移/相机与角色相对位置)"
Write-Output "  锚点来源:     send_cmd2.ps1 -Id 42 -A0 1  (1=vcamFollow 角色瞄准点)"
Write-Output "  自动校准:     send_cmd2.ps1 -Id 41 -A0 0 -A1 1  (修正量 = 锚点 − 武装点)"
Write-Output "  朝向若歪:     send_cmd2.ps1 -Id 38 -A0 1"

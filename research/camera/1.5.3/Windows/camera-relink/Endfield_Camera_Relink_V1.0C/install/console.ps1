# EndfieldCameraLink 控制台（交互菜单）
# 由包根目录的 相机控制台.cmd 调用；也可以直接:
#   powershell -ExecutionPolicy Bypass -File install\console.ps1
param(
    [string]$Action   # 可选: 直接执行某个动作(install/launch/arm/stop/log/rebuild/addon/shortcut)，跳过菜单
)
$ErrorActionPreference = 'Continue'
$pkgRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)

function Get-Admin {
    $p = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
    return $p.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}

function Show-Header {
    Clear-Host
    Write-Host ""
    Write-Host "  ============================================================" -ForegroundColor Cyan
    Write-Host "    EndfieldCameraLink  ·  终末地 × Blender 相机联动" -ForegroundColor Cyan
    Write-Host "  ============================================================" -ForegroundColor Cyan

    # 状态行
    $game = Get-Process Endfield -ErrorAction SilentlyContinue
    if ($game) {
        $mod = '未确认'
        $log = Join-Path $pkgRoot 'game_mod\EndfieldCamLink.log'
        if (Test-Path $log) {
            $tail = Get-Content $log -Tail 60 -Encoding UTF8 -ErrorAction SilentlyContinue
            if ($tail -match 'Ready: 相机链路已挂载') { $mod = '已注入' }
            if ($tail -match '成功挂上 Cinemachine') { $mod = '已注入(位姿可用)' }
        }
        Write-Host ("  游戏进程 : 运行中 (PID {0}, {1} MB)   模块: {2}" -f `
            $game.Id, [math]::Round($game.WorkingSet64/1MB), $mod) -ForegroundColor Green
    } else {
        Write-Host "  游戏进程 : 未运行" -ForegroundColor DarkGray
    }
    $bl = Get-Process blender -ErrorAction SilentlyContinue
    if ($bl) { Write-Host "  Blender  : 运行中" -ForegroundColor Green }
    else     { Write-Host "  Blender  : 未运行" -ForegroundColor DarkGray }
    $adm = if (Get-Admin) { '是' } else { '否(需要时会自动提权)' }
    Write-Host ("  管理员   : {0}" -f $adm) -ForegroundColor DarkGray
    Write-Host ""
}

function Pause-Any {
    Write-Host ""
    Read-Host "按回车返回菜单" | Out-Null
}

function Confirm-KillGame {
    $g = Get-Process Endfield -ErrorAction SilentlyContinue
    if (-not $g) { return $true }
    Write-Host ""
    Write-Host "检测到游戏正在运行。" -ForegroundColor Yellow
    Write-Host "  重新注入必须结束当前游戏进程(否则 DLL 被占用)。" -ForegroundColor Yellow
    $a = Read-Host "是否结束游戏进程? (y/N)"
    return ($a -eq 'y' -or $a -eq 'Y')
}

function Invoke-Ps([string]$relPath, [hashtable]$extraParams) {
    $full = Join-Path $pkgRoot $relPath
    if (-not (Test-Path $full)) { Write-Host "脚本不存在: $full" -ForegroundColor Red; return 1 }
    # 注意: 不能把变量命名为 $args —— 那是 PowerShell 自动变量
    $psArgs = @('-NoProfile','-ExecutionPolicy','Bypass','-File', "`"$full`"")
    if ($extraParams) {
        foreach ($k in $extraParams.Keys) {
            $psArgs += "-$k"
            if ($extraParams[$k] -ne $true) { $psArgs += "`"$($extraParams[$k])`"" }
        }
    }
    & powershell @psArgs
    return $LASTEXITCODE
}

# ---------------- 各功能 ----------------

function Do-Install {
    Invoke-Ps 'install\install_all.ps1' $null
}

function Do-Launch {
    if (-not (Confirm-KillGame)) { Write-Host "已取消。" -ForegroundColor DarkGray; return }
    # v0.2.4: 先把 Blender 侧调成 120 帧拍摄状态 —— 检测到 Blender 就设帧率/发送频率(120),
    # 没检测到就先写好设置令牌再启动 Blender(插件加载后自动应用并开始发送)。
    # 注意: 必须在这里(提权之前)做, 否则 Blender 也会被以管理员身份启动。
    Invoke-Ps 'tools\ensure_blender.ps1' @{ Fps = 120; Rate = 120 } | Out-Null
    Write-Host ""
    # launch_game.ps1 自身会请求提权
    Invoke-Ps 'tools\launch_game.ps1' @{ ForceKill = $true; NoPause = $true }
    Write-Host ""
    Write-Host "提示: 启动后约 30 秒模块才就绪(需等待 IL2CPP 初始化)。" -ForegroundColor DarkGray
    Write-Host "      进入游戏世界后请选择 [3] 武装相机。" -ForegroundColor DarkGray
}

function Do-Arm {
    if (-not (Get-Process Endfield -ErrorAction SilentlyContinue)) {
        Write-Host "游戏未运行。请先执行 [2] 启动游戏并注入。" -ForegroundColor Yellow
        return
    }
    $a = Read-Host "是否同时开启「位置跟随」(Blender 控制相机位置/朝向)? (Y/n)"
    $noPose = ($a -eq 'n' -or $a -eq 'N')
    if ($noPose) { Invoke-Ps 'tools\arm_lens.ps1' @{ NoPose = $true } }
    else         { Invoke-Ps 'tools\arm_lens.ps1' $null }
    Write-Host ""
    Write-Host "完成后到 Blender: 侧栏(N) > Endfield Camera > 开始实时发送" -ForegroundColor DarkGray
}

function Do-Stop {
    & (Join-Path $pkgRoot 'tools\send_cmd2.ps1') -Id 22 -A0 0 -Repeat 2 | Out-Null
    Start-Sleep -Milliseconds 500
    & (Join-Path $pkgRoot 'tools\send_cmd.ps1') -Id 3 -Repeat 2 | Out-Null
    Start-Sleep -Milliseconds 500
    & (Join-Path $pkgRoot 'tools\send_cmd.ps1') -Id 32 -Repeat 1 | Out-Null
    Write-Host "已发送: 关闭位姿写入 / 关闭自由相机 / 退出拍照模式" -ForegroundColor Green
    Write-Host "(Blender 里也请点「停止」)" -ForegroundColor DarkGray
}

function Do-Status {
    $log = Join-Path $pkgRoot 'game_mod\EndfieldCamLink.log'
    if (-not (Test-Path $log)) {
        if (Get-Process Endfield -ErrorAction SilentlyContinue) {
            Write-Host "游戏正在运行，但本包没有模块日志。" -ForegroundColor Yellow
            Write-Host "说明本次游戏不是用本包启动的（或注入失败），模块不会响应指令。" -ForegroundColor Yellow
            Write-Host "处理: 退出游戏，然后用 [2] 启动游戏并注入。" -ForegroundColor Yellow
        } else {
            Write-Host "游戏未运行。请先执行 [2] 启动游戏并注入。" -ForegroundColor Yellow
        }
        return
    }
    & (Join-Path $pkgRoot 'tools\send_cmd.ps1') -Id 9 -Repeat 1 | Out-Null
    & (Join-Path $pkgRoot 'tools\send_cmd.ps1') -Id 10 -Repeat 1 | Out-Null
    & (Join-Path $pkgRoot 'tools\send_cmd2.ps1') -Id 35 -Repeat 1 | Out-Null
    Start-Sleep -Seconds 2
    Write-Host ""
    Get-Content $log -Tail 25 -Encoding UTF8 | ForEach-Object { Write-Host "  $_" }
}

function Do-ResetOrigin {
    $log = Join-Path $pkgRoot 'game_mod\EndfieldCamLink.log'
    if (-not (Test-Path $log)) {
        Write-Host "本包没有模块日志 —— 本次游戏不是用本包启动的，指令不会有响应。" -ForegroundColor Yellow
        Write-Host "处理: 退出游戏，然后用 [2] 启动游戏并注入。" -ForegroundColor Yellow
        return
    }
    & (Join-Path $pkgRoot 'tools\send_cmd2.ps1') -Id 34 -Repeat 2 | Out-Null
    Start-Sleep -Milliseconds 800
    Write-Host "已重置相对基线(当前游戏位姿与当前 Blender 位姿重新对齐为原点)。" -ForegroundColor Green
}

function Do-Log {
    $log = Join-Path $pkgRoot 'game_mod\EndfieldCamLink.log'
    if (-not (Test-Path $log)) {
        Write-Host "日志不存在: $log" -ForegroundColor Yellow
        if (Get-Process Endfield -ErrorAction SilentlyContinue) {
            Write-Host "游戏在运行，但本包没有日志 —— 说明本次游戏不是用本包启动的。" -ForegroundColor Yellow
            Write-Host "处理: 退出游戏，然后用 [2] 启动游戏并注入。" -ForegroundColor Yellow
        } else {
            Write-Host "请先执行 [2] 启动游戏并注入。" -ForegroundColor Yellow
        }
        return
    }
    Write-Host "（最后 40 行；完整日志: $log）" -ForegroundColor DarkGray
    Write-Host ""
    Get-Content $log -Tail 40 -Encoding UTF8 | ForEach-Object { Write-Host "  $_" }
}

function Do-Addon {
    Invoke-Ps 'install\install_blender_addon.ps1' $null
}

function Do-Rebuild {
    if (-not (Confirm-KillGame)) { Write-Host "已取消。" -ForegroundColor DarkGray; return }
    Invoke-Ps 'tools\rebuild_and_launch.ps1' $null
}

function Do-Shortcut {
    Invoke-Ps 'install\create_shortcuts.ps1' @{ Force = $true }
}

# ---------------- 动作登记表 + 统一分发 ----------------
# 中文提示统一在这里用 Write-Host 输出：
# Win32 控制台 API 与代码页无关，因此不会受 .cmd 的 GBK/UTF-8/65001 影响。
# （教训: 把中文写进 .cmd 的 echo 里，在非对应代码页的控制台下会乱码。）
$actions = [ordered]@{
    install = @{
        Title = '1 · 安装配置'
        Desc  = @('路径探测 + 安装 Blender 插件 + 完整性自检', '不需要管理员权限')
        Fn    = { Do-Install }
    }
    launch = @{
        Title = '2 · 启动游戏并注入模块'
        Desc  = @('会弹出 UAC 确认框，请点“是”',
                  '顺带处理 Blender: 已运行就设为 120 帧/120 发送, 没运行就启动它(同样设好并自动开始发送)',
                  '注意: 请不要从启动器直接点开始游戏，必须用本程序启动，模块才会被加载',
                  '启动后约 30 秒模块才就绪（需等 IL2CPP 初始化完成）')
        Fn    = { Do-Launch }
    }
    arm = @{
        Title = '3 · 武装相机'
        Desc  = @('必须在「已进入游戏世界」之后运行，每次进入世界都要做',
                  '作用: 取拍照相机实例（光圈/对焦的载体）+ 打开官方自由相机 + 开启位置跟随')
        Fn    = { Do-Arm }
    }
    stop = @{
        Title = '4 · 关闭相机控制'
        Desc  = @('关闭位姿写入并让相机交还游戏；Blender 里也请点「停止」')
        Fn    = { Do-Stop }
    }
    log    = @{ Title = '5 · 查看运行日志'; Desc = @('显示模块日志最后若干行'); Fn = { Do-Log } }
    reset  = @{ Title = '6 · 重新对齐位置原点'; Desc = @('把当前游戏位姿与当前 Blender 位姿重新对齐为原点'); Fn = { Do-ResetOrigin } }
    rebuild = @{ Title = '7 · 从源码重建模块'; Desc = @('需要 MinGW-w64 g++；会先结束正在运行的游戏'); Fn = { Do-Rebuild } }
    status = @{ Title = '8 · 相机状态'; Desc = @('查询实例是否就绪并显示最近日志'); Fn = { Do-Status } }
    addon  = @{ Title = '9 · 只安装 Blender 插件'; Desc = @('把插件装到用户插件目录'); Fn = { Do-Addon } }
    shortcut = @{ Title = '0 · 创建桌面快捷方式'; Desc = @('在桌面创建功能入口（加 -All 可建全部）'); Fn = { Do-Shortcut } }
}

function Show-Banner([string]$title, [string[]]$desc) {
    Write-Host ""
    Write-Host "  ============================================================" -ForegroundColor Cyan
    Write-Host ("   终末地相机  ·  " + $title) -ForegroundColor Cyan
    foreach ($d in $desc) { Write-Host ("   " + $d) -ForegroundColor DarkGray }
    Write-Host "  ============================================================" -ForegroundColor Cyan
    Write-Host ""
}

function Run-Action([string]$key) {
    $a = $actions[$key]
    if (-not $a) { Write-Host "未知动作: $key" -ForegroundColor Red; return }
    try { $Host.UI.RawUI.WindowTitle = "终末地相机 - " + $a.Title } catch {}
    Show-Banner $a.Title $a.Desc
    & $a.Fn
}

# 菜单项（1-7 与包根目录的 .cmd 启动器编号保持一致）
$menu = @(
    @{ Key='1'; Text='安装配置（路径探测 + Blender 插件 + 自检）';            Key2='install' },
    @{ Key='2'; Text='启动游戏并注入模块            ← 每次先做这步';           Key2='launch' },
    @{ Key='3'; Text='武装相机（进入游戏世界后运行）   ← 每次都要';           Key2='arm' },
    @{ Key='4'; Text='关闭相机控制（相机交还游戏）';                          Key2='stop' },
    @{ Key='5'; Text='查看运行日志';                                          Key2='log' },
    @{ Key='6'; Text='重新对齐位置原点';                                      Key2='reset' },
    @{ Key='7'; Text='从源码重建模块（需要 MinGW）';                          Key2='rebuild' },
    @{ Key='8'; Text='查看相机状态 / 实例是否就绪';                          Key2='status' },
    @{ Key='9'; Text='只安装 Blender 插件';                                   Key2='addon' },
    @{ Key='0'; Text='创建桌面快捷方式';                                      Key2='shortcut' }
)

# 直接执行某个动作（供 .cmd 启动器调用）
if ($Action) {
    Run-Action $Action.ToLower()
    exit 0
}

while ($true) {
    Show-Header
    foreach ($m in $menu) {
        Write-Host ("   [{0}] {1}" -f $m.Key, $m.Text)
    }
    Write-Host "   [Q] 退出"
    Write-Host ""
    $sel = Read-Host "请输入序号"
    if ($sel -eq 'q' -or $sel -eq 'Q') { break }
    $hit = $menu | Where-Object { $_.Key -eq $sel }
    if (-not $hit) { continue }
    Run-Action $hit.Key2
    Pause-Any
}

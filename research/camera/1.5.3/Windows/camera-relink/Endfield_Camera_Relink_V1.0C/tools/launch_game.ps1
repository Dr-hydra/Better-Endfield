param(
    [string]$GameExe,
    [string]$Dll,
    [switch]$StartLauncher,   # 若游戏卡在启动前握手, 加这个参数先拉起启动器
    [switch]$ForceKill,       # 若已有残留游戏进程, 强制结束(受 ACE 保护的进程需要管理员)
    [switch]$NoPause
)
# 启动《明日方舟：终末地》并在进程挂起阶段注入 EndfieldCamLink.dll。
#
# 为什么必须"挂起时注入"而不是运行中注入:
#   1) 游戏以管理员权限运行, 中等权限进程 OpenProcess 会得到 err=5;
#   2) 挂起阶段注入可在 ACE 反作弊完全武装之前完成, 也不向游戏目录写任何文件。
#
# 脚本会自动请求管理员权限(游戏清单要求)。用法:
#   powershell -ExecutionPolicy Bypass -File tools\launch_game.ps1
$ErrorActionPreference = 'Stop'
$pkgRoot   = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$iniPath   = Join-Path $pkgRoot 'paths.ini'
$modDir    = Join-Path $pkgRoot 'game_mod'

# ---------- 自动提权 ----------
$principal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
if (-not $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    Write-Host "需要管理员权限(游戏清单要求), 正在请求提权..." -ForegroundColor Yellow
    $argList = @('-NoProfile','-ExecutionPolicy','Bypass','-File', "`"$PSCommandPath`"")
    if ($GameExe) { $argList += @('-GameExe', "`"$GameExe`"") }
    if ($Dll)     { $argList += @('-Dll', "`"$Dll`"") }
    if ($StartLauncher) { $argList += '-StartLauncher' }
    if ($ForceKill)     { $argList += '-ForceKill' }
    Start-Process -FilePath 'powershell.exe' -ArgumentList $argList -Verb RunAs
    exit 0
}

function Read-PathIni {
    $r = @{}
    if (Test-Path $iniPath) {
        foreach ($l in Get-Content $iniPath) {
            if ($l -match '^\s*([A-Za-z_]+)\s*=\s*(.+?)\s*$') { $r[$Matches[1]] = $Matches[2] }
        }
    }
    return $r
}
$cfg = Read-PathIni

if (-not $GameExe) { $GameExe = $cfg['game_exe'] }
if (-not $Dll)     { $Dll     = Join-Path $modDir 'EndfieldCamLink.dll' }
$injector = Join-Path $modDir 'injector.exe'
$ini       = Join-Path $modDir 'EndfieldCamLink.ini'

# ---------- 校验 ----------
$missing = @()
foreach ($p in @($GameExe, $Dll, $injector, $ini)) {
    if (-not $p -or -not (Test-Path $p)) { $missing += "$p" }
}
if ($missing.Count -gt 0) {
    Write-Host "以下文件缺失, 无法启动:" -ForegroundColor Red
    $missing | ForEach-Object { Write-Host "  $_" }
    if (-not $GameExe) {
        Write-Host ""
        Write-Host "未配置游戏路径。请先运行 install\find_paths.ps1, 或用 -GameExe 指定:" -ForegroundColor Yellow
        Write-Host '  -GameExe "D:\Hypergryph Launcher\games\Endfield Game\Endfield.exe"'
    }
    if (-not $NoPause) { Read-Host "按回车退出" | Out-Null }
    exit 1
}

# ---------- 已有进程处理 ----------
$existing = Get-Process Endfield -ErrorAction SilentlyContinue
if ($existing) {
    if ($ForceKill) {
        Write-Host "结束残留游戏进程..." -ForegroundColor Yellow
        $existing | ForEach-Object { try { Stop-Process -Id $_.Id -Force -ErrorAction Stop } catch {} }
        Start-Sleep -Seconds 5
        if (Get-Process Endfield -ErrorAction SilentlyContinue) {
            Write-Host "仍有残留进程无法结束(受 ACE 保护)。请手工结束或重启系统。" -ForegroundColor Red
            if (-not $NoPause) { Read-Host "按回车退出" | Out-Null }
            exit 1
        }
    } else {
        Write-Host "游戏已在运行。若要重新注入, 请先退出游戏, 或加 -ForceKill 参数。" -ForegroundColor Yellow
        if (-not $NoPause) { Read-Host "按回车退出" | Out-Null }
        exit 1
    }
}

# ---------- 可选: 先拉起启动器 ----------
if ($StartLauncher) {
    $lc = $cfg['launcher_exe']
    if ($lc -and (Test-Path $lc)) {
        if (-not (Get-Process -Name 'Launcher' -ErrorAction SilentlyContinue)) {
            Write-Host "启动启动器(用于初始化 ACE)..." -ForegroundColor Cyan
            Start-Process -FilePath $lc
            Start-Sleep -Seconds 20
        }
    } else {
        Write-Host "未找到启动器, 跳过。" -ForegroundColor Yellow
    }
}

# ---------- 挂起启动 + 注入 ----------
Write-Host "挂起启动游戏并注入模块..." -ForegroundColor Cyan
Remove-Item (Join-Path $modDir 'EndfieldCamLink.log') -Force -ErrorAction SilentlyContinue
$out = & $injector --launch $GameExe $Dll 2>&1
$out | ForEach-Object { Write-Host "  $_" }
$code = $LASTEXITCODE

Write-Host ""
switch ($code) {
    0  { Write-Host "已注入并恢复游戏运行。等待游戏加载到主界面后进入游戏世界。" -ForegroundColor Green }
    10 { Write-Host "游戏在 60 秒内退出(退出码见上)。这通常意味着本次启动遇到问题, 重试一次即可。" -ForegroundColor Yellow }
    default { Write-Host "注入流程退出码 $code (8=需要管理员, 9=注入失败)" -ForegroundColor Red }
}

Write-Host ""
Write-Host "模块日志: $modDir\EndfieldCamLink.log"
Write-Host "看到 'Ready: 相机链路已挂载' 即表示注入成功(启动后约 30 秒)。"
Write-Host ""
Write-Host "进入游戏世界后, 运行:" -ForegroundColor Cyan
Write-Host "  powershell -ExecutionPolicy Bypass -File tools\arm_lens.ps1"
if (-not $NoPause) { Read-Host "按回车退出" | Out-Null }

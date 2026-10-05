param(
    [int]$Fps = 120,          # Blender 场景输出帧率(scene.render.fps)
    [int]$Rate = 120,         # 插件信号发送频率(每秒包数, 上限 1000; 插件侧同样会夹紧)
    [string]$Scene,           # 可选: 需要 Blender 启动时打开的 .blend 文件
    [string]$BlenderExe,      # 可选: 指定 blender.exe(默认读 paths.ini)
    [switch]$NoLaunch,        # 只写设置, 不启动 Blender
    [switch]$NoAutostart      # 不让插件自动开始发送(只调帧率/频率)
)
# 让 Blender 侧直接进入"120 帧拍摄状态":
#   · 检测有没有 Blender 进程;
#   · 有  → 直接把它的输出帧率与插件发送频率都设成 120(走令牌文件, 不需要人工点面板);
#   · 没有 → 先把设置令牌写好, 再启动 Blender —— 插件加载后第一次轮询就会应用这些设置
#            (并且自动开始发送), 所以不需要你去面板上点。
#
# 通道: 复用既有的"临时文件令牌"机制(与武装令牌同一套路), 不引入新网络通道:
#   本脚本写 %TEMP%\EndfieldCameraLink_settings.txt
#   插件每 0.25 秒轮询一次, 内容不同于上次就应用一次(fps / 发送频率 / 是否自动开始发送)。
#   令牌内容形如: 2026-09-11 23:55:01 fps=120 rate=120 autostart=1
#
# 【重要提醒】如果 Blender 本来没在运行, 本脚本只能启动它到**新场景/启动文件** ——
#   未保存的 .blend 不会自动恢复。要接着做上一个工程, 请用 -Scene "D:\path\xxx.blend"。
$ErrorActionPreference = 'Continue'
$pkgRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)

# v0.3.7: 发送频率上限 240 → 1000。这里与插件用同一个上限, 免得写出一个插件不认的值
# (插件侧 _apply_settings 也会夹紧, 这里是第二道)。
$RateMax = 1000
if ($Rate -gt $RateMax) { $Rate = $RateMax }
if ($Rate -lt 1) { $Rate = 1 }

function Write-SettingsToken([int]$fps, [int]$rate, [bool]$autostart) {
    $f = Join-Path $env:TEMP 'EndfieldCameraLink_settings.txt'
    $line = "{0} fps={1} rate={2} autostart={3}" -f (Get-Date -Format 'yyyy-MM-dd HH:mm:ss'), $fps, $rate,
                                                     $(if ($autostart) { 1 } else { 0 })
    $enc = New-Object System.Text.UTF8Encoding($false)
    [System.IO.File]::WriteAllText($f, $line, $enc)
    return $f
}

# ---------- 1) 找 blender.exe ----------
if (-not $BlenderExe) {
    $ini = Join-Path $pkgRoot 'paths.ini'
    if (Test-Path $ini) {
        $BlenderExe = (Get-Content $ini -Encoding UTF8 | Where-Object { $_ -match '^blender_exe=' }) -replace '^blender_exe=', ''
    }
}
if (-not $BlenderExe -or -not (Test-Path $BlenderExe)) {
    $c = Get-Command blender -ErrorAction SilentlyContinue
    if ($c) { $BlenderExe = $c.Source }
}
if (-not $BlenderExe -or -not (Test-Path $BlenderExe)) {
    foreach ($p in @('D:\Blender\blender.exe', 'C:\Program Files\Blender Foundation\Blender 5.2\blender.exe',
                     'C:\Program Files\Blender Foundation\Blender 4.2\blender.exe')) {
        if (Test-Path $p) { $BlenderExe = $p; break }
    }
}

# ---------- 2) 先写令牌(无论 Blender 在不在跑, 令牌都会等着被应用) ----------
$tokenFile = Write-SettingsToken $Fps $Rate (-not $NoAutostart)
Write-Host ("[设置令牌] fps={0} 发送频率={1} 自动开始发送={2}" -f $Fps, $Rate, (-not $NoAutostart)) -ForegroundColor Cyan
Write-Host ("           {0}  ->  {1}" -f $tokenFile, (Get-Content $tokenFile -Raw).Trim()) -ForegroundColor DarkGray

# ---------- 3) Blender 在不在? 不在就启动 ----------
$proc = Get-Process blender -ErrorAction SilentlyContinue
if ($proc) {
    $plist = @($proc)
    Write-Host ("[检测] Blender 正在运行 ({0} 个进程: {1}) —— 设置会在 0.25 秒内自动生效, 无需手动点面板。" -f
                $plist.Count, (($plist | ForEach-Object { $_.Id }) -join ', ')) -ForegroundColor Green
    exit 0
}

Write-Host "[检测] 没有发现 Blender 进程。" -ForegroundColor Yellow
if ($NoLaunch) {
    Write-Host "       (-NoLaunch: 按要求不启动; 令牌已就位, 下次 Blender 启动时会自动应用。)" -ForegroundColor DarkGray
    exit 0
}
if (-not $BlenderExe) {
    Write-Host "[!] 找不到 blender.exe —— 无法启动。" -ForegroundColor Red
    Write-Host "    请在 paths.ini 里写一行 blender_exe=<完整路径>, 或用 -BlenderExe 指定。" -ForegroundColor Yellow
    exit 2
}

# 注意: 变量名不能叫 $args(PowerShell 自动变量), 这里用 $blenderArgs
$blenderArgs = @()
$sceneNote = "新场景/启动文件"
if ($Scene) {
    if (Test-Path $Scene) { $blenderArgs += $Scene; $sceneNote = $Scene }
    else { Write-Host ("[!] 指定的场景不存在, 忽略: " + $Scene) -ForegroundColor Yellow }
}
Write-Host ("[启动] {0}   ({1})" -f $BlenderExe, $sceneNote) -ForegroundColor Cyan
# 注意: 空数组不能传给 -ArgumentList —— PS 5.1 会以 "argument is null, empty, or an element of
# the argument collection contains a null value" 直接报错; 而"不带 -Scene 启动 Blender"
# 恰好就是空数组, 也就是最常见的那条路径(实测踩到: 不加这个判断, 该分支根本跑不起来)。
if ($blenderArgs.Count -gt 0) {
    Start-Process -FilePath $BlenderExe -ArgumentList $blenderArgs
} else {
    Start-Process -FilePath $BlenderExe
}
Write-Host "       已启动。插件加载后会自动: 输出帧率=120 → 发送频率=120 → 开始发送。" -ForegroundColor Green
if (-not $Scene) {
    Write-Host "       注意: 这次打开的是新场景, 未保存的工程不会自动恢复(需要的话用 -Scene 指定 .blend)。" -ForegroundColor DarkGray
}

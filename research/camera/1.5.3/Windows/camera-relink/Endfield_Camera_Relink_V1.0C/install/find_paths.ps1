param(
    [switch]$Quiet
)
# 探测游戏与 Blender 的安装路径，写入 <包根>\paths.ini 供其它脚本使用。
# 只做只读探测，不修改系统任何东西。
$ErrorActionPreference = 'Continue'
$pkgRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$iniPath = Join-Path $pkgRoot 'paths.ini'

function Write-Info($m) { if (-not $Quiet) { Write-Output $m } }

# ---------- 游戏 ----------
function Find-Endfield {
    $cands = New-Object System.Collections.Generic.List[string]
    # 1) 各固定盘符下的启动器目录
    foreach ($d in (Get-PSDrive -PSProvider FileSystem | Where-Object { $_.Free -ne $null } | ForEach-Object { $_.Root })) {
        $cands.Add((Join-Path $d 'Hypergryph Launcher\games\Endfield Game\Endfield.exe'))
        $cands.Add((Join-Path $d 'Program Files\Hypergryph Launcher\games\Endfield Game\Endfield.exe'))
        $cands.Add((Join-Path $d 'Games\Hypergryph Launcher\games\Endfield Game\Endfield.exe'))
    }
    foreach ($c in $cands) { if (Test-Path $c) { return (Resolve-Path $c).Path } }
    # 2) 注册表卸载项里找 Hypergryph / Endfield
    foreach ($hive in @('HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\*',
                        'HKLM:\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\*',
                        'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\*')) {
        foreach ($k in (Get-ItemProperty $hive -ErrorAction SilentlyContinue)) {
            $name = "$($k.DisplayName)"
            if ($name -match 'Hypergryph|Endfield|明日方舟|终末地') {
                $loc = $k.InstallLocation
                if ($loc) {
                    $p = Join-Path $loc 'games\Endfield Game\Endfield.exe'
                    if (Test-Path $p) { return (Resolve-Path $p).Path }
                    $p2 = Join-Path $loc 'Endfield.exe'
                    if (Test-Path $p2) { return (Resolve-Path $p2).Path }
                }
            }
        }
    }
    return $null
}

# ---------- 启动器(exe) ----------
function Find-Launcher {
    $game = $null
    if (Test-Path $iniPath) { $game = (Get-Content $iniPath | Where-Object { $_ -match '^game_exe=' }) -replace '^game_exe=', '' }
    if ($game) {
        $dir = Split-Path -Parent (Split-Path -Parent (Split-Path -Parent $game))  # ...\games\Endfield Game -> ...\Hypergryph Launcher
        $l = Join-Path $dir 'Launcher.exe'
        if (Test-Path $l) { return (Resolve-Path $l).Path }
    }
    foreach ($d in (Get-PSDrive -PSProvider FileSystem | ForEach-Object { $_.Root })) {
        $l = Join-Path $d 'Hypergryph Launcher\Launcher.exe'
        if (Test-Path $l) { return (Resolve-Path $l).Path }
    }
    return $null
}

# ---------- Blender ----------
function Find-Blender {
    $cands = New-Object System.Collections.Generic.List[string]
    foreach ($d in (Get-PSDrive -PSProvider FileSystem | ForEach-Object { $_.Root })) {
        $cands.Add((Join-Path $d 'Blender\blender.exe'))
        $cands.Add((Join-Path $d 'Program Files\Blender Foundation\Blender 5.2\blender.exe'))
        $cands.Add((Join-Path $d 'Program Files\Blender Foundation\Blender 4.2\blender.exe'))
        $cands.Add((Join-Path $d 'Program Files\Blender Foundation\Blender 3.6\blender.exe'))
    }
    $cands.Add((Join-Path $env:LOCALAPPDATA 'Programs\Blender Foundation\Blender\blender.exe'))
    foreach ($c in $cands) { if (Test-Path $c) { return (Resolve-Path $c).Path } }
    # 兜底: PATH 里的 blender
    $cmd = Get-Command blender -ErrorAction SilentlyContinue
    if ($cmd) { return $cmd.Source }
    return $null
}

# ---------- 依赖检查 ----------
Write-Info "=== 环境探测 ==="
$os = (Get-CimInstance Win32_OperatingSystem).Caption
$arch = $env:PROCESSOR_ARCHITECTURE
Write-Info ("系统: {0} / {1}" -f $os, $arch)
if ($arch -ne 'AMD64') { Write-Warning "本插件只支持 64 位 Windows (当前: $arch)" }

$game = Find-Endfield
if ($game) { Write-Info ("游戏: {0}" -f $game) } else { Write-Warning "未自动找到 Endfield.exe，请在 paths.ini 里手工填写 game_exe=" }

$launcher = Find-Launcher
if ($launcher) { Write-Info ("启动器: {0}" -f $launcher) } else { Write-Info "启动器: 未找到(可选)" }

$blender = Find-Blender
if ($blender) {
    Write-Info ("Blender: {0}" -f $blender)
    $bv = (& $blender --version 2>$null | Select-Object -First 1)
    if ($bv) { Write-Info ("Blender 版本: {0}" -f $bv.Trim()) }
} else { Write-Warning "未自动找到 blender.exe，请在 paths.ini 里手工填写 blender_exe=" }

$mh = Join-Path $pkgRoot 'game_mod\EndfieldCamLink.dll'
Write-Info ("注入模块: {0} ({1})" -f $(if (Test-Path $mh) { '存在' } else { '缺失!' }), $mh)

# ---------- 写 paths.ini ----------
$lines = @(
    '; EndfieldCameraLink 路径配置 (由 install\find_paths.ps1 生成, 可手工修改)',
    '; 路径含空格不需要加引号',
    ("game_exe=" + ($game    | ForEach-Object { $_ })),
    ("launcher_exe=" + ($launcher | ForEach-Object { $_ })),
    ("blender_exe=" + ($blender | ForEach-Object { $_ }))
)
Set-Content -Path $iniPath -Value $lines -Encoding UTF8
Write-Info ""
Write-Info ("已写入: {0}" -f $iniPath)

if (-not $Quiet) {
    Write-Info ""
    Write-Info "下一步: 运行 install\install_all.ps1 完成安装"
}

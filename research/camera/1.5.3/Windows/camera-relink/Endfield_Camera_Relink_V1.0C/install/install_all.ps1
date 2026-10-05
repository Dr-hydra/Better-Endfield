param(
    [switch]$SkipPaths,
    [switch]$SkipAddon
)
# 一键安装/配置。做四件事:
#   0) 检查旧版残留(只提示, 不删任何东西)
#   1) 探测游戏与 Blender 路径 -> 写 paths.ini
#   2) 安装并自动启用 Blender 插件
#   3) 自检并打印后续步骤
# 不需要管理员权限(游戏侧只在运行时需要)。
#
# 本脚本同时就是**升级脚本**: 新版覆盖旧目录后重跑一次即可, 无需卸载旧版。
#
# v1.0.0au-fix: **不再在安装时创建桌面快捷方式**(按用户要求)。全部功能都从包根目录的
#   「相机控制台.cmd」进入(交互菜单含全部动作); 控制台里的 [0] 仍可按需手工建快捷方式。
$ErrorActionPreference = 'Continue'
$pkgRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)

Write-Host "===============================" -ForegroundColor Cyan
Write-Host " EndfieldCameraLink 安装程序" -ForegroundColor Cyan
Write-Host "===============================" -ForegroundColor Cyan
Write-Host ""

# 前置: 若包是从邮件/网盘下载的, 文件会带"来自 Internet"锁定标记,
# 可能导致脚本被拦或 exe 触发 SmartScreen。这里顺手解除(无锁定则跳过)。
try {
    & (Join-Path $pkgRoot 'install\unblock_files.ps1') -Quiet | Out-Null
} catch { }

# ---------- 0) 旧版残留检测(v1.0.0w) ----------
# 升级方式是"覆盖 + 重跑本脚本", 但有两处旧版残留不在正常流程的覆盖范围内,
# 而且都不会自己报错 —— 用户以为升级完了, 实际还在跑旧文件:
#   a) 旧版(<=0.3.5)的用户目录预设  %APPDATA%\EndfieldCameraLink\presets
#      (0.3.6 起改读 <包根>\presets; 那份旧目录里是 0.3.0 的老示例预设)
#   b) 正在运行的游戏 / Blender: game_mod\EndfieldCamLink.dll 被进程占用,
#      覆盖解压会失败或被跳过; 插件则在 Blender 内存里, 覆盖文件不会立刻生效
# 这里一律只做提示, 不动用户的任何文件。
Write-Host "[0/4] 检查旧版残留..." -ForegroundColor Cyan
$legacyFound = $false

$legacyPresets = Join-Path $env:APPDATA 'EndfieldCameraLink\presets'
if (Test-Path $legacyPresets) {
    $legacyFound = $true
    $nLegacy = @(Get-ChildItem $legacyPresets -Filter *.py -ErrorAction SilentlyContinue).Count
    Write-Host ("  · 旧版预设目录(0.3.5 及以前使用, 现在不再读取): {0}" -f $legacyPresets) -ForegroundColor Yellow
    Write-Host ("    里面有 {0} 个 .py。当前版本读的是 <包根>\presets; 这个旧目录可以删掉, 也可以留着当备份。" -f $nLegacy) -ForegroundColor DarkGray
    Write-Host "    若旧场景(.blend)面板里的「预设文件夹」还指着它, 面板就仍会读到里面的旧文件 —— 把该字段清空即可。" -ForegroundColor DarkGray
}

if (Get-Process Endfield -ErrorAction SilentlyContinue) {
    $legacyFound = $true
    Write-Host "  · [重要] 游戏正在运行。" -ForegroundColor Yellow
    Write-Host "    game_mod\EndfieldCamLink.dll 正被进程占用: 覆盖解压会失败或被跳过," -ForegroundColor DarkGray
    Write-Host "    而且当前跑的是内存里那份旧模块。请退出游戏 -> 重新解压覆盖 -> 再跑一次本安装。" -ForegroundColor DarkGray
}
if (Get-Process blender -ErrorAction SilentlyContinue) {
    $legacyFound = $true
    Write-Host "  · Blender 正在运行: 覆盖插件文件后, 内存里仍是旧插件。" -ForegroundColor Yellow
    Write-Host "    请重启 Blender, 或在偏好设置里对本插件点一次「重新加载脚本 / Reload Scripts」。" -ForegroundColor DarkGray
}
if (-not $legacyFound) { Write-Host "  未发现旧版残留(全新安装)。" -ForegroundColor DarkGray }
Write-Host ""

if (-not $SkipPaths) {
    Write-Host "[1/4] 探测安装路径..." -ForegroundColor Cyan
    & (Join-Path $pkgRoot 'install\find_paths.ps1')
    Write-Host ""
} else {
    Write-Host "[1/4] 跳过路径探测" -ForegroundColor DarkGray
}

if (-not $SkipAddon) {
    Write-Host "[2/4] 安装并启用 Blender 插件..." -ForegroundColor Cyan
    try {
        & (Join-Path $pkgRoot 'install\install_blender_addon.ps1')
    } catch {
        Write-Warning ("插件安装失败: " + $_.Exception.Message)
        Write-Host "可改用手工方式: Blender 偏好设置 > 插件 > 从磁盘安装 > 选择 blender_addon\endfield_camera_bridge.zip" -ForegroundColor Yellow
    }
    Write-Host ""
} else {
    Write-Host "[2/4] 跳过插件安装" -ForegroundColor DarkGray
}

Write-Host "[3/4] 自检..." -ForegroundColor Cyan
$checks = @(
    @{ n = '注入模块 DLL';        p = 'game_mod\EndfieldCamLink.dll' },
    @{ n = '注入器';              p = 'game_mod\injector.exe' },
    @{ n = '模块配置';            p = 'game_mod\EndfieldCamLink.ini' },
    @{ n = '一键武装脚本';        p = 'tools\arm_lens.ps1' },
    @{ n = '启动脚本';            p = 'tools\launch_game.ps1' },
    @{ n = '控制台';              p = 'install\console.ps1' },
    @{ n = '启动器 · 相机控制台'; p = '相机控制台.cmd' },
    @{ n = 'Blender 插件';        p = 'blender_addon\endfield_camera_bridge\__init__.py' },
    @{ n = '插件安装包';          p = 'blender_addon\endfield_camera_bridge.zip' },
    @{ n = '使用文档';            p = 'docs\01_使用文档.md' },
    @{ n = '安装方案';            p = 'docs\02_其他电脑安装方案.md' },
    @{ n = '性质与风险评估';      p = 'docs\05_性质与侵入程度评估.md' },
    @{ n = '源码工程';            p = 'source\build.ps1' }
)
$fail = 0
foreach ($c in $checks) {
    $full = Join-Path $pkgRoot $c.p
    if (Test-Path $full) {
        Write-Host ("  [OK]   {0}" -f $c.n) -ForegroundColor Green
    } else {
        Write-Host ("  [缺失] {0}  ({1})" -f $c.n, $c.p) -ForegroundColor Red
        $fail++
    }
}
$iniOk = Test-Path (Join-Path $pkgRoot 'paths.ini')
Write-Host ("  [{0}] 路径配置 paths.ini" -f $(if ($iniOk) { 'OK' } else { '缺失' })) -ForegroundColor $(if ($iniOk) { 'Green' } else { 'Yellow' })

Write-Host ""
if ($fail -eq 0) {
    Write-Host "安装完成, 全部组件就位。" -ForegroundColor Green
} else {
    Write-Host "有 $fail 项缺失, 请检查包是否完整。" -ForegroundColor Red
}
Write-Host ""
Write-Host "接下来的使用流程(全部从包根目录的「相机控制台.cmd」进入):" -ForegroundColor Cyan
Write-Host "  双击 相机控制台.cmd" -ForegroundColor Green
Write-Host "    [2] 启动游戏并注入模块   ← 每次先做这步（会弹 UAC）"
Write-Host "    [3] 武装相机             ← 进入游戏世界后运行"
Write-Host "  （菜单里还有 [4] 关闭相机控制 / [5] 查看运行日志 / [6] 重新对齐原点 /"
Write-Host "    [7] 重建模块 / [8] 相机状态 / [9] 只安装 Blender 插件）"
Write-Host ""
Write-Host "  最后在 Blender: 侧栏(N) > Endfield Camera > 开始实时发送"
Write-Host ""
Write-Host "详细说明见 docs\01_使用文档.md"

param(
    [switch]$Force,        # 覆盖已存在的快捷方式
    [switch]$All,          # 兼容旧参数: 现在只剩一个启动器, 有无该开关结果相同
    [string]$Folder        # 可选: 快捷方式放到指定目录(默认桌面)
)
# 在桌面建立功能入口的快捷方式。
#
# v1.0.0au-fix: **安装时不再自动创建快捷方式**(按用户要求), 本脚本只由
#   控制台菜单 [0] 手工触发。包根目录如今只剩一个启动器「相机控制台.cmd」,
#   全部功能都在它的菜单里, 所以这里也只建这一个入口。
$ErrorActionPreference = 'Continue'
$pkgRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)

# 生成图标(失败不影响快捷方式创建)
$iconPath = Join-Path $pkgRoot 'assets\endfield_camera_link.ico'
try {
    if (-not (Test-Path $iconPath)) { & (Join-Path $pkgRoot 'install\make_icon.ps1') | Out-Null }
} catch {
    Write-Warning ("图标生成失败, 将使用系统默认图标: " + $_.Exception.Message)
}

# 唯一的入口: 相机控制台(交互菜单, 含全部功能)
$items = @(
    @{ Name = '终末地相机 - 控制台'; Cmd = '相机控制台.cmd'; Desc = '交互菜单: 安装/启动注入/武装/关闭/日志/对齐原点/重建/状态' }
)

# 桌面目录(兼容 OneDrive 重定向)
$desktop = if ($Folder) { $Folder } else { [Environment]::GetFolderPath('Desktop') }
if (-not (Test-Path $desktop)) { New-Item -ItemType Directory -Path $desktop -Force | Out-Null }

$shell = New-Object -ComObject WScript.Shell
$made = 0; $skipped = 0

foreach ($it in $items) {
    $target = Join-Path $pkgRoot $it.Cmd
    if (-not (Test-Path $target)) {
        Write-Warning ("启动器不存在, 跳过: " + $it.Cmd)
        continue
    }
    $lnk = Join-Path $desktop ($it.Name + '.lnk')
    if ((Test-Path $lnk) -and -not $Force) {
        Write-Host ("  [已存在] " + $it.Name) -ForegroundColor DarkGray
        $skipped++
        continue
    }
    try {
        $sc = $shell.CreateShortcut($lnk)
        $sc.TargetPath       = $target
        $sc.WorkingDirectory = $pkgRoot
        $sc.Description      = $it.Desc
        $sc.WindowStyle      = 1
        if (Test-Path $iconPath) { $sc.IconLocation = "$iconPath,0" }
        $sc.Save()
        Write-Host ("  [创建]   " + $it.Name) -ForegroundColor Green
        $made++
    } catch {
        Write-Warning ("  创建失败 {0}: {1}" -f $it.Name, $_.Exception.Message)
    }
}
[void][Runtime.InteropServices.Marshal]::ReleaseComObject($shell)

Write-Host ""
Write-Host ("桌面快捷方式: 新建 {0} 个, 跳过 {1} 个" -f $made, $skipped) -ForegroundColor Cyan
Write-Host ("位置: " + $desktop)
Write-Host ""
Write-Host "使用顺序: 双击「终末地相机 - 控制台」→ 菜单 [2] 启动游戏并注入 → 进游戏世界 → [3] 武装相机 → Blender 开始发送"

param(
    [string]$OutDir,          # 可选: 输出目录(默认 <包根>\dist)
    [string]$ZipName,         # 可选: 指定 zip 文件名(默认按"发布名称"生成, 见下)
    [string]$StageName,       # 可选: 包内根目录名(默认由发布名称去空格得到)
    [switch]$KeepStaging      # 可选: 保留临时暂存目录以便检查
)
# 制作"可直接发给别人"的干净分发包。
#
# 为什么要这一步 —— 直接压缩当前文件夹会有 4 个问题:
#   1) paths.ini 里是本机的绝对路径(游戏/Blender 安装位置), 对方机器上不存在
#   2) 运行期日志(EndfieldCamLink.log 等)会被一起带走
#   3) 源码工程的编译中间产物(obj\)与 Python 字节码(__pycache__)是垃圾
#   4) 从邮件/网盘下载的 zip 解压后每个文件会带"来自 Internet"标记(MOTW),
#      可能导致脚本被拦、exe 被 SmartScreen 警告 —— 所以包内附了放行脚本
#
# 产物命名(2026-09-15 起):
#   读 VERSION.txt 的 `发布名称 : ...` 一行作为**单一来源**:
#     发布名称 = "Endfield Camera Relink V1.0B"
#       -> 包内根目录 EndfieldCameraRelink_V1.0B\  (去空格)
#       -> 分发包     dist\Endfield_Camera_Relink_V1.0B.zip
#   "请先读我.txt" 的标题也取自同一个 $release, 不再硬编码(避免改了 VERSION.txt 而说明书还是旧版号)。
#   想手工指定: -ZipName / -StageName。**不同发布名称不会互相覆盖**(旧包留在 dist\ 里)。
$ErrorActionPreference = 'Stop'
$pkgRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
if (-not $OutDir) { $OutDir = Join-Path $pkgRoot 'dist' }
# 注意: 目标目录可能已经存在(例如直接指定盘根 D:\), 此时不能再 New-Item,
# 否则会报 "The path is not of a legal form"。
if (-not (Test-Path $OutDir)) { New-Item -ItemType Directory -Path $OutDir -Force | Out-Null }

# 版本号与发布名称: 都从 VERSION.txt 里取
$ver = '1.0.0'
$release = 'EndfieldCameraLink'
$verFile = Join-Path $pkgRoot 'VERSION.txt'
if (Test-Path $verFile) {
    $m = Select-String -Path $verFile -Pattern '包版本\s*:\s*([0-9.]+)' | Select-Object -First 1
    if ($m) { $ver = $m.Matches[0].Groups[1].Value }
    $m2 = Select-String -Path $verFile -Pattern '发布名称\s*:\s*(.+?)\s*$' | Select-Object -First 1
    if ($m2) { $release = $m2.Matches[0].Groups[1].Value.Trim() }
}
# 文件名/目录名里把空格换成下划线(中文保持原样)
$releaseFile = ($release -replace '\s+', '_')
if (-not $ZipName)   { $ZipName   = "$releaseFile.zip" }
if (-not $StageName) { $StageName = $releaseFile }
Write-Host ("发布名称: {0}  ->  分发包 {1}" -f $release, $ZipName) -ForegroundColor Cyan

# ---------- 1) 暂存 ----------
$stage = Join-Path $env:TEMP ("eflink_dist_" + [Guid]::NewGuid().ToString('N').Substring(0,8))
$stagePkg = Join-Path $stage $StageName
Write-Host "暂存目录: $stagePkg" -ForegroundColor DarkGray
New-Item -ItemType Directory -Path $stagePkg -Force | Out-Null

Write-Host "复制文件..." -ForegroundColor Cyan
# 不进分发包的本机开发物:
#   dist\          —— 历史分发包
#   obj\ / __pycache__\ —— 编译与 Python 中间产物
#   build_probe\ / build_ui\ / build_verify*\ —— "构建到别处以免改动被游戏占用的 DLL"用的临时输出目录
#   backup\        —— 回档快照(整包 zip + 旧成品 DLL + 取证参考构建)
$excludeDirs = @('dist', 'obj', '__pycache__', 'build_probe', 'build_ui', 'build_verify',
                 'build_verify2', 'backup')
# 注意: /XD 传**目录名**(不是全路径) —— 这样任意深度下同名目录都会被排除。
# 踩过: 只排除包根的同名目录时, source\backup\... 这类子目录里的开发物会漏进分发包。
robocopy $pkgRoot $stagePkg /E /NFL /NDL /NJH /NJS /NP /XD $excludeDirs | Out-Null
if ($LASTEXITCODE -ge 8) { throw "robocopy 失败, 退出码 $LASTEXITCODE" }

# ---------- 2) 清掉本机专属状态与运行期产物 ----------
Write-Host "清理本机专属状态与运行期产物..." -ForegroundColor Cyan
$removed = @()
# paths.ini: 换成空模板, 让对方的安装程序自己探测
$staleIni = Join-Path $stagePkg 'paths.ini'
if (Test-Path $staleIni) { Remove-Item $staleIni -Force; $removed += 'paths.ini(本机路径)' }
$tmpl = @'
; EndfieldCameraLink 路径配置
; 运行「相机控制台.cmd」-> 菜单 [1] 安装配置 会自动探测并填写这里；也可以手工填写。
; 路径含空格不需要加引号。
; 示例:
;   game_exe=D:\Hypergryph Launcher\games\Endfield Game\Endfield.exe
;   launcher_exe=D:\Hypergryph Launcher\Launcher.exe
;   blender_exe=C:\Program Files\Blender Foundation\Blender 5.2\blender.exe
;
; 以下两行由安装脚本自动填写(运镜预设放在**包内**, 与包一起分发):
;   package_dir = 本包根目录
;   preset_dir  = 运镜预设目录(默认 <包根>\presets; 留空则自动取该默认值)

game_exe=
launcher_exe=
blender_exe=
'@
[System.IO.File]::WriteAllText($staleIni, $tmpl, [System.Text.UTF8Encoding]::new($true))
$removed += 'paths.ini(已换成空模板)'

# 运行期日志(注意: docs\reference\ 下的符号清单是资料, 保留)
foreach ($p in @('game_mod\EndfieldCamLink.log', 'game_mod\injector.log', 'game_mod\rebuild.log',
                 'source\rebuild.log', 'injector.log')) {
    $f = Join-Path $stagePkg $p
    if (Test-Path $f) { Remove-Item $f -Force; $removed += $p }
}
Get-ChildItem $stagePkg -Recurse -Filter '__pycache__' -Directory -ErrorAction SilentlyContinue |
    ForEach-Object { Remove-Item $_.FullName -Recurse -Force; $removed += ($_.FullName.Replace($stagePkg+'\','') + '\') }
Get-ChildItem $stagePkg -Recurse -Filter '*.lnk' -ErrorAction SilentlyContinue |
    ForEach-Object { Remove-Item $_.FullName -Force; $removed += $_.Name }
$removed | Sort-Object -Unique | ForEach-Object { Write-Host ("  移除/重置: " + $_) -ForegroundColor DarkGray }

# 顺手刷新插件安装包, 保证 zip 与目录内源码一致
$addonDir = Join-Path $stagePkg 'blender_addon\endfield_camera_bridge'
$addonZip = Join-Path $stagePkg 'blender_addon\endfield_camera_bridge.zip'
if (Test-Path $addonZip) { Remove-Item $addonZip -Force }
Compress-Archive -Path $addonDir -DestinationPath $addonZip -Force

# ---------- 2.5) 脚本 BOM 自检 ----------
# 中文 Windows(本机 ANSI 代码页 936)下, 没有 UTF-8 BOM 的 .ps1 会被 Windows PowerShell 当成
# ANSI 读取 → 中文乱码甚至语法错误(本项目已两次踩到)。打包前自动查一遍, 只警告不阻断。
$noBom = @()
Get-ChildItem $stagePkg -Recurse -Filter *.ps1 -File -ErrorAction SilentlyContinue | ForEach-Object {
    $b = [System.IO.File]::ReadAllBytes($_.FullName)
    if ($b.Length -lt 3 -or $b[0] -ne 0xEF -or $b[1] -ne 0xBB -or $b[2] -ne 0xBF) {
        $noBom += $_.FullName.Replace($stagePkg + '\', '')
    }
}
if ($noBom.Count -gt 0) {
    Write-Host ("  [警告] 有 {0} 个 .ps1 缺 UTF-8 BOM(中文会乱码) —— 请先跑 tools\fix_ps1_bom.ps1 再打包:" -f $noBom.Count) -ForegroundColor Yellow
    $noBom | ForEach-Object { Write-Host ("    " + $_) -ForegroundColor Yellow }
} else {
    Write-Host "  脚本自检: .ps1 全部带 UTF-8 BOM" -ForegroundColor DarkGray
}

# ---------- 3) 放入"首次使用"说明 ----------
$readmeFirst = @"
解压后请先做这一步
==================

1. 把整个 __STAGE__ 文件夹解压到任意非系统盘目录
   （例如 D:\__STAGE__）。不要放进游戏安装目录。

2. 如果这是从邮件/网盘下载的压缩包，先右键 zip 文件 -> 属性 ->
   勾选“解除锁定”再解压；或解压后运行 install\unblock_files.ps1。

3. 双击  相机控制台.cmd    ← **全部操作都在这一个入口里, 没有别的 .cmd**
   菜单 [1] 安装配置 会自动: 探测游戏与 Blender 路径 / 安装并启用 Blender 插件 / 完整性自检

   ★ 如果你是**从旧版升级**（手上已经有旧版的文件夹）:
     只需 退出游戏 + 退出 Blender -> 用本压缩包覆盖解压到原目录 -> 再跑一次
     菜单 [1] 安装配置 即可，不需要卸载旧版、不需要手工删文件。
     （原因: 它本身就是升级脚本，会把路径、插件、包内预设目录全部按新版重做一遍）
     完整方案见 docs\08_升级与旧版迁移.md —— 里面有必须补做的 3 件事，
     和一段可以直接复制发给别人的说明。

4. 之后每次的流程(仍在同一个控制台里):
     菜单 [2] 启动游戏并注入模块   （每次先做，会弹 UAC 点“是”）
     菜单 [3] 武装相机             （进入游戏世界后做）
     然后在 Blender: 按 N -> 侧栏 Endfield Camera -> 开始实时发送

   （菜单里还有 [4] 关闭相机控制 / [5] 查看运行日志 / [6] 重新对齐位置原点 /
     [7] 从源码重建模块 / [8] 相机状态 / [9] 只安装 Blender 插件）

遇到问题先看 docs\03_故障排查.md
"@
$readmeFirst = $readmeFirst -replace '__STAGE__', $StageName
$readmeFirst = ("$release`r`n署名: 一块铅矾    辅助模型: deepseek V4.1 flash / GPT6 Astra`r`n" + ("=" * 64) + "`r`n`r`n") + $readmeFirst
[System.IO.File]::WriteAllText((Join-Path $stagePkg '请先读我.txt'), $readmeFirst,
    [System.Text.UTF8Encoding]::new($true))

# ---------- 4) 打包 ----------
# 文件名由发布名称决定(见文件头说明); $ver 仅用于日志
$zipPath = Join-Path $OutDir $zipName
if (Test-Path $zipPath) { Remove-Item $zipPath -Force }
Write-Host "压缩..." -ForegroundColor Cyan
Compress-Archive -Path $stagePkg -DestinationPath $zipPath -CompressionLevel Optimal -Force

# 暂存目录里的文件数要在删除前统计(否则永远是 0)
$files = (Get-ChildItem $stagePkg -Recurse -File -ErrorAction SilentlyContinue | Measure-Object).Count

if (-not $KeepStaging) { Remove-Item $stage -Recurse -Force -ErrorAction SilentlyContinue }

# ---------- 5) 报告 ----------
$zi = Get-Item $zipPath
$hash = (Get-FileHash $zipPath -Algorithm SHA256).Hash
# zip 自身的哈希无法写进包内(VERSION.txt 就在包里), 所以放在旁边的 sidecar 文件里,
# 分发时把这个值一起给对方核对。
$sidecar = "$zipPath.sha256.txt"
$sidecarText = @(
    ("{0}  {1}" -f $hash, $zipName),
    ("大小: {0} 字节 ({1} MB)" -f $zi.Length, [math]::Round($zi.Length/1MB, 2)),
    ("内容条目: {0} 个文件" -f $files),
    ("打包时间: {0}" -f (Get-Date -Format 'yyyy-MM-dd HH:mm:ss')),
    "",
    "核对方式(PowerShell):",
    ("  Get-FileHash '{0}' -Algorithm SHA256" -f $zipName)
) -join "`r`n"
[System.IO.File]::WriteAllText($sidecar, $sidecarText, [System.Text.UTF8Encoding]::new($true))

Write-Host ""
Write-Host "分发包已生成" -ForegroundColor Green
Write-Host ("  文件: {0}" -f $zipPath)
Write-Host ("  大小: {0} MB ({1} 字节)" -f [math]::Round($zi.Length/1MB, 2), $zi.Length)
Write-Host ("  条目: {0} 个文件" -f $files)
Write-Host ("  SHA256: {0}" -f $hash)
Write-Host ("  校验值文件: {0}" -f $sidecar)
Write-Host ""
Write-Host "校验值可直接发给对方, 让他解压后核对内容未被篡改/损坏。" -ForegroundColor DarkGray
Write-Host "旧版升级方案见 docs\08_升级与旧版迁移.md(退游戏+退 Blender -> 覆盖 -> 跑控制台菜单 [1])。" -ForegroundColor DarkGray

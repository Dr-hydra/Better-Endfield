param(
    [string]$AddonSource,   # 可选: 自定义插件目录(默认用包内的 blender_addon\endfield_camera_bridge)
    [string]$BlenderExe,    # 可选: 指定 blender.exe
    [switch]$NoEnable       # 可选: 只复制文件, 不自动在 Blender 里启用
)
# 安装 Endfield Camera Bridge 插件到 Blender, 并**自动启用**。
#
# 两步:
#   1) 复制插件文件到"用户插件目录"(不改动 Blender 安装目录, 无需管理员权限):
#        %APPDATA%\Blender Foundation\Blender\<版本>\scripts\addons\endfield_camera_bridge\
#   2) 后台启动一次 Blender, 调用 addon_utils.enable(default_set=True) 再 save_userpref(),
#      让插件真正被勾选 —— 否则只复制文件, 用户还得手工去偏好设置里勾。
#
# 注意(实测踩过): 步骤 2 **绝不能**加 --factory-startup。
#   加了会以出厂设置启动, 之后 save_userpref() 会把用户的偏好设置覆盖成出厂值。
#   必须让 Blender 正常加载用户真实偏好, 只改动插件启用列表。
$ErrorActionPreference = 'Stop'
$pkgRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)

if (-not $AddonSource) { $AddonSource = Join-Path $pkgRoot 'blender_addon\endfield_camera_bridge' }
if (-not (Test-Path (Join-Path $AddonSource '__init__.py'))) {
    throw "找不到插件源码: $AddonSource\__init__.py"
}

$iniPath = Join-Path $pkgRoot 'paths.ini'
if (-not $BlenderExe -and (Test-Path $iniPath)) {
    $BlenderExe = (Get-Content $iniPath | Where-Object { $_ -match '^blender_exe=' }) -replace '^blender_exe=', ''
}
if ($BlenderExe -and -not (Test-Path $BlenderExe)) { $BlenderExe = $null }

# ---------- 1) 复制文件 ----------
$versions = @()
$blenderVersion = $null
if ($BlenderExe) {
    $v = (& $BlenderExe --version 2>$null | Select-Object -First 1)
    if ($v -match '(\d+\.\d+)') { $blenderVersion = $Matches[1]; $versions += $blenderVersion }
}
$userRoot = Join-Path $env:APPDATA 'Blender Foundation\Blender'
if (Test-Path $userRoot) {
    $versions += (Get-ChildItem $userRoot -Directory | Select-Object -ExpandProperty Name)
}
if (-not $versions) { $versions = @('5.2','4.2','3.6') }
$versions = $versions | Sort-Object -Unique

Write-Host "步骤 1/2 · 复制插件文件" -ForegroundColor Cyan
if ($blenderVersion) { Write-Host ("  检测到 Blender 版本: " + $blenderVersion) -ForegroundColor DarkGray }
$installed = @()
foreach ($ver in $versions) {
    $dst = Join-Path $userRoot "$ver\scripts\addons\endfield_camera_bridge"
    try {
        New-Item -ItemType Directory -Path $dst -Force | Out-Null
        # 复制插件**整棵目录**(不能只复制 __init__.py: 0.3.0 起还有 presets\ 子目录,
        # 里面是随插件分发的示例运镜预设 —— 只复制单文件会让示例预设消失)。
        # 跳过 __pycache__(编译缓存不应随包分发)。
        Copy-Item (Join-Path $AddonSource '__init__.py') -Destination $dst -Force
        Get-ChildItem $AddonSource -Directory -ErrorAction SilentlyContinue |
            Where-Object { $_.Name -ne '__pycache__' } | ForEach-Object {
                Copy-Item $_.FullName -Destination $dst -Recurse -Force
            }
        Get-ChildItem $AddonSource -File -ErrorAction SilentlyContinue |
            Where-Object { $_.Name -ne '__init__.py' -and $_.Extension -in '.py', '.json', '.md' } |
            ForEach-Object { Copy-Item $_.FullName -Destination $dst -Force }
        foreach ($junk in @('__pycache__')) {
            $p = Join-Path $dst $junk
            if (Test-Path $p) { Remove-Item $p -Recurse -Force }
        }
        # v0.3.6: 把包根写进插件目录 —— 插件装在 %APPDATA% 里、与包根没有相对关系,
        # 所以必须由安装脚本告知, 否则插件找不到包内的 presets\ 与 paths.ini
        # (预设数据与路径设置自 v0.3.6 起都放在包内, 方便整个包分发)。
        $enc = New-Object System.Text.UTF8Encoding($false)
        [System.IO.File]::WriteAllText((Join-Path $dst 'package_path.txt'), $pkgRoot, $enc)
        $nPreset = @(Get-ChildItem (Join-Path $dst 'presets') -Filter *.py -ErrorAction SilentlyContinue).Count
        $installed += [pscustomobject]@{ Ver = $ver; Path = $dst }
        Write-Host ("  [OK] Blender {0}  ->  {1}  (随插件示例预设 {2} 个)" -f $ver, $dst, $nPreset) -ForegroundColor Green
    } catch {
        Write-Warning ("  [失败] Blender {0}: {1}" -f $ver, $_.Exception.Message)
    }
}
if (-not $installed) { throw "插件文件复制全部失败" }

# ---------- 1b) 包内预设目录与路径设置(v0.3.6) ----------
# 预设数据放在 <包根>\presets; 找不到就从插件的示例预设补齐(不动用户已改过的文件 —— 那份逻辑
# 在插件里也有, 这里只做"包内目录不存在/为空"时的播种)。
Write-Host ""
Write-Host "步骤 1b/2 · 包内预设目录与路径设置" -ForegroundColor Cyan
$presetDir = Join-Path $pkgRoot 'presets'
New-Item -ItemType Directory -Path $presetDir -Force | Out-Null
$seed = Join-Path $AddonSource 'presets'
$seeded = 0
if (Test-Path $seed) {
    foreach ($f in (Get-ChildItem $seed -Filter *.py -ErrorAction SilentlyContinue)) {
        $t = Join-Path $presetDir $f.Name
        if (-not (Test-Path $t)) { Copy-Item $f.FullName $t -Force; $seeded++ }
    }
}
Write-Host ("  [OK] 预设目录: {0}  (本次补齐 {1} 个, 现有 {2} 个)" -f `
            $presetDir, $seeded, @(Get-ChildItem $presetDir -Filter *.py -ErrorAction SilentlyContinue).Count) `
           -ForegroundColor Green

# 把 package_dir / preset_dir 写进包内 paths.ini(路径设置也在包内, 与包一起分发)
$iniPath = Join-Path $pkgRoot 'paths.ini'
$iniLines = @()
if (Test-Path $iniPath) { $iniLines = @(Get-Content $iniPath -Encoding UTF8) }
$iniLines = @($iniLines | Where-Object { $_ -notmatch '^(package_dir|preset_dir)\s*=' })
$iniLines += "; ---- 运镜预设(v0.3.6 起: 预设数据与路径设置都放在包内, 方便整个包分发) ----"
$iniLines += ("package_dir=" + $pkgRoot)
$iniLines += ("preset_dir=" + $presetDir)
$enc = New-Object System.Text.UTF8Encoding($false)
[System.IO.File]::WriteAllLines($iniPath, $iniLines, $enc)
Write-Host ("  [OK] 已写入 paths.ini: package_dir / preset_dir") -ForegroundColor Green

# ---------- 2) 自动启用 ----------
Write-Host ""
Write-Host "步骤 2/2 · 在 Blender 里自动启用插件" -ForegroundColor Cyan

if ($NoEnable) {
    Write-Host "  已按 -NoEnable 跳过自动启用。" -ForegroundColor DarkGray
} elseif (-not $BlenderExe) {
    Write-Host "  未找到 blender.exe, 无法自动启用。" -ForegroundColor Yellow
    Write-Host "  请手工勾选: 编辑 > 偏好设置 > 插件 > 搜索 Endfield > 勾选" -ForegroundColor Yellow
} elseif (Get-Process blender -ErrorAction SilentlyContinue) {
    Write-Host "  检测到 Blender 正在运行 —— 跳过自动启用。" -ForegroundColor Yellow
    Write-Host "  原因: 正在运行的 Blender 退出时会回写偏好设置, 可能覆盖本次改动。" -ForegroundColor DarkGray
    Write-Host "  两种处理:" -ForegroundColor Yellow
    Write-Host "    a) 关闭 Blender 后重新运行本安装（会全自动启用）"
    Write-Host "    b) 在 Blender 里手工勾选: 编辑 > 偏好设置 > 插件 > 搜索 Endfield"
} else {
    $py = @'
import addon_utils, bpy, sys
name = 'endfield_camera_bridge'
try:
    addon_utils.enable(name, default_set=True, persistent=True)
except Exception as e:
    print('EF_ERR_ENABLE:', type(e).__name__, e)
print('EF_STATE:', addon_utils.check(name))
try:
    print('EF_SAVE:', bpy.ops.wm.save_userpref())
except Exception as e:
    print('EF_ERR_SAVE:', type(e).__name__, e)
sys.stdout.flush()
'@
    $pyPath = Join-Path $env:TEMP 'ef_enable_addon.py'
    [System.IO.File]::WriteAllText($pyPath, $py, [System.Text.UTF8Encoding]::new($false))
    Write-Host "  正在后台启动 Blender 以启用插件（约 5-15 秒, 不会弹出界面）..." -ForegroundColor DarkGray
    # 关键: 不加 --factory-startup, 否则会覆盖用户偏好设置
    $out = & $BlenderExe --background --python $pyPath 2>&1
    $stateLine = ($out | Select-String -Pattern 'EF_STATE:' | Select-Object -First 1)
    $saveLine  = ($out | Select-String -Pattern 'EF_SAVE:'  | Select-Object -First 1)
    $errLine   = ($out | Select-String -Pattern 'EF_ERR_'   | Select-Object -First 1)

    if ($errLine)   { Write-Warning ("  " + $errLine.Line) }
    if ($stateLine) { Write-Host ("  " + $stateLine.Line) -ForegroundColor DarkGray }

    if ($stateLine -and $stateLine.Line -match '\(True,\s*True\)' -and
        $saveLine  -and $saveLine.Line  -match 'FINISHED') {
        Write-Host "  [OK] 插件已启用, 偏好设置已保存 —— 无需再手工勾选。" -ForegroundColor Green
    } else {
        Write-Host "  自动启用未能确认成功。" -ForegroundColor Yellow
        Write-Host "  请手工勾选: Blender > 编辑 > 偏好设置 > 插件 > 搜索 Endfield" -ForegroundColor Yellow
    }
}

Write-Host ""
Write-Host "安装完成。在 Blender 的 3D 视图按 N, 侧栏会出现 Endfield Camera 标签页。" -ForegroundColor Cyan
Write-Host ("若偏好设置里搜不到, 也可用 ZIP 安装: " + (Join-Path $pkgRoot 'blender_addon\endfield_camera_bridge.zip'))

<#
  Endfield Poser —— 发布打包脚本

  用法：
    powershell -ExecutionPolicy Bypass -File tools\package_release.ps1 -Version 0.3.6

  产出：
    build/release/EndfieldPoser-v<版本>/          组装好的目录
    build/release/EndfieldPoser-v<版本>.zip       可直接上传 Releases 的压缩包

  说明：
    - DLL 由 tools\build_msvc.ps1 产出到 plugin\，本脚本只组装与校验，不编译。
    - 包内固定带 LICENSE 与 THIRD_PARTY_NOTICES（AGPL 分发要求），并在打包前跑一遍
      「署名自查」：以 THIRD_PARTY_NOTICES 里列出的名称为关键词扫包内文本文件，
      THIRD_PARTY_NOTICES 与 licenses/ 许可文件除外；命中即报错。
    - 面向用户的四个文件来自版本库：docs/notice.md（→ 使用须知.md）、
      docs/tutorial.md（→ 使用教程.md）、packaging\安装说明.txt、packaging\poser_config.txt。
#>
[CmdletBinding()]
param(
  [Parameter(Mandatory = $true)][string]$Version,
  [string]$InstallNotes = '',
  [string]$PluginConfig = '',
  [string]$Tutorial     = '',
  [switch]$AllowStaleDlls,
  [switch]$AllowVersionMismatch,
  [switch]$Force,
  [switch]$NoZip
)

$ErrorActionPreference = 'Stop'
$root      = Split-Path -Parent $PSScriptRoot
$releaseRoot = Join-Path $root 'build\release'
$stage     = Join-Path $releaseRoot "EndfieldPoser-v$Version"
$zipPath   = Join-Path $releaseRoot "EndfieldPoser-v$Version.zip"

$tutorialSrc = if ($Tutorial)     { $Tutorial }     else { Join-Path $root 'docs\tutorial.md' }
$notesSrc    = if ($InstallNotes) { $InstallNotes } else { Join-Path $root 'packaging\安装说明.txt' }
$configSrc   = if ($PluginConfig) { $PluginConfig } else { Join-Path $root 'packaging\poser_config.txt' }
$noticeSrc   = Join-Path $root 'docs\notice.md'

$required = @(
  (Join-Path $root 'LICENSE'),
  (Join-Path $root 'THIRD_PARTY_NOTICES'),
  (Join-Path $root 'licenses\cloth-upstream.txt'),
  (Join-Path $root 'licenses\Brotli.txt'),
  (Join-Path $root 'build\cloth_resources.exe'),
  (Join-Path $root '安全安装.bat'),
  (Join-Path $root 'tools\deploy.ps1'),
  (Join-Path $root 'tools\character_face_resources.ps1'),
  (Join-Path $root 'docs\mmd-player.md'),
  $noticeSrc, $tutorialSrc, $notesSrc, $configSrc,
  (Join-Path $root 'plugin\poser.dll'),
  (Join-Path $root 'plugin\d3dcompiler_47.dll'),
  (Join-Path $root 'plugin\vulkan-1.dll')
)
$missing = $required | Where-Object { -not (Test-Path -LiteralPath $_) }
if ($missing) {
  Write-Host '缺失以下文件，先跑 tools\build_msvc.ps1：' -ForegroundColor Red
  $missing | ForEach-Object { Write-Host "  $_" }
  exit 1
}

# --- 版本一致性 1：src/core/version.h 是版本号唯一来源 ---
$versionHeader = Join-Path $root 'src\core\version.h'
$vh = Get-Content -LiteralPath $versionHeader -Encoding UTF8 -Raw
$codeVersion = '{0}.{1}.{2}' -f `
  ([regex]::Match($vh, 'POSER_VERSION_MAJOR\s+(\d+)').Groups[1].Value), `
  ([regex]::Match($vh, 'POSER_VERSION_MINOR\s+(\d+)').Groups[1].Value), `
  ([regex]::Match($vh, 'POSER_VERSION_PATCH\s+(\d+)').Groups[1].Value)
if ($Version -notmatch '^\d+\.\d+\.\d+$') {
  Write-Host "警告：版本号 $Version 不是 x.y.z 形式，跳过 version.h 与安装说明的比对" -ForegroundColor Yellow
} elseif ($codeVersion -ne $Version) {
  if ($AllowVersionMismatch) {
    Write-Host "警告：src/core/version.h 是 $codeVersion，与 -Version $Version 不一致" -ForegroundColor Yellow
  } else {
    Write-Host "src/core/version.h 的版本与 -Version 不一致：" -ForegroundColor Red
    Write-Host "  version.h：$codeVersion"
    Write-Host "  参数：     $Version    （改 version.h 后重新编译，确要照发请加 -AllowVersionMismatch）"
    exit 1
  }
}

# --- 版本一致性 2：安装说明.txt 第一行应带当前版本号 ---
$notesHead = Get-Content -LiteralPath $notesSrc -Encoding UTF8 -TotalCount 1
if ($notesHead -notmatch [regex]::Escape($Version)) {
  if ($AllowVersionMismatch) {
    Write-Host "警告：$notesSrc 首行没有版本号 $Version（$notesHead）" -ForegroundColor Yellow
  } else {
    Write-Host "安装说明.txt 的版本与 -Version 不一致：" -ForegroundColor Red
    Write-Host "  首行：$notesHead"
    Write-Host "  参数：$Version    （确要照发请加 -AllowVersionMismatch）"
    exit 1
  }
}

# --- DLL 新鲜度：源码比 poser.dll 新就提醒 ---
$dll = Join-Path $root 'plugin\poser.dll'
# Inspect the built DLL, not a cache or sidecar that may describe an older build.
& (Join-Path $PSScriptRoot 'assert_release_overlay.ps1') -Dll $dll
$dllTime = (Get-Item -LiteralPath $dll).LastWriteTime
$newer = Get-ChildItem -Recurse -File -LiteralPath (Join-Path $root 'src') |
  Where-Object { $_.LastWriteTime -gt $dllTime }
if ($newer) {
  Write-Host "警告：有 $($newer.Count) 个源文件比 poser.dll 新，包里的 DLL 可能是旧的（先跑 build_msvc.ps1）" -ForegroundColor Yellow
  $newer | Select-Object -First 5 | ForEach-Object { Write-Host "  $($_.Name)  $($_.LastWriteTime)" }
  if (-not $AllowStaleDlls) { throw '请先重新构建；确需旧 DLL 时显式使用 -AllowStaleDlls。' }
}

# The DLL must contain the same clothing payloads as this source tree.
& (Join-Path $root 'build\cloth_resources.exe') --repo $root --dll $dll
if ($LASTEXITCODE -ne 0) { throw 'Embedded clothing resource verification failed.' }

# --- 组装 ---
if (Test-Path -LiteralPath $stage) {
  if (-not $Force) {
    Write-Host "$stage 已存在；要覆盖请加 -Force" -ForegroundColor Red
    exit 1
  }
  $stageFull = [IO.Path]::GetFullPath($stage)
  if (-not $stageFull.StartsWith([IO.Path]::GetFullPath($releaseRoot).TrimEnd('\') + '\', [StringComparison]::OrdinalIgnoreCase)) {
    Write-Host "拒绝删除不在 build\release 下的路径：$stageFull" -ForegroundColor Red
    exit 1
  }
  Remove-Item -LiteralPath $stage -Recurse -Force
}
New-Item -ItemType Directory -Force -Path (Join-Path $stage 'plugin') | Out-Null

Copy-Item -LiteralPath (Join-Path $root 'licenses') -Destination (Join-Path $stage 'licenses') -Recurse -Force
Copy-Item -LiteralPath (Join-Path $root 'LICENSE')            -Destination (Join-Path $stage 'LICENSE') -Force
Copy-Item -LiteralPath (Join-Path $root 'THIRD_PARTY_NOTICES') -Destination (Join-Path $stage 'THIRD_PARTY_NOTICES') -Force
Copy-Item -LiteralPath (Join-Path $root '安全安装.bat')         -Destination (Join-Path $stage '安全安装.bat') -Force
Copy-Item -LiteralPath $noticeSrc                              -Destination (Join-Path $stage '使用须知.md') -Force
Copy-Item -LiteralPath $tutorialSrc                            -Destination (Join-Path $stage '使用教程.md') -Force
Copy-Item -LiteralPath $notesSrc                               -Destination (Join-Path $stage '安装说明.txt') -Force
Copy-Item -LiteralPath $configSrc                              -Destination (Join-Path $stage 'plugin\poser_config.txt') -Force
Copy-Item -LiteralPath (Join-Path $root 'plugin\poser.dll')            -Destination (Join-Path $stage 'plugin\poser.dll') -Force
Copy-Item -LiteralPath (Join-Path $root 'plugin\d3dcompiler_47.dll')   -Destination (Join-Path $stage 'd3dcompiler_47.dll') -Force
Copy-Item -LiteralPath (Join-Path $root 'plugin\vulkan-1.dll')         -Destination (Join-Path $stage 'vulkan-1.dll') -Force
& (Join-Path $PSScriptRoot 'copy_character_faces.ps1') -Destination (Join-Path $stage 'plugin\mmd\character-faces')

# Include the complete installer payload. Runtime profiles remain in plugin/
# for manual installs; the installer uses resources/ and preserves user edits.
New-Item -ItemType Directory -Force -Path (Join-Path $stage 'tools'), (Join-Path $stage 'docs'), (Join-Path $stage 'resources\character-faces') | Out-Null
foreach ($tool in @('deploy.ps1', 'character_face_resources.ps1')) {
  Copy-Item -LiteralPath (Join-Path $root ('tools\' + $tool)) -Destination (Join-Path $stage ('tools\' + $tool))
}
Copy-Item -LiteralPath (Join-Path $root 'docs\mmd-player.md') -Destination (Join-Path $stage 'docs\mmd-player.md')
Copy-Item -LiteralPath (Join-Path $root 'docs\tutorial.md') -Destination (Join-Path $stage 'docs\tutorial.md')
Copy-Item -LiteralPath (Join-Path $root 'docs\notice.md') -Destination (Join-Path $stage 'docs\notice.md')
Copy-Item -LiteralPath (Join-Path $root 'docs\roadmap.md') -Destination (Join-Path $stage 'docs\roadmap.md')
Copy-Item -LiteralPath (Join-Path $root 'docs\qq-group.jpg') -Destination (Join-Path $stage 'docs\qq-group.jpg')
Copy-Item -LiteralPath (Join-Path $root 'README.md') -Destination (Join-Path $stage 'README.md')
foreach ($rootDoc in @('使用须知.md', '使用教程.md')) {
  $docPath = Join-Path $stage $rootDoc
  $docText = [IO.File]::ReadAllText($docPath)
  $docText = $docText.Replace('](mmd-player.md', '](docs/mmd-player.md')
  [IO.File]::WriteAllText($docPath, $docText, [Text.UTF8Encoding]::new($true))
}
foreach ($profile in Get-ChildItem -LiteralPath (Join-Path $root 'resources\character-faces') -Filter '*.face.json' -File) {
  Copy-Item -LiteralPath $profile.FullName -Destination (Join-Path $stage 'resources\character-faces')
}

# --- 发版前署名自查 ---
$names = @()
$noticeText = Get-Content -LiteralPath (Join-Path $root 'THIRD_PARTY_NOTICES') -Encoding UTF8
foreach ($line in $noticeText) {
  if ($line -match '^\s*\d+\)\s*([^（(]+)') {
    $token = $Matches[1].Trim()
    if ($token -match '/') { $names += ($token -split '/') }
  }
  if ($line -match '^\s*-\s*([^（(]+)（') { $names += $Matches[1].Trim() }
}
$names = $names | ForEach-Object { $_.Trim() } | Where-Object { $_ -and $_ -notmatch '^第三方' } | Select-Object -Unique

$scanExt  = @('.md', '.txt', '.bat', '.json', '.ini', '.cfg', '.ps1')
$scanHits = @()
$riskyHits = @()
$riskyWords = @('反作弊', 'AntiCheat', '绕过检测', '规避检测')
Get-ChildItem -Recurse -File -LiteralPath $stage | ForEach-Object {
  if ($scanExt -notcontains $_.Extension.ToLower()) { return }
  if ($_.Name -eq 'THIRD_PARTY_NOTICES' -or $_.FullName.StartsWith((Join-Path $stage 'licenses') + '\', [StringComparison]::OrdinalIgnoreCase)) { return }
  $text = Get-Content -LiteralPath $_.FullName -Raw -Encoding UTF8
  foreach ($n in $names) {
    if ($text -match [regex]::Escape($n)) { $scanHits += "$($_.Name) <- $n" }
  }
  foreach ($w in $riskyWords) {
    if ($text -match [regex]::Escape($w)) { $riskyHits += "$($_.Name) <- $w" }
  }
}
if ($riskyHits) {
  Write-Host '警告：命中需要复核的措辞：' -ForegroundColor Yellow
  $riskyHits | Select-Object -Unique | ForEach-Object { Write-Host "  $_" }
}

# 文案漂移检查：默认热键早已从 F11/F12 改成 L/P，默认发行版使用官方启动器且不含 XXMI 兼容层。
# 包内文档若还留着单独出现的 F11/F12（例如老版安装向导里的"F12：呼出面板"），
# 说明有文档没跟着更新 —— 实测踩过（安全安装.bat 停留在 v0.3.0 的说法）。
# 用 Latin-1 读文件：无论内容是 UTF-8 还是 GBK，ASCII 片段都能原样匹配；F12/F11 这种
# 成对写法（迁移说明里会用）不算，只看单独出现的。
$latin = [Text.Encoding]::GetEncoding(28591)
$driftHits = @()
Get-ChildItem -Recurse -File -LiteralPath $stage | ForEach-Object {
  if ($scanExt -notcontains $_.Extension.ToLower()) { return }
  $name = $_.Name
  $raw = $latin.GetString([IO.File]::ReadAllBytes($_.FullName))
  # 排除这些正当写法：F12/F11（迁移说明）、Ctrl+F12（修饰键）、F10~F12（范围）
  foreach ($m in [regex]::Matches($raw, '(?<![/+~0-9A-Za-z])F1[12](?![/~0-9A-Za-z])')) {
    $driftHits += "$name <- $($m.Value)"
  }
}
if ($driftHits) {
  Write-Host '警告：包内文档出现单独的热键 F11/F12（默认已是 L / P，疑似文案没更新）：' -ForegroundColor Yellow
  $driftHits | Select-Object -Unique | ForEach-Object { Write-Host "  $_" }
}

if ($scanHits) {
  Write-Host '违规：包内文档出现了 THIRD_PARTY_NOTICES 里列出的名称（除许可文件）：' -ForegroundColor Red
  $scanHits | Select-Object -Unique | ForEach-Object { Write-Host "  $_" }
  Write-Host '按仓库约定，对外文档只能写「上游开源来源（见 THIRD_PARTY_NOTICES）」。' -ForegroundColor Red
  exit 1
}
Write-Host "署名自查通过（关键词：$($names -join ', ')）" -ForegroundColor Green

# --- 压缩 ---
if (-not $NoZip) {
  if (Test-Path -LiteralPath $zipPath) { Remove-Item -LiteralPath $zipPath -Force }
  Add-Type -AssemblyName System.IO.Compression.FileSystem
  try {
    # entryNameEncoding 传 $null：.NET 用 UTF-8 写条目名并置 UTF-8 标志位，
    # 资源管理器 / 7-Zip 都能正确识别中文文件名（显式传 UTF8Encoding 反而会让部分工具读错）。
    [System.IO.Compression.ZipFile]::CreateFromDirectory(
      $stage, $zipPath,
      [System.IO.Compression.CompressionLevel]::Optimal,
      $true,
      $null)
  } catch {
    Write-Host "ZipFile.CreateFromDirectory 失败，回退 Compress-Archive：$($_.Exception.Message)" -ForegroundColor Yellow
    Compress-Archive -Path $stage -DestinationPath $zipPath -Force
  }
}

# --- 输出 ---
Write-Host ''
Write-Host "组装完成：$stage"
Get-ChildItem -Recurse -File -LiteralPath $stage | ForEach-Object {
  Write-Host ("  {0,10}  {1}" -f $_.Length, $_.FullName.Substring($stage.Length + 1))
}
if (-not $NoZip) {
  $h = Get-FileHash -LiteralPath $zipPath -Algorithm SHA256
  Write-Host ''
  Write-Host ("发布包：{0}  ({1} 字节)" -f $zipPath, (Get-Item -LiteralPath $zipPath).Length)
  Write-Host ("SHA256：{0}" -f $h.Hash)
  Write-Host '提醒：zip 里的 DLL 没有做名称扫描（二进制），如改过出处注释请自行确认。'
}

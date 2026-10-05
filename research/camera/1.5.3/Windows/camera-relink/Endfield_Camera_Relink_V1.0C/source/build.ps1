param(
    [string]$Gpp,          # 可选: 指定 g++.exe 路径
    [string]$OutDir,       # 可选: 产物输出目录(默认 ..\game_mod)
    [switch]$KeepObjects   # 可选: 保留中间 .o 文件
)
# 从源码构建 EndfieldCamLink.dll 与 injector.exe (MinGW-w64 g++, 不需要 CMake)。
# 产物直接输出到 ..\game_mod\ , 构建完即可用 tools\launch_game.ps1 启动。
#
# 何时需要重新构建:
#   - 游戏大版本更新后 (模块靠运行时按名字解析, 一般无需重建; 若失效再重建)
#   - 换了架构/编译器环境
#   - 修改了源码
$ErrorActionPreference = 'Stop'
$srcRoot = $PSScriptRoot                      # ...\source
$pkgRoot = Split-Path -Parent $srcRoot        # ...\EndfieldCameraLink

$src = Join-Path $srcRoot 'src'
$inc = Join-Path $srcRoot 'include'
$mh  = Join-Path $srcRoot 'third_party\minhook'
$out = if ($OutDir) { $OutDir } else { Join-Path $pkgRoot 'game_mod' }
$obj = Join-Path $srcRoot 'obj'

foreach ($p in @($src, $inc, $mh, (Join-Path $srcRoot 'tools\injector\main.cpp'))) {
    if (-not (Test-Path $p)) { throw "源码不完整, 缺少: $p" }
}
New-Item -ItemType Directory -Path $out -Force | Out-Null
New-Item -ItemType Directory -Path $obj -Force | Out-Null

# ---------- 定位 g++ ----------
if (-not $Gpp) {
    $cmd = Get-Command g++ -ErrorAction SilentlyContinue
    if ($cmd) { $Gpp = $cmd.Source }
}
if (-not $Gpp) {
    $cands = @(
        'D:\mingw64\mingw64\bin\g++.exe', 'D:\mingw64\bin\g++.exe',
        'C:\mingw64\mingw64\bin\g++.exe', 'C:\mingw64\bin\g++.exe',
        "$env:LOCALAPPDATA\Programs\WinLibs\mingw64\bin\g++.exe",
        'C:\msys64\mingw64\bin\g++.exe'
    )
    foreach ($d in (Get-PSDrive -PSProvider FileSystem | ForEach-Object { $_.Root })) {
        $cands += (Join-Path $d 'mingw64\mingw64\bin\g++.exe')
    }
    foreach ($c in $cands) { if (Test-Path $c) { $Gpp = $c; break } }
}
if (-not $Gpp -or -not (Test-Path $Gpp)) {
    throw @"
未找到 g++。请任选一种方式安装 MinGW-w64:
  winget install -e --id BrechtSanders.WinLibs.POSIX.UCRT
  (或下载 WinLibs / MSYS2 MinGW-w64, 确保 g++ 在 PATH 中)
装好后重跑本脚本, 或用 -Gpp "C:\path\to\g++.exe" 指定。
"@
}
Write-Host "使用编译器: $Gpp" -ForegroundColor Cyan

# ---------- 1) MinHook (静态链接进模块) ----------
$mhSrcs = @('src\buffer.c','src\trampoline.c','src\hook.c','src\hde\hde32.c','src\hde\hde64.c') |
          ForEach-Object { Join-Path $mh $_ }
$mhObjs = @()
foreach ($s in $mhSrcs) {
    if (-not (Test-Path $s)) { throw "缺少 MinHook 源码: $s" }
    $o = Join-Path $obj ((Split-Path $s -Leaf) -replace '\.c$', '.o')
    & $Gpp -c -O2 "$s" -I (Join-Path $mh 'include') -o $o
    if ($LASTEXITCODE -ne 0) { throw "MinHook 编译失败: $s" }
    $mhObjs += $o
}

# ---------- 2) 模块 DLL ----------
$cppFiles = Get-ChildItem $src -Filter '*.cpp' | ForEach-Object { $_.FullName }
Write-Host ("编译模块 ($($cppFiles.Count) 个源文件)...") -ForegroundColor Cyan
& $Gpp -shared -static -O2 -std=c++17 $cppFiles $mhObjs `
    -I $inc -I (Join-Path $mh 'include') -lws2_32 -static-libgcc -static-libstdc++ `
    -o (Join-Path $out 'EndfieldCamLink.dll')
if ($LASTEXITCODE -ne 0) { throw "模块编译失败" }

# ---------- 3) 注入器 ----------
Write-Host "编译注入器..." -ForegroundColor Cyan
& $Gpp -O2 -std=c++17 -static -static-libgcc -static-libstdc++ `
    (Join-Path $srcRoot 'tools\injector\main.cpp') -o (Join-Path $out 'injector.exe')
if ($LASTEXITCODE -ne 0) { throw "注入器编译失败" }

if (-not $KeepObjects) { Remove-Item $obj -Recurse -Force -ErrorAction SilentlyContinue }

Write-Host ""
Write-Host "构建完成, 产物在 game_mod\ :" -ForegroundColor Green
Get-ChildItem $out -File | Where-Object { $_.Extension -in '.dll','.exe' } |
    Select-Object Name, @{n='KB';e={[math]::Round($_.Length/1KB,1)}}, LastWriteTime | Format-Table -AutoSize
Write-Host "下一步: powershell -ExecutionPolicy Bypass -File tools\launch_game.ps1"

param(
    [string]$Gpp,
    [switch]$NoLaunch
)
# 从源码重建模块, 然后启动游戏并注入(一步到位)。
# 会先结束残留的游戏进程(受 ACE 保护的进程需要管理员权限, 故自动提权)。
$ErrorActionPreference = 'Continue'
$pkgRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)

$principal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
if (-not $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    Write-Host "需要管理员权限(结束游戏进程/注入), 正在请求提权..." -ForegroundColor Yellow
    $a = @('-NoProfile','-ExecutionPolicy','Bypass','-File', "`"$PSCommandPath`"")
    if ($Gpp) { $a += @('-Gpp', "`"$Gpp`"") }
    if ($NoLaunch) { $a += '-NoLaunch' }
    Start-Process powershell.exe -ArgumentList $a -Verb RunAs
    exit 0
}

# 1) 结束残留进程(释放 DLL 占用, 否则链接会失败)
Get-Process Endfield,UnityCrashHandler64,injector -ErrorAction SilentlyContinue |
    ForEach-Object { try { Stop-Process -Id $_.Id -Force -ErrorAction Stop } catch {} }
Start-Sleep -Seconds 5
$left = (Get-Process Endfield -ErrorAction SilentlyContinue | Measure-Object).Count
if ($left -gt 0) {
    Write-Host "仍有 $left 个游戏进程无法结束, 请手工处理后再试。" -ForegroundColor Red
    Read-Host "按回车退出" | Out-Null
    exit 1
}

# 2) 构建
Write-Host "开始构建..." -ForegroundColor Cyan
$buildArgs = @('-NoProfile','-ExecutionPolicy','Bypass','-File', (Join-Path $pkgRoot 'source\build.ps1'))
if ($Gpp) { $buildArgs += @('-Gpp', $Gpp) }
& powershell @buildArgs
if ($LASTEXITCODE -ne 0) {
    Write-Host "构建失败, 不启动游戏。" -ForegroundColor Red
    Read-Host "按回车退出" | Out-Null
    exit 1
}

# 3) 启动并注入
if (-not $NoLaunch) {
    Write-Host ""
    & powershell -NoProfile -ExecutionPolicy Bypass -File (Join-Path $pkgRoot 'tools\launch_game.ps1') -ForceKill -NoPause
}

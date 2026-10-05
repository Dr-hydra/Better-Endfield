param(
    [switch]$Quiet
)
# 解除"来自 Internet"的文件锁定 (Mark of the Web, MOTW)。
#
# 为什么需要:
#   从邮件 / 网盘 / 聊天工具下载的 zip, 解压后每个文件都会带上 Zone.Identifier
#   备用数据流。这会导致:
#     · PowerShell 脚本被安全策略拦下
#     · injector.exe 触发 SmartScreen "Windows 已保护你的电脑"
#   本脚本会把包内所有文件的锁定去掉。只动这一个数据流, 不修改文件内容。
#
# 用法:
#   右键下载的 zip -> 属性 -> 勾选"解除锁定" -> 再解压   (推荐, 一次性)
#   或者解压后运行: powershell -ExecutionPolicy Bypass -File install\unblock_files.ps1
$ErrorActionPreference = 'Continue'
$pkgRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)

$exts = @('.dll','.exe','.ps1','.cmd','.zip','.md','.txt','.ini','.py','.h','.cpp','.c','.def','.json')
$locked = @()
$total = 0
foreach ($f in (Get-ChildItem $pkgRoot -Recurse -File -ErrorAction SilentlyContinue)) {
    $total++
    try {
        # 只有存在 Zone.Identifier 备用流才算被锁定
        $streams = Get-Item -LiteralPath $f.FullName -Stream Zone.Identifier -ErrorAction SilentlyContinue
        if ($streams) {
            Unblock-File -LiteralPath $f.FullName -ErrorAction Stop
            $locked += $f.FullName.Replace($pkgRoot + '\', '')
        }
    } catch { }
}

if (-not $Quiet) {
    if ($locked.Count -gt 0) {
        Write-Host ("已解除锁定 {0} 个文件 (共扫描 {1} 个):" -f $locked.Count, $total) -ForegroundColor Green
        $locked | Select-Object -First 20 | ForEach-Object { Write-Host ("  " + $_) -ForegroundColor DarkGray }
        if ($locked.Count -gt 20) { Write-Host ("  ...(其余 " + ($locked.Count - 20) + " 个)") -ForegroundColor DarkGray }
    } else {
        Write-Host ("未发现被锁定的文件 (共扫描 {0} 个), 无需处理。" -f $total) -ForegroundColor Green
    }
}

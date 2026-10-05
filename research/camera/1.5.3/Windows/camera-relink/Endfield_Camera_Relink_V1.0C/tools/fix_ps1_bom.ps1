param(
    [switch]$Quiet
)
# 修复并校验全包 .ps1 的 UTF-8 BOM。
#
# 为什么必须有这个工具:
#   PowerShell 5.1 读取**无 BOM** 的 .ps1 时按系统 ANSI 代码页(简中=GBK)解码 ——
#   中文变乱码, 某些字节组合还会吞掉引号/括号, 整脚本解析失败。表现为运行 .cmd 时:
#       "Missing closing ')' in expression." + 界面中文乱码
#   这个坑在本项目已复现多次: 任何编辑器/工具(含 AI 助手的文件编辑接口)重写 .ps1 时
#   都可能把 BOM 丢掉。**每次改完 .ps1 都跑一遍本脚本**。
#
# 用法:
#   powershell -ExecutionPolicy Bypass -File tools\fix_ps1_bom.ps1
#   powershell -ExecutionPolicy Bypass -File tools\fix_ps1_bom.ps1 -Quiet   # 只在有问题时输出
$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$utf8Bom = New-Object System.Text.UTF8Encoding($true)

$fixed = @()
$bad = @()
$files = Get-ChildItem $root -Recurse -Filter *.ps1 -ErrorAction SilentlyContinue |
    Where-Object { $_.FullName -notmatch '\\build_probe\\|\\obj\\' }

foreach ($f in $files) {
    $bytes = [System.IO.File]::ReadAllBytes($f.FullName)
    $hasBom = ($bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF)
    if (-not $hasBom) {
        $text = [System.IO.File]::ReadAllText($f.FullName, [System.Text.Encoding]::UTF8)
        [System.IO.File]::WriteAllText($f.FullName, $text, $utf8Bom)
        $fixed += $f.FullName.Substring($root.Length + 1)
    }
    # 用 PS 自带解析器复核(与运行时同一套语法分析)
    $errs = $null
    [void][System.Management.Automation.Language.Parser]::ParseFile($f.FullName, [ref]$null, [ref]$errs)
    if ($errs -and $errs.Count -gt 0) {
        $bad += ("{0}: {1}" -f $f.FullName.Substring($root.Length + 1), $errs[0].Message)
    }
}

if ($fixed.Count -gt 0) {
    Write-Host ("[已补回 BOM] " + ($fixed -join ', ')) -ForegroundColor Yellow
} elseif (-not $Quiet) {
    Write-Host "BOM 全部齐备($($files.Count) 个 .ps1)" -ForegroundColor Green
}
if ($bad.Count -gt 0) {
    Write-Host "[解析失败]" -ForegroundColor Red
    $bad | ForEach-Object { Write-Host ("  " + $_) -ForegroundColor Red }
    exit 1
}
if (-not $Quiet) { Write-Host "语法解析: 0 错误" -ForegroundColor Green }
exit 0

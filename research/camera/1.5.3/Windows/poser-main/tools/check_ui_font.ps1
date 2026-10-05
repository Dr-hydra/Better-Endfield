<#
  UI 字体自检。

  游戏内面板用的字体只含 ImGui 的「常用简体字」表（2500 字 + 基本符号），
  再加上 gui_overlay.h 里 kExtraUiChars 补的那几个字。界面文案里出现表外的汉字时，
  面板上会直接渲染成方框 —— 肉眼不一定马上发现，这个脚本用来提前抓出来。

  用法：
    powershell -ExecutionPolicy Bypass -File tools\check_ui_font.ps1

  退出码 0 = 全部覆盖；1 = 有缺字（下面会列出来，把它们加进 gui_overlay.h 的
  kExtraUiChars 即可）。
#>
[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $PSScriptRoot

# ---- 1) 解析 ImGui 的常用字表 ----
$draw = Join-Path $root 'deps\imgui\imgui_draw.cpp'
$lines = Get-Content -LiteralPath $draw
$start = ($lines | Select-String -Pattern 'ImFontAtlas::GetGlyphRangesChineseSimplifiedCommon' |
          Select-Object -First 1).LineNumber
if (-not $start) { throw "找不到 GetGlyphRangesChineseSimplifiedCommon（$draw）" }
$seg = $lines[($start - 1)..($start + 90)]

$bi = ($seg | Select-String -Pattern 'base_ranges\[\]' | Select-Object -First 1).LineNumber
$blk = @(); $i = $bi
while ($i -lt $seg.Count) { $blk += $seg[$i]; if ($seg[$i] -match '^\s*\};') { break }; $i++ }
$base = @([regex]::Matches(($blk -join ' '), '0x[0-9A-Fa-f]{4}') |
          ForEach-Object { [Convert]::ToInt32($_.Value, 16) })

$oi = ($seg | Select-String -Pattern 'accumulative_offsets_from_0x4E00\[\]' |
       Select-Object -First 1).LineNumber
$oblk = @(); $i = $oi
while ($i -lt $seg.Count) { $oblk += $seg[$i]; if ($seg[$i] -match '^\s*\};') { break }; $i++ }
$offs = @([regex]::Matches(($oblk -join ' '), '(?<![\w])\d+') | ForEach-Object { [int]$_.Value })

$covered = New-Object 'System.Collections.Generic.HashSet[int]'
for ($k = 0; $k + 1 -lt $base.Count; $k += 2) {
  for ($c = $base[$k]; $c -le $base[$k + 1]; $c++) { [void]$covered.Add($c) }
}
# 每个偏移是"相对上一个码点的单字增量"（见 imgui_draw.cpp 的 UnpackAccumulativeOffsetsIntoRanges）
$c = 0x4E00
foreach ($o in $offs) { $c += $o; [void]$covered.Add($c) }

# ---- 2) 加上 gui_overlay.h 里手工补的字 ----
$overlay = Get-Content -LiteralPath (Join-Path $root 'src\core\gui_overlay.h') -Raw -Encoding UTF8
$m = [regex]::Match($overlay, 'kExtraUiChars\s*=\s*u8"([^"]*)"')
if (-not $m.Success) { throw '找不到 src\core\gui_overlay.h 里的 kExtraUiChars' }
$extra = $m.Groups[1].Value
foreach ($ch in $extra.ToCharArray()) { [void]$covered.Add([int]$ch) }
Write-Host ("字体表：{0} 个码位（含补字 {1}）" -f $covered.Count, $extra)

# ---- 3) 扫界面文案 ----
$files = Get-ChildItem -Recurse -File -Include *.h, *.cpp (Join-Path $root 'src')
$bad = @()
foreach ($f in $files) {
  $text = Get-Content -LiteralPath $f.FullName -Raw -Encoding UTF8
  foreach ($lit in [regex]::Matches($text, 'u8"((?:[^"\\]|\\.)*)"')) {
    $s = $lit.Groups[1].Value
    # \uXXXX 转义形式
    foreach ($e in [regex]::Matches($s, '\\u([0-9A-Fa-f]{4})')) {
      $ch = [char][Convert]::ToInt32($e.Groups[1].Value, 16)
      if (-not $covered.Contains([int]$ch)) { $bad += [pscustomobject]@{ File = $f.Name; Char = $ch } }
    }
    # 直接写中文的形式（本项目的界面串两种写法都有）
    foreach ($ch in $s.ToCharArray()) {
      $code = [int]$ch
      if ($code -ge 0x2E80 -and $code -le 0x9FFF -and -not $covered.Contains($code)) {
        $bad += [pscustomobject]@{ File = $f.Name; Char = $ch }
      }
    }
  }
}

if (-not $bad) {
  Write-Host 'OK：界面文案用到的字全部在字体表内。' -ForegroundColor Green
  exit 0
}

Write-Host '以下字不在字体表内，会显示成方框：' -ForegroundColor Red
$bad | Group-Object File | ForEach-Object {
  $chars = ($_.Group.Char | Sort-Object -Unique) -join ''
  Write-Host ("  {0,-24} {1}" -f $_.Name, $chars)
}
Write-Host ("`n把它们补进 src\core\gui_overlay.h 的 kExtraUiChars 即可（当前：{0}）。" -f $extra)
exit 1

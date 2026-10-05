param(
    [string]$OutFile   # 默认写到 <包根>\assets\endfield_camera_link.ico
)
# 生成一张 256×256 的相机图标并封装成 .ico（PNG 载荷，Vista+ 支持）。
# 纯本地绘制，失败不致命 —— 调用方会退回系统默认图标。
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Drawing

$pkgRoot = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
if (-not $OutFile) {
    $dir = Join-Path $pkgRoot 'assets'
    New-Item -ItemType Directory -Path $dir -Force | Out-Null
    $OutFile = Join-Path $dir 'endfield_camera_link.ico'
}

function New-RoundRect([System.Drawing.Drawing2D.GraphicsPath]$p,[single]$x,[single]$y,[single]$w,[single]$h,[single]$r) {
    $d = $r * 2
    $p.AddArc($x, $y, $d, $d, 180, 90)
    $p.AddArc($x + $w - $d, $y, $d, $d, 270, 90)
    $p.AddArc($x + $w - $d, $y + $h - $d, $d, $d, 0, 90)
    $p.AddArc($x, $y + $h - $d, $d, $d, 90, 90)
    $p.CloseFigure()
}

$S = 256
$bmp = New-Object System.Drawing.Bitmap($S, $S, [System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
$g = [System.Drawing.Graphics]::FromImage($bmp)
$g.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
$g.Clear([System.Drawing.Color]::Transparent)

# 圆角底板(竖向渐变)
$bgPath = New-Object System.Drawing.Drawing2D.GraphicsPath
New-RoundRect $bgPath 8 8 ($S-16) ($S-16) 44
$grad = New-Object System.Drawing.Drawing2D.LinearGradientBrush(
    (New-Object System.Drawing.Point(0,0)), (New-Object System.Drawing.Point(0,$S)),
    [System.Drawing.Color]::FromArgb(255,32,50,68), [System.Drawing.Color]::FromArgb(255,14,22,32))
$g.FillPath($grad, $bgPath)

# 机身
$bodyBrush = New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::FromArgb(255,232,238,244))
$bodyPath = New-Object System.Drawing.Drawing2D.GraphicsPath
New-RoundRect $bodyPath 34 88 188 116 22
$g.FillPath($bodyBrush, $bodyPath)

# 取景器凸起
$g.FillRectangle($bodyBrush, 96, 66, 58, 26)

# 镜头
$outer = New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::FromArgb(255,42,58,76))
$g.FillEllipse($outer, 128-46, 146-46, 92, 92)
$ring = New-Object System.Drawing.Pen ([System.Drawing.Color]::FromArgb(255,90,169,230)), 8
$g.DrawEllipse($ring, 128-38, 146-38, 76, 76)
$glow = New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::FromArgb(255,120,196,255))
$g.FillEllipse($glow, 128-16, 146-16, 32, 32)

# 录制指示灯
$red = New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::FromArgb(255,229,83,75))
$g.FillEllipse($red, 182, 104, 22, 22)

$g.Dispose()

# 存为 PNG 内存流
$ms = New-Object System.IO.MemoryStream
$bmp.Save($ms, [System.Drawing.Imaging.ImageFormat]::Png)
$png = $ms.ToArray()
$ms.Dispose(); $bmp.Dispose()

# 封装 ICO (单条目, PNG 载荷; 宽高字段 0 表示 256)
$out = New-Object System.IO.MemoryStream
$bw = New-Object System.IO.BinaryWriter($out)
$bw.Write([UInt16]0); $bw.Write([UInt16]1); $bw.Write([UInt16]1)   # reserved, type=icon, count
$bw.Write([Byte]0); $bw.Write([Byte]0)                            # width=256, height=256
$bw.Write([Byte]0); $bw.Write([Byte]0)                            # 调色板数, 保留
$bw.Write([UInt16]1); $bw.Write([UInt16]32)                       # planes, bpp
$bw.Write([UInt32]$png.Length)
$bw.Write([UInt32]22)                                             # 数据偏移 = 6 + 16
$bw.Write($png)
$bw.Flush()
[System.IO.File]::WriteAllBytes($OutFile, $out.ToArray())
$bw.Dispose(); $out.Dispose()

Write-Output ("图标已生成: {0} ({1} KB)" -f $OutFile, [math]::Round((Get-Item $OutFile).Length/1KB,1))

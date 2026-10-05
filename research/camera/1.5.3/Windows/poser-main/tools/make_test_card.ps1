Add-Type -AssemblyName System.Drawing
$outDir = Join-Path $PSScriptRoot '..\debug'
New-Item -ItemType Directory -Force -Path $outDir | Out-Null
$path = Join-Path $outDir 'test_card.png'
$bmp = New-Object System.Drawing.Bitmap(400, 200)
$g = [System.Drawing.Graphics]::FromImage($bmp)
$g.Clear([System.Drawing.Color]::DarkSlateBlue)
$font = New-Object System.Drawing.Font('Arial', 26, [System.Drawing.FontStyle]::Bold)
$g.DrawString('Endfield Poser', $font, [System.Drawing.Brushes]::Orange, 30, 60)
$g.DrawString('GLM-4V vision test', $font, [System.Drawing.Brushes]::White, 30, 110)
$g.Dispose()
$bmp.Save($path, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp.Dispose()
Write-Host "saved $path"

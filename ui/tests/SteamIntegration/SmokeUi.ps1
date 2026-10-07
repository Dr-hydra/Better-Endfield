[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$Executable,
    [Parameter(Mandatory)][string]$Metadata,
    [Parameter(Mandatory)][string]$WorkDirectory
)
$ErrorActionPreference = 'Stop'
Write-Output 'Steam preview: loading automation support.'
Add-Type -AssemblyName UIAutomationClient
Add-Type -AssemblyName UIAutomationTypes
Add-Type -AssemblyName System.Drawing
Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;
public static class SteamPreviewCapture {
    [StructLayout(LayoutKind.Sequential)] public struct Rect { public int Left, Top, Right, Bottom; }
    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr handle, out Rect rect);
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr handle);
    [DllImport("user32.dll")] public static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool repaint);
}
'@
Write-Output 'Steam preview: automation support loaded.'
$exe = (Resolve-Path -LiteralPath $Executable).Path
$metadataFile = (Resolve-Path -LiteralPath $Metadata).Path
$work = [IO.Path]::GetFullPath($WorkDirectory)
New-Item -ItemType Directory -Path $work -Force | Out-Null

function Find-Control([string[]]$Names) {
    foreach ($name in $Names) {
        $condition = New-Object Windows.Automation.PropertyCondition([Windows.Automation.AutomationElement]::NameProperty, $name)
        $element = $script:root.FindFirst([Windows.Automation.TreeScope]::Descendants, $condition)
        if ($null -ne $element) { return $element }
    }
    return $null
}
function Wait-Control([string[]]$Names) {
    $deadline = [DateTime]::UtcNow.AddSeconds(15)
    do {
        $element = Find-Control $Names
        if ($null -ne $element) { return $element }
        if ($script:process.HasExited) { throw 'Preview process exited.' }
        Start-Sleep -Milliseconds 100
    } while ([DateTime]::UtcNow -lt $deadline)
    throw "Missing UI control: $($Names -join ' / ')"
}
function Capture-Preview([string]$Name) {
    $null = [SteamPreviewCapture]::SetForegroundWindow($script:process.MainWindowHandle)
    Start-Sleep -Milliseconds 250
    $rect = New-Object SteamPreviewCapture+Rect
    if (-not [SteamPreviewCapture]::GetWindowRect($script:process.MainWindowHandle, [ref]$rect)) { throw 'Missing preview bounds.' }
    $bitmap = New-Object Drawing.Bitmap(($rect.Right - $rect.Left), ($rect.Bottom - $rect.Top))
    $graphics = [Drawing.Graphics]::FromImage($bitmap)
    try { $graphics.CopyFromScreen($rect.Left,$rect.Top,0,0,$bitmap.Size); $bitmap.Save((Join-Path $work $Name),[Drawing.Imaging.ImageFormat]::Png) }
    finally { $graphics.Dispose(); $bitmap.Dispose() }
}

$process = $null
try {
    Write-Output 'Steam preview: starting isolated preview window.'
    $process = Start-Process -FilePath $exe -ArgumentList ('--steam-setup-preview "' + $metadataFile + '"') -WorkingDirectory $work -WindowStyle Hidden -PassThru
    $deadline = [DateTime]::UtcNow.AddSeconds(20)
    do {
        $process.Refresh()
        if ($process.HasExited) { throw 'Preview process exited before creating a window.' }
        if ($process.MainWindowHandle -ne [IntPtr]::Zero) { break }
        Start-Sleep -Milliseconds 100
    } while ([DateTime]::UtcNow -lt $deadline)
    $root = [Windows.Automation.AutomationElement]::FromHandle($process.MainWindowHandle)
    $preview = Wait-Control @('预览 ACF', 'Preview ACF')
    $deadline = [DateTime]::UtcNow.AddSeconds(10)
    while (-not $preview.Current.IsEnabled) {
        if ([DateTime]::UtcNow -gt $deadline) { throw 'Metadata did not enable ACF preview.' }
        Start-Sleep -Milliseconds 100
    }
    foreach ($names in @(
        @('应用 / 更新 ACF','Apply / update ACF'),
        @('移除 BE 的 ACF','Remove BE''s ACF'),
        @('同步 Windows 管理员设置','Sync Windows elevation setting'),
        @('恢复管理员设置','Restore elevation setting'),
        @('从 Steam 启动国服','Launch CN from Steam')
    )) {
        $button = Wait-Control $names
        if ($button.Current.IsEnabled) { throw "Preview permits mutation: $($button.Current.Name)" }
    }
    Capture-Preview 'steam-preview-normal.png'
    $preview.GetCurrentPattern([Windows.Automation.InvokePattern]::Pattern).Invoke()
    $null = Wait-Control @('ACF 预览（尚未写入）', 'ACF preview (not written)')
    $condition = New-Object Windows.Automation.PropertyCondition([Windows.Automation.AutomationElement]::ControlTypeProperty, [Windows.Automation.ControlType]::Edit)
    $deadline = [DateTime]::UtcNow.AddSeconds(10)
    do {
        $texts = $root.FindAll([Windows.Automation.TreeScope]::Descendants, $condition) | ForEach-Object {
            $_.GetCurrentPattern([Windows.Automation.ValuePattern]::Pattern).Current.Value
        }
        $acf = $texts | Where-Object { $_ -match '"AppState"' } | Select-Object -First 1
        if ($acf) { break }
        Start-Sleep -Milliseconds 100
    } while ([DateTime]::UtcNow -lt $deadline)
    if (-not $acf -or $acf -notmatch '4732690' -or $acf -notmatch 'InstalledDepots') { throw 'Incorrect ACF preview.' }
    $acf | Set-Content -LiteralPath (Join-Path $work 'preview.acf') -Encoding UTF8
    Capture-Preview 'steam-preview-acf.png'
    $close = Wait-Control @('关闭', 'Close')
    $close.GetCurrentPattern([Windows.Automation.InvokePattern]::Pattern).Invoke()
    $null = [SteamPreviewCapture]::MoveWindow($process.MainWindowHandle, 30, 30, 760, 700, $true)
    Start-Sleep -Milliseconds 300
    Capture-Preview 'steam-preview-small.png'
    [ordered]@{ success = $true; acf_preview = $true; mutating_buttons_disabled = $true; window_sizes = @('initial','760x700'); metadata = $metadataFile } |
        ConvertTo-Json | Set-Content -LiteralPath (Join-Path $work 'summary.json') -Encoding UTF8
    Write-Output ('Steam UI preview passed: ' + (Join-Path $work 'summary.json'))
}
catch {
    if ($null -ne $process -and -not $process.HasExited) { Capture-Preview 'steam-preview-failure.png' }
    if ($null -ne $root) {
        $root.FindAll([Windows.Automation.TreeScope]::Descendants, [Windows.Automation.Condition]::TrueCondition) |
            ForEach-Object { $_.Current.Name } | Set-Content -LiteralPath (Join-Path $work 'controls.txt') -Encoding UTF8
    }
    throw
}
finally {
    if ($null -ne $process -and -not $process.HasExited) {
        $null = $process.CloseMainWindow()
        if (-not $process.WaitForExit(5000)) { $process.Kill() }
    }
}

[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$ToolDirectory,
    [Parameter(Mandatory)][string]$WorkDirectory
)
$ErrorActionPreference = 'Stop'
# Run using Windows PowerShell: its desktop framework supplies UI Automation.
Add-Type -AssemblyName UIAutomationClient
Add-Type -AssemblyName UIAutomationTypes

$toolRoot = (Resolve-Path -LiteralPath $ToolDirectory).Path
$gui = Join-Path $toolRoot 'BetterEndfieldNext.BemTools.exe'
$cli = Join-Path $toolRoot 'BetterEndfieldNext.BemConverter.exe'
foreach ($file in @($gui, $cli, (Join-Path $toolRoot 'docs/BEM_CREATOR_GUIDE.md'))) {
    if (-not (Test-Path -LiteralPath $file -PathType Leaf)) { throw "Missing toolchain file: $file" }
}
$work = Join-Path ([IO.Path]::GetFullPath($WorkDirectory)) ('GUI 中文测试 ' + [Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $work -Force | Out-Null
Copy-Item -LiteralPath (Join-Path $toolRoot 'examples/multi-resource/project') -Destination (Join-Path $work 'project') -Recurse
$project = Join-Path $work 'project/export.bemproj.json'
$output = Join-Path $work 'project/dist/synthetic.bem'
if (Test-Path -LiteralPath $output) { throw 'The portable example must not contain a previously built package.' }

function Find-Control([string[]]$Names) {
    foreach ($name in $Names) {
        $condition = New-Object Windows.Automation.PropertyCondition([Windows.Automation.AutomationElement]::NameProperty, $name)
        $element = $script:root.FindFirst([Windows.Automation.TreeScope]::Descendants, $condition)
        if ($null -ne $element) { return $element }
    }
    return $null
}
function Wait-Control([string[]]$Names) {
    $deadline = [DateTime]::UtcNow.AddSeconds(20)
    do {
        $element = Find-Control $Names
        if ($null -ne $element) { return $element }
        if ($script:process.HasExited) { throw "Creator GUI exited: $($script:process.ExitCode)" }
        Start-Sleep -Milliseconds 100
    } while ([DateTime]::UtcNow -lt $deadline)
    throw "GUI control did not appear: $($Names -join ' / ')"
}
function Invoke-Control([string[]]$Names) {
    $element = Wait-Control $Names
    $pattern = $element.GetCurrentPattern([Windows.Automation.InvokePattern]::Pattern)
    $pattern.Invoke()
}

$process = $null
try {
    # This working directory deliberately differs from the tool's directory.
    $process = Start-Process -FilePath $gui -ArgumentList ('"' + $project + '"') -WorkingDirectory $work -WindowStyle Hidden -PassThru
    $deadline = [DateTime]::UtcNow.AddSeconds(20)
    do {
        $process.Refresh()
        if ($process.HasExited) { throw "Creator GUI exited: $($process.ExitCode)" }
        if ($process.MainWindowHandle -ne [IntPtr]::Zero) { break }
        Start-Sleep -Milliseconds 100
    } while ([DateTime]::UtcNow -lt $deadline)
    if ($process.MainWindowHandle -eq [IntPtr]::Zero) { throw 'Creator GUI did not create a window.' }
    $root = [Windows.Automation.AutomationElement]::FromHandle($process.MainWindowHandle)
    $null = Wait-Control @('工程已打开', 'Project opened')
    Invoke-Control @('保存参数并导出 BEM', 'Save settings and export BEM')
    $deadline = [DateTime]::UtcNow.AddSeconds(30)
    while (-not (Test-Path -LiteralPath $output)) {
        if ($process.HasExited) { throw "Creator GUI exited during export: $($process.ExitCode)" }
        if ([DateTime]::UtcNow -ge $deadline) { throw 'GUI export did not create the BEM package.' }
        Start-Sleep -Milliseconds 100
    }
    $validationText = & $cli validate $output --resource weapon --platform android-arm64
    if ($LASTEXITCODE -ne 0) { throw 'The GUI-exported BEM failed CLI resource/platform validation.' }
    $validation = ($validationText -join "`n") | ConvertFrom-Json
    if (-not $validation.success -or $validation.format_version -ne '1.4') { throw 'Unexpected GUI export validation report.' }

    Invoke-Control @('帮助与支持范围', 'Help and supported features')
    $null = Wait-Control @('创作者帮助', 'Creator help')
    Invoke-Control @('返回任务', 'Back to task')

    $version = (& $cli --version) -join "`n"
    if ($LASTEXITCODE -ne 0) { throw 'The frozen CLI version check failed.' }
    $guiVersion = [Diagnostics.FileVersionInfo]::GetVersionInfo($gui).ProductVersion
    if (-not $version.Contains('BEM Tools ' + $guiVersion)) { throw "GUI/CLI version mismatch: $guiVersion / $version" }
    $summary = [ordered]@{
        success = $true
        gui_version = $guiVersion
        cli_version = $version
        opened_export_project = $true
        gui_exported_bem14 = $true
        android_weapon_validation = $true
        bundled_help_opened = $true
        launched_from_other_directory = $true
        unicode_paths = $true
        package = $output
    }
    $summary | ConvertTo-Json | Set-Content -LiteralPath (Join-Path $work 'summary.json') -Encoding UTF8
    Write-Output ('Portable GUI smoke passed: ' + (Join-Path $work 'summary.json'))
}
catch {
    if ($null -ne $root) {
        try {
            $root.FindAll([Windows.Automation.TreeScope]::Descendants, [Windows.Automation.Condition]::TrueCondition) |
                ForEach-Object { $_.Current.Name } | Set-Content -LiteralPath (Join-Path $work 'gui-controls.txt') -Encoding UTF8
        } catch { }
    }
    throw
}
finally {
    if ($null -ne $process -and -not $process.HasExited) {
        $null = $process.CloseMainWindow()
        if (-not $process.WaitForExit(5000)) { $process.Kill() }
    }
}

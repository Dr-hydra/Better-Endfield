[CmdletBinding()]
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release",

    [string]$Version = "2.3.1"
)

$ErrorActionPreference = "Stop"
$repoRoot = [System.IO.Path]::GetFullPath((Split-Path -Parent $PSScriptRoot))
$artifactsRoot = Join-Path $repoRoot "artifacts"
$stagingDir = Join-Path $artifactsRoot ".installer-staging"
$outputDir = Join-Path $artifactsRoot "installer"
$installerScript = Join-Path $repoRoot "installer\BetterEndfield.iss"

function Assert-WorkspaceChildPath {
    param([Parameter(Mandatory)][string]$Path)

    $fullPath = [System.IO.Path]::GetFullPath($Path)
    $workspacePrefix = $repoRoot.TrimEnd('\') + '\'
    if (-not $fullPath.StartsWith(
        $workspacePrefix,
        [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "Refusing to modify a path outside the workspace: $fullPath"
    }
    return $fullPath
}

function Reset-Directory {
    param([Parameter(Mandatory)][string]$Path)

    $safePath = Assert-WorkspaceChildPath $Path
    if (Test-Path -LiteralPath $safePath) {
        Remove-Item -LiteralPath $safePath -Recurse -Force
    }
    New-Item -ItemType Directory -Path $safePath -Force | Out-Null
    return $safePath
}

$innoCandidates = @(
    (Join-Path $env:LOCALAPPDATA "Programs\Inno Setup 6\ISCC.exe"),
    (Join-Path ${env:ProgramFiles(x86)} "Inno Setup 6\ISCC.exe"),
    (Join-Path $env:ProgramFiles "Inno Setup 6\ISCC.exe")
)
$iscc = $innoCandidates | Where-Object { Test-Path -LiteralPath $_ } |
    Select-Object -First 1
if (-not $iscc) {
    throw "ISCC.exe was not found. Install Inno Setup 6 for the current user or system."
}

$stagingDir = Reset-Directory $stagingDir
$outputDir = Reset-Directory $outputDir

try {
    & (Join-Path $PSScriptRoot "BuildBetterEndfield.ps1") `
        -Configuration $Configuration `
        -PublishDir $stagingDir
    if ($LASTEXITCODE -ne 0) {
        throw "Application build failed with exit code $LASTEXITCODE."
    }

    Get-ChildItem -LiteralPath $stagingDir -Recurse -File -Include *.pdb,*.log |
        Remove-Item -Force

    $includedCultures = @('en-US', 'zh-CN', 'zh-TW')
    foreach ($directory in Get-ChildItem -LiteralPath $stagingDir -Directory) {
        try {
            [void][System.Globalization.CultureInfo]::GetCultureInfo($directory.Name)
        }
        catch [System.Globalization.CultureNotFoundException] {
            continue
        }

        if ($directory.Name -notin $includedCultures) {
            $safeCultureDirectory = Assert-WorkspaceChildPath $directory.FullName
            Remove-Item -LiteralPath $safeCultureDirectory -Recurse -Force
        }
    }

    & $iscc `
        "/DStageDir=$stagingDir" `
        "/DOutputDir=$outputDir" `
        "/DAppVersion=$Version" `
        $installerScript
    if ($LASTEXITCODE -ne 0) {
        throw "Inno Setup compilation failed with exit code $LASTEXITCODE."
    }
}
finally {
    if (Test-Path -LiteralPath $stagingDir) {
        $safeStaging = Assert-WorkspaceChildPath $stagingDir
        Remove-Item -LiteralPath $safeStaging -Recurse -Force
    }
}

$installers = Get-ChildItem -LiteralPath $outputDir -File
if ($installers.Count -ne 1) {
    throw "Expected one installer output, found $($installers.Count)."
}

Write-Host ""
Write-Host "Installer complete: $($installers[0].FullName)"

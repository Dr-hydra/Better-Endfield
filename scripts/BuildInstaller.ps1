[CmdletBinding()]
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release",

    [string]$Version = "",

    [string]$PublishDir = "",

    [string]$OutputDir = "",

    [string]$WorkspaceConfig = ""
)

$ErrorActionPreference = "Stop"
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$ws = Get-BEWorkspace -Config $WorkspaceConfig
Set-BEWorkspaceEnvironment $ws
$repoRoot = $ws.repo_root

# Directory.Build.props is the single source of truth: it is what the assembly
# — and therefore the About page — reports at runtime. Deriving the installer
# version from it keeps the number shown in Apps & Features, the Setup file
# name and the About page from ever drifting apart.
$propsPath = Join-Path $repoRoot "Directory.Build.props"
$assemblyVersion = ([xml](Get-Content -LiteralPath $propsPath -Raw)).
    SelectSingleNode("//Version").InnerText.Trim()
if (-not $assemblyVersion) {
    throw "Directory.Build.props does not define <Version>."
}
if (-not $Version) {
    $Version = $assemblyVersion
}
elseif ($Version -ne $assemblyVersion) {
    throw ("-Version $Version does not match <Version>$assemblyVersion</Version> " +
        "in Directory.Build.props. The installer and the About page would " +
        "report different versions; update Directory.Build.props instead.")
}
$publishDir = if ([string]::IsNullOrWhiteSpace($PublishDir)) {
    Join-Path $ws.paths.build "windows\win-x64\$Configuration\publish"
} else {
    [System.IO.Path]::GetFullPath($PublishDir)
}
$outputDir = if ([string]::IsNullOrWhiteSpace($OutputDir)) {
    Get-BEReleaseDirectory -Workspace $ws -Version $Version
} else {
    [System.IO.Path]::GetFullPath($OutputDir)
}
$stagingDir = Join-Path $ws.paths.temp ("installer-staging\" + [Guid]::NewGuid().ToString('N'))
$installerScript = Join-Path $repoRoot "installer\BetterEndfield.iss"

function Assert-StagingChildPath {
    param([Parameter(Mandatory)][string]$Path)

    $fullPath = [System.IO.Path]::GetFullPath($Path)
    $stagingIdentity = [System.IO.Path]::GetFullPath($stagingDir).TrimEnd('\')
    if ($fullPath.TrimEnd('\') -ne $stagingIdentity -and -not $fullPath.StartsWith(
        $stagingIdentity + '\',
        [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "Refusing to remove a path outside installer staging: $fullPath"
    }
    $ancestor = $fullPath
    while ($ancestor) {
        if (Test-Path -LiteralPath $ancestor) {
            $entry = Get-Item -LiteralPath $ancestor -Force
            if ($entry.Attributes -band [System.IO.FileAttributes]::ReparsePoint) {
                throw "Installer staging traverses a reparse point: $ancestor"
            }
        }
        $ancestor = Split-Path -Parent $ancestor
    }
    return $fullPath
}

$iscc = $ws.tools.iscc
if ($iscc) {
    if (-not (Get-Command $iscc -ErrorAction SilentlyContinue)) {
        throw "Configured ISCC command was not found: $iscc"
    }
} else {
    $innoCommand = Get-Command ISCC.exe -ErrorAction SilentlyContinue
    if ($innoCommand) { $iscc = $innoCommand.Source }
    $innoCandidates = @()
    if ($env:LOCALAPPDATA) { $innoCandidates += Join-Path $env:LOCALAPPDATA 'Programs\Inno Setup 6\ISCC.exe' }
    if (${env:ProgramFiles(x86)}) { $innoCandidates += Join-Path ${env:ProgramFiles(x86)} 'Inno Setup 6\ISCC.exe' }
    if ($env:ProgramFiles) { $innoCandidates += Join-Path $env:ProgramFiles 'Inno Setup 6\ISCC.exe' }
    if (-not $iscc) {
        $iscc = $innoCandidates | Where-Object { Test-Path -LiteralPath $_ -PathType Leaf } |
            Select-Object -First 1
    }
}
if (-not $iscc) {
    throw "ISCC.exe was not found. Install Inno Setup 6 for the current user or system."
}

if (-not (Test-Path -LiteralPath (Join-Path $publishDir 'BetterEndfield.exe') -PathType Leaf)) {
    throw "Publish output is missing: $publishDir. Run BuildBetterEndfield.ps1 -Configuration $Configuration first."
}
# Stage the existing publish output without changing it or clearing release history.
[void](Assert-StagingChildPath $stagingDir)
New-Item -ItemType Directory -Path $stagingDir -Force | Out-Null
New-Item -ItemType Directory -Path $outputDir -Force | Out-Null

try {
    foreach ($entry in Get-ChildItem -LiteralPath $publishDir -Force) {
        Copy-Item -LiteralPath $entry.FullName -Destination $stagingDir -Recurse -Force
    }

    # Windows PowerShell 5.1 ignores -Include together with -LiteralPath and
    # -Recurse and would match every staged file, so filter explicitly.
    Get-ChildItem -LiteralPath $stagingDir -Recurse -File |
        Where-Object { $_.Extension -in @(".pdb", ".log") } |
        Remove-Item -Force

    $stagedExecutable = Join-Path $stagingDir "BetterEndfield.exe"
    if (-not (Test-Path -LiteralPath $stagedExecutable -PathType Leaf)) {
        throw "Installer staging lost BetterEndfield.exe before packaging."
    }

    $includedCultures = @('en-US', 'zh-CN', 'zh-TW')
    foreach ($directory in Get-ChildItem -LiteralPath $stagingDir -Directory) {
        try {
            [void][System.Globalization.CultureInfo]::GetCultureInfo($directory.Name)
        }
        catch [System.Globalization.CultureNotFoundException] {
            continue
        }

        if ($directory.Name -notin $includedCultures) {
            $safeCultureDirectory = Assert-StagingChildPath $directory.FullName
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
        $safeStaging = Assert-StagingChildPath $stagingDir
        Remove-Item -LiteralPath $safeStaging -Recurse -Force
    }
}

$installer = Join-Path $outputDir "BetterEndfield-$Version-Setup.exe"
if (-not (Test-Path -LiteralPath $installer -PathType Leaf)) {
    throw "Installer output is missing: $installer"
}

Write-Host ""
Write-Host "Installer complete: $installer"

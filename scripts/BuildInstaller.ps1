[CmdletBinding()]
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release",

    [string]$Version = ""
)

$ErrorActionPreference = "Stop"
$repoRoot = [System.IO.Path]::GetFullPath((Split-Path -Parent $PSScriptRoot))

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

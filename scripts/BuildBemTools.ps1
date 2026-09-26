[CmdletBinding()]
param([string]$Destination = "")
$ErrorActionPreference = "Stop"
$repo = Split-Path -Parent $PSScriptRoot
& (Join-Path $PSScriptRoot 'PrepareBemArchiveBackend.ps1')
if ([string]::IsNullOrWhiteSpace($Destination)) {
    $Destination = Join-Path $repo "artifacts\bem-tools"
}
# Creator tools are self-contained at runtime. Build dependencies are explicit.
& python -c "import PyInstaller, zstandard"
if ($LASTEXITCODE -ne 0) { throw "Install tools/CustomModel/requirements-build.txt into the build Python environment first." }
& python -m PyInstaller --noconfirm --clean --onedir --console `
    --name BetterEndfield.BemConverter `
    --hidden-import bem_v11 `
    --distpath (Join-Path $Destination "dist") `
    --workpath (Join-Path $Destination "work") `
    --specpath $Destination `
    (Join-Path $repo "tools\CustomModel\bem_tool.py")
if ($LASTEXITCODE -ne 0) { throw "BEM converter build failed." }
& python (Join-Path $repo "tools\CustomModel\package_toolchain.py") (Join-Path $Destination "dist\BetterEndfield.BemConverter")
if ($LASTEXITCODE -ne 0) { throw "BEM toolchain packaging failed." }
Write-Host "BEM converter: $Destination\dist\BetterEndfield.BemConverter"

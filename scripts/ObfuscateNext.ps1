[CmdletBinding()]
param([Parameter(Mandatory=$true)][string]$AssemblyPath,
      [Parameter(Mandatory=$true)][string]$OutputDirectory,
      [string]$DependencyDirectory = '',
      [string]$WorkspaceConfig = '')
$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'Workspace.ps1')
$obfuscationWorkspace = Get-BEWorkspace -Config $WorkspaceConfig
$tool = Join-Path $obfuscationWorkspace.paths.toolchains 'obfuscar/obfuscar.console.exe'
if (-not (Test-Path -LiteralPath $tool -PathType Leaf)) {
    throw 'Install Obfuscar.GlobalTool 2.2.50 into toolchains/obfuscar before publishing Release.'
}
$assembly = Get-Item -LiteralPath $AssemblyPath
$output = [IO.Path]::GetFullPath($OutputDirectory)
New-Item -ItemType Directory -Path $output -Force | Out-Null
$escape = { param($value) [Security.SecurityElement]::Escape($value) }
$assemblyDirectory = Split-Path $assembly.FullName
$dotnetDirectory = Split-Path $obfuscationWorkspace.tools.dotnet
$env:DOTNET_ROOT = $dotnetDirectory
$searchDirectories = @($assemblyDirectory)
if ($DependencyDirectory) { $searchDirectories += $DependencyDirectory }
$searchDirectories += Get-ChildItem -LiteralPath (Join-Path $dotnetDirectory 'shared/Microsoft.NETCore.App') -Directory | Select-Object -ExpandProperty FullName
$searchXml = ($searchDirectories | ForEach-Object { '  <AssemblySearchPath path="' + (& $escape $_) + '" />' }) -join "`n"
$configuration = @"
<Obfuscator>
  <Var name="InPath" value="$(& $escape $assemblyDirectory)" />
  <Var name="OutPath" value="$(& $escape $output)" />
  <Var name="KeepPublicApi" value="true" />
  <Var name="HidePrivateApi" value="true" />
  <Var name="RenameProperties" value="false" />
  <Var name="RenameFields" value="false" />
  <Var name="HideStrings" value="false" />
  <Var name="UseUnicodeNames" value="false" />
$searchXml
  <Module file="$(& $escape $assembly.FullName)">
    <SkipType name="BetterEndfieldNext.UI.App*" />
    <SkipType name="BetterEndfieldNext.UI.MainWindow*" />
    <SkipNamespace name="BetterEndfieldNext.UI.Views" />
    <SkipNamespace name="BetterEndfieldNext.UI.Controls" />
    <SkipNamespace name="BetterEndfieldNext.UI.Models" />
    <SkipNamespace name="WinRT" />
    <SkipNamespace name="ABI" />
  </Module>
</Obfuscator>
"@
$configFile = Join-Path $output 'obfuscar.xml'
[IO.File]::WriteAllText($configFile, $configuration, [Text.UTF8Encoding]::new($false))
& $tool $configFile
if ($LASTEXITCODE -ne 0) { throw "Obfuscar failed with exit code $LASTEXITCODE." }
$result = Join-Path $output $assembly.Name
if (-not (Test-Path -LiteralPath $result -PathType Leaf)) { throw 'Obfuscar did not produce the application assembly.' }
if ((Get-FileHash -LiteralPath $result).Hash -eq (Get-FileHash -LiteralPath $assembly.FullName).Hash) {
    throw 'Application assembly was not transformed.'
}
Write-Output "Next assembly obfuscated; mappings remain in $output."
